using DependencyLab.Services;
using Microsoft.AspNetCore.Mvc;

namespace DependencyLab.Controllers;

public class Happy2Controller(IBookRepository repo) : Controller
{
    public IActionResult Index()
    {
        return Ok(repo.GetAllBooks().Select(book => book.Title));
    }
}