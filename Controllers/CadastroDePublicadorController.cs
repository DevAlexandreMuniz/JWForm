using Microsoft.AspNetCore.Mvc;

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
}