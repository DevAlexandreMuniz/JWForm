using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using JWForm.Context;

namespace JWForm.Controllers;

public class RelatoriosController : Controller
{
    private readonly Contexto _contexto;

    public RelatoriosController(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<IActionResult> Criar()
    {
        var relatorios = _contexto.Relatorios;

        return View(relatorios);
    }

    public async Task<IActionResult> Lista()
    {
        var relatorios = await _contexto.Relatorios.ToListAsync();
        return View(relatorios);
    }
}