using JWForm.Repositories.Interfaces;
using JWForm.Context;
using JWForm.Models;

namespace JWForm.Repositories;

public class PublicadorRepository : IPublicadorRepository
{
    private readonly Contexto _context;

    public PublicadorRepository(Contexto context)
    {
        _context = context;
    }

    public IEnumerable<Publicador> Publicadores => _context.Publicadores;

    public IEnumerable<Publicador> PublicadoresPendentes => _context.Publicadores.Where(w => !w.EnviouORelatorio);

    public Publicador EncontrarPublicador(int publicadorId)
    {
        return _context.Publicadores.FirstOrDefault(f => f.PublicadorId == publicadorId);
    }
}