using Microsoft.AspNetCore.Mvc;

namespace RedNb.WebGateway.Host.Controllers;

public class HomeController : AbpController
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }
}