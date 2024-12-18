using Microsoft.AspNetCore.Mvc;

namespace DependencyLab.Controllers;

public class AboutController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public IActionResult Testing()
    {
        _logger.LogInformation("👉 Testing Method");
        return Ok("Testing");
    }
}