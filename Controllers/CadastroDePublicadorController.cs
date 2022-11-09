using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JWForm.Models;

namespace JWForm.Controllers;

public class CadastroDePublicadorController : Controller
{
    private readonly ILogger<CadastroDePublicadorController> _logger;

    public CadastroDePublicadorController(ILogger<CadastroDePublicadorController> logger)
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