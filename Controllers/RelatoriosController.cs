using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using JWForm.ViewModels;
using JWForm.Context;
using JWForm.Models;
using System.Diagnostics;

namespace JWForm.Controllers;

[Authorize]
public class RelatoriosController : Controller
{
    private readonly Contexto db;

    public RelatoriosController(Contexto contexto)
    {
        db = contexto;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Criar()
    {
        var publicadores = await db.Publicadores.AsNoTracking().OrderBy(a => a.Nome).ToListAsync();

        ViewData["ListaDePublicadores"] = new SelectList(publicadores, "PublicadorId", "Nome");  

        ViewData["Mes"] = DateTime.Today.AddMonths(-1).ToString("yyyy-MM");

        var relatorios = await db.Relatorios.ToListAsync();

        return View(relatorios);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Criar(int publicador, int videos, int publicacoes, int revisitas, int estudosBiblicos, int horas, string observacao)
    {
        var data = DateTime.Today.AddMonths(-1);
   
        var relatorios = new Relatorio(data, videos, publicacoes, revisitas, estudosBiblicos, horas, observacao, publicador);

        await db.Relatorios.AddAsync(relatorios);
        await db.SaveChangesAsync();

        return View("_enviadoComSucesso");
    }

    public async Task<IActionResult> Editar(int id)
    {
        var relatorio = await db.Relatorios.Include(i => i.Publicador).SingleOrDefaultAsync(a => a.RelatorioId == id);

        var publicadores = await db.Publicadores.AsNoTracking().OrderBy(a => a.Nome).ToListAsync();

        ViewData["ListaDePublicadores"] = new SelectList(publicadores, "PublicadorId", "Nome");

        return View(relatorio);
    }

     [HttpPost]
    public async Task<IActionResult> Editar(int relatorioId, DateTime data, int publicador, int videos, int publicacoes, int revisitas, int estudosBiblicos, int horas, string observacao)
    {

        var relatorio = await db.Relatorios.Include(i => i.Publicador).SingleOrDefaultAsync(a => a.RelatorioId == relatorioId);

        relatorio.Atualizar(data, videos, publicacoes, revisitas, estudosBiblicos, horas, observacao, publicador);

        db.Update(relatorio);
        await db.SaveChangesAsync();

        return View("_enviadoComSucesso");
    }

    public async Task<IActionResult> Resumo(ResumoVM viewModel, DateTime data)
    {
        if(data == DateTime.MinValue)
            data = DateTime.Today.AddMonths(-1);

        ViewData["mes"] = data.ToString("yyyy-MM");

        viewModel.totalDeHorasPublicadoresNaoBatizados = db.Relatorios
            .Include(i => i.Publicador)
            .Where(w => w.Publicador.Tipo == TipoPublicador.NaoBatizado && w.Data.Month == data.Month && w.Data.Year == data.Year)
            .Sum(s => s.Horas);

        viewModel.totalDeHorasPublicadoresBatizados = db.Relatorios
            .Include(i => i.Publicador)
            .Where(w => w.Publicador.Tipo == TipoPublicador.Batizado && w.Data.Month == data.Month && w.Data.Year == data.Year)
            .Sum(s => s.Horas);

        viewModel.totalDeHorasPioneirosAuxiliares = db.Relatorios
            .Include(i => i.Publicador)
            .Where(w => w.Publicador.Tipo == TipoPublicador.PioneiroAuxiliar && w.Data.Month == data.Month && w.Data.Year == data.Year)
            .Sum(s => s.Horas);

        viewModel.totalDeHorasPioneirosRegulares = db.Relatorios
            .Include(i => i.Publicador)
            .Where(w => w.Publicador.Tipo == TipoPublicador.PioneiroRegular && w.Data.Month == data.Month && w.Data.Year == data.Year)
            .Sum(s => s.Horas);

        viewModel.totalDeHoras = db.Relatorios
            .Include(i => i.Publicador)
            .Where(w => w.Data.Month == data.Month && w.Data.Year == data.Year)
            .Sum(s => s.Horas);

        viewModel.totalDePublicadoresPendentes = db.Publicadores
            .Count(c => !c.Relatorios
                .Any(a => a.Data.Month == data.Month && a.Data.Year == data.Year));

        viewModel.totalDePublicadoresInativos = db.Publicadores
            .Count(c => !c.Relatorios
                .Any(a => a.Data.Month == DateTime.Today.Month && a.Data.Year == DateTime.Today.Year &&
                            a.Data.Month == DateTime.Today.AddMonths(-1).Month && a.Data.Year == DateTime.Today.AddMonths(-1).Year &&
                            a.Data.Month == DateTime.Today.AddMonths(-2).Month && a.Data.Year == DateTime.Today.AddMonths(-2).Year && 
                            a.Data.Month == DateTime.Today.AddMonths(-3).Month && a.Data.Year == DateTime.Today.AddMonths(-3).Year &&
                            a.Data.Month == DateTime.Today.AddMonths(-4).Month && a.Data.Year == DateTime.Today.AddMonths(-4).Year));        

        return View(viewModel);
    }

    public async Task<IActionResult> Listar(int publicadorId, DateTime data)
    {
        ViewData["mes"] = DateTime.Today.ToString("yyyy-MM");

        var relatorios = await db.Relatorios
            .Include(i => i.Publicador)
            .Where(w => w.Publicador.PublicadorId == publicadorId && w.Data.Month == data.Month)
            .ToListAsync();

        var publicadores = await db.Publicadores.AsNoTracking().OrderBy(a => a.Nome).ToListAsync();

        ViewData["ListaDePublicadores"] = new SelectList(publicadores, "PublicadorId", "Nome");

        return View(relatorios);
    }
}