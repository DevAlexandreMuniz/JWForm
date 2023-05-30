using JWForm.Models;

namespace JWForm.Repositories.Interfaces;

public interface IPublicadorRepository
{
    IEnumerable<Publicador> Publicadores { get; }

    Publicador EncontrarPublicador(int publicadorId);
}