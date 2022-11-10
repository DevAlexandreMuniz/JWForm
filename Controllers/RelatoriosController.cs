using JWForm.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JWForm.Controllers;

public class RelatoriosController : Controller
{
    private readonly IRelatorioRepository _relatorioRepository;

    public RelatoriosController(IRelatorioRepository relatorioRepository)
    {
        _relatorioRepository = relatorioRepository;
    }

    public async Task<IActionResult> index()
    {
        var relatorios = _relatorioRepository.Relatorios;
        return View(relatorios);
    }

    public async Task<IActionResult> Criar()
    {
        var relatorios = _relatorioRepository.Relatorios;

        return View(relatorios);
    }
}