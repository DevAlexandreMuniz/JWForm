using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using JWForm.ViewModels;
using JWForm.Context;
using JWForm.Models;

namespace JWForm.Controllers;

public class RelatoriosController : Controller
{
    private readonly Contexto db;

    public RelatoriosController(Contexto contexto)
    {
        db = contexto;
    }

    public async Task<IActionResult> Criar()
    {
        var publicadores = await db.Publicadores.AsNoTracking().OrderBy(a => a.Nome).ToListAsync();

        ViewData["ListaDePublicadores"] = new SelectList(publicadores, "PublicadorId", "Nome");  

        var relatorios = await db.Relatorios.ToListAsync();

        return View(relatorios);
    }

    [HttpPost]
    public async Task<IActionResult> Criar(int publicador, string mes, int videos, int publicacoes, int revisitas, int estudosBiblicos, int horas, string observacao)
    {
        var relatorios = new Relatorio(mes, videos, publicacoes, revisitas, estudosBiblicos, horas, observacao, publicador);

        await db.Relatorios.AddAsync(relatorios);
        await db.SaveChangesAsync();

        return View("_enviadoComSucesso");
    }

    public async Task<IActionResult> Resumo(ResumoVM viewModel)
    {
        var relatorios = await db.Relatorios.Include(i => i.Publicador).ToListAsync();
        var publicadores = await db.Publicadores.ToListAsync();

        viewModel.totalDeHorasPublicadoresNaoBatizado = publicadores.Where(w => w.Tipo == TipoPublicador.NaoBatizado);

        viewModel.totalDeHorasPublicadoresBatizado = publicadores.Where(w => w.Tipo == TipoPublicador.Batizado);

        viewModel.totalDeHorasPioneiroAuxiliar = publicadores.Where(w => w.Tipo == TipoPublicador.PioneiroAuxiliar);

        viewModel.totalDeHorasPioneiroRegular = publicadores.Where(w => w.Tipo == TipoPublicador.PioneiroRegular);

        viewModel.totalDeHoras = relatorios.Sum(s => s.Horas);

        return View(viewModel);
    }

    public async Task<IActionResult> Lista()
    {
        var relatorios = await db.Relatorios.Include(i => i.Publicador).ToListAsync();
        return View(relatorios);
    }
}