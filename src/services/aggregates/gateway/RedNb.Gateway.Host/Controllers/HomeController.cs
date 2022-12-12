using Microsoft.AspNetCore.Mvc;

namespace RedNb.Gateway.Host.Controllers;

public class HomeController : AbpController
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _conf;

    public HomeController(ILogger<HomeController> logger, IConfiguration conf)
    {
        _logger = logger;
        _conf = conf;
    }

    public IActionResult Index()
    {
        var a = _conf["ConnectionStrings:Default"];

        return View();
    }
}