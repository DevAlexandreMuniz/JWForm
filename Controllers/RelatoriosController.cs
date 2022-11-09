using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using JWForm.Models;

namespace JWForm.Controllers;

public class RelatoriosController : Controller
{
    private readonly ILogger<RelatoriosController> _logger;

    public RelatoriosController(ILogger<RelatoriosController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {

        return View();
    }

    public async Task<IActionResult> Criar()
    {

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}