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
}