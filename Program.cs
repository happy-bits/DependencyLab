using DependencyLab.Models;
using DependencyLab.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Välj sätt att spara böcker
switch (Settings.RepositoryChoise)
{
    case RepositoryChoise.InMemory:
        builder.Services.AddSingleton<IBookRepository, InMemoryBookRepository>();
        break;
    case RepositoryChoise.Csv:
        builder.Services.AddScoped<IBookRepository, CsvFileBookRepository>();
        break;
    case RepositoryChoise.Json:
        builder.Services.AddScoped<IBookRepository, JsonFileBookRepository>();
        break;

    default:
        throw new NotImplementedException();

}

// Välj hur bookservice ska fungera
// builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookService, EnhancedBookService>();

bool isProduction = true;

if (isProduction)
{
    builder.Services.AddScoped<INotificationService, EmailNotificationService>();
}
else
{
    builder.Services.AddScoped<INotificationService, ConsoleNotificationService>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
)
.WithStaticAssets();

app.Run();
