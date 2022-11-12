using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using JWForm.Context;
using JWForm.Models;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> Criar(string nome, string grupoDeCampo)
    {
        var publicador = new Publicador(nome, grupoDeCampo);

        await db.Publicadores.AddAsync(publicador);
        await db.SaveChangesAsync();

        return View("_sucesso");
    }
}