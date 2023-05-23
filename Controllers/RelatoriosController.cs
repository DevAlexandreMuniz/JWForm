using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using JWForm.ViewModels;
using JWForm.Context;
using JWForm.Models;
using System.Diagnostics;

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
    public async Task<IActionResult> Criar(int publicador, DateTime mes, int videos, int publicacoes, int revisitas, int estudosBiblicos, int horas, string observacao)
    {
        var relatorios = new Relatorio(mes, videos, publicacoes, revisitas, estudosBiblicos, horas, observacao, publicador);

        await db.Relatorios.AddAsync(relatorios);
        await db.SaveChangesAsync();

        return View("_enviadoComSucesso");
    }

    public async Task<IActionResult> Resumo(ResumoVM viewModel, DateTime mes)
    {
        var relatorios = await db.Relatorios.Include(i => i.Publicador).ToListAsync();

        viewModel.totalDeHorasPublicadoresNaoBatizados = relatorios.Where(w => w.Publicador.Tipo == TipoPublicador.NaoBatizado && w.Mes == mes).Sum(s => s.Horas);

        viewModel.totalDeHorasPublicadoresBatizados = relatorios.Where(w => w.Publicador.Tipo == TipoPublicador.Batizado && w.Mes == mes).Sum(s => s.Horas);

        viewModel.totalDeHorasPioneirosAuxiliares = relatorios.Where(w => w.Publicador.Tipo == TipoPublicador.PioneiroAuxiliar && w.Mes == mes).Sum(s => s.Horas);

        viewModel.totalDeHorasPioneirosRegulares = relatorios.Where(w => w.Publicador.Tipo == TipoPublicador.PioneiroRegular && w.Mes == mes).Sum(s => s.Horas);

        viewModel.totalDeHoras = relatorios.Where(w => w.Mes == mes).Sum(s => s.Horas);

        return View(viewModel);
    }

    public async Task<IActionResult> Lista()
    {
        var relatorios = await db.Relatorios.Include(i => i.Publicador).ToListAsync();
        return View(relatorios);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}