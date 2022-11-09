using JWForm.Models;

namespace JWForm.Repositories.Interfaces;

public interface IPublicadorRepository
{
    IEnumerable<Publicador> Publicadores { get; }
}