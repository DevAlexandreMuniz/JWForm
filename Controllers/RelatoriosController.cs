using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using JWForm.ViewModels;
using JWForm.Context;
using JWForm.Models;
using System.Diagnostics;
using System.Reflection;
using System.ComponentModel;

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
        var data = DateTime.Today.AddMonths(-1).ToUniversalTime();
   
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

        var relatorio = await db.Relatorios
            .Include(i => i.Publicador)
            .SingleOrDefaultAsync(a => a.RelatorioId == relatorioId);

        relatorio.Atualizar(data, videos, publicacoes, revisitas, estudosBiblicos, horas, observacao, publicador);

        db.Update(relatorio);
        await db.SaveChangesAsync();

        return View("_enviadoComSucesso");
    }

    public async Task<IActionResult> Apagar(int relatorioId)
    {

        var relatorio = await db.Relatorios
            .Include(i => i.Publicador)
            .SingleOrDefaultAsync(a => a.RelatorioId == relatorioId);

        if (relatorio == null)
            return NotFound();

        return View(relatorio);
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmarParaApagar(Relatorio relatorio)
    {
        if (relatorio is null)
            throw new ArgumentNullException(nameof(relatorio));

        db.Relatorios.Remove(relatorio);
        await db.SaveChangesAsync();

        return RedirectToAction(nameof(Listar));
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

        var quatroMesesAtras = DateTime.Today.AddMonths(-4);

        viewModel.totalDePublicadoresInativos = db.Publicadores
            .Count(c => !c.Relatorios
                .Any(a => a.Data.Month >= quatroMesesAtras.Month && a.Data.Year >= quatroMesesAtras.Year));        

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



    public static string GetEnumDescription(Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());
        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes.Length > 0)
            return attributes[0].Description;
        else
            return value.ToString();
    }
}
