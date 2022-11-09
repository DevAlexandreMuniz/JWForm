using JWForm.Repositories.Interfaces;
using JWForm.Context;
using JWForm.Models;

namespace JWForm.Repositories;

public class RelatorioRepository : IRelatorioRepository
{
    private readonly Contexto _context;

    public RelatorioRepository(Contexto context)
    {
        _context = context;
    }

    public IEnumerable<Relatorio> Relatorios => _context.Relatorios;
}