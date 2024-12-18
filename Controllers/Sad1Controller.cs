using DependencyLab.Models;
using DependencyLab.Services;
using Microsoft.AspNetCore.Mvc;

namespace DependencyLab.Controllers;

public class Sad1Controller(
    ILogger<CsvFileBookRepository> loggerCsv,
    ILogger<JsonFileBookRepository> loggerJson) : Controller
{
    public IActionResult Index()
    {
        IBookRepository repo;

        switch (Settings.RepositoryChoise)
        {
            case RepositoryChoise.InMemory:
                repo = new InMemoryBookRepository();
                break;
            case RepositoryChoise.Csv:
                repo = new CsvFileBookRepository(loggerCsv);
                break;
            case RepositoryChoise.Json:
                repo = new JsonFileBookRepository(loggerJson);
                break;

            default:
                throw new NotImplementedException();

        }

        return Ok(repo.GetAllBooks().Select(book => book.Title));
    }
}