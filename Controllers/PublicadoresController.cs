using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using JWForm.Context;
using JWForm.Models;

namespace JWForm.Controllers;

[Authorize]
public class PublicadoresController : Controller
{
    private readonly Contexto db;

    public PublicadoresController(Contexto contexo)
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

    public async Task<IActionResult> Editar(int id)
    {
        var publicador = await db.Publicadores.SingleOrDefaultAsync(a => a.PublicadorId == id);

        return View(publicador);
    }

    [HttpPost]
    public async Task<IActionResult> Editar(int publicadorId, string nome, string grupoDeCampo, TipoPublicador tipo)
    {

        var publicador = await db.Publicadores.SingleOrDefaultAsync(a => a.PublicadorId == publicadorId);

        publicador.Atualizar(nome, grupoDeCampo, tipo);

        db.Update(publicador);
        await db.SaveChangesAsync();

        return View("_cadastradoComSucesso");
    }

    public async Task<IActionResult> Listar()
    {
        var publicadores = await db.Publicadores.ToListAsync();
        return View(publicadores);
    }
}   