using DependencyLab.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Välj sätt att spara böcker
// builder.Services.AddSingleton<IBookRepository, JsonFileBookRepository>(); // Json
builder.Services.AddSingleton<IBookRepository, InMemoryBookRepository>(); // I minnet

// Välj hur bookservice ska fungera
// builder.Services.AddScoped<IBookService, BookService>(); // Den vanliga
builder.Services.AddScoped<IBookService, EnhancedBookService>(); // loggar + ser till att max 10 borrows

// Välj notification
// builder.Services.AddScoped<INotificationService, EmailNotificationService>(); // Skicka ut ett mejl
builder.Services.AddScoped<INotificationService, ConsoleNotificationService>(); // Logga i consolen


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
