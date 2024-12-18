using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DependencyLab.Models;

namespace DependencyLab.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{

    public IActionResult Index()
    {
        logger.LogInformation("👉 Hallå eller?");
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
