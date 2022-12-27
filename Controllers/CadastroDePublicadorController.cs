using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using JWForm.Context;
using JWForm.Models;

namespace JWForm.Controllers;

public class CadastroDePublicadorController : Controller
{
    private readonly Contexto db;

    public CadastroDePublicadorController(Contexto contexo)
    {
        db = contexo;
    }
    public async Task<IActionResult> Criar()
    {
        var publicador = await db.Publicadores.ToListAsync();
        return View(publicador);
    }

    [HttpPost]
    public async Task<IActionResult> Criar(string nome, string grupoDeCampo, TipoPublicador tipo)
    {
        var publicador = new Publicador(nome, grupoDeCampo, tipo);

        await db.Publicadores.AddAsync(publicador);
        await db.SaveChangesAsync();

        return View("_cadastradoComSucesso");
    }
}