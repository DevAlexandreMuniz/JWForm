using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using JWForm.Context;
using JWForm.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    public async Task<IActionResult> Pendentes(DateTime data)
    {
        if(data == DateTime.MinValue)
            data = DateTime.Today.AddMonths(-1);

        ViewData["mes"] = data.ToString("yyyy-MM");

        var publicadoresPendentes = await db.Publicadores
            .Where(w => !w.Relatorios
            .Any(a => a.Data.Month == data.Month && a.Data.Year == data.Year))
            .ToListAsync();

        return View(publicadoresPendentes);
    }

    public async Task<IActionResult> Listar(string grupoDeCampo)
    {
        var listaPublicadores = await db.Publicadores
            .AsNoTracking()
            .OrderBy(a => a.Nome)
            .ToListAsync();

        var publicadores = listaPublicadores
            .Where(w =>w.GrupoDeCampo == grupoDeCampo);        
            
        ViewData["ListaDePublicadores"] = new SelectList(listaPublicadores, "PublicadorId", "Nome");
        return View(publicadores);
    }
}   