using JWForm.Models;

namespace JWForm.Repositories.Interfaces;

public interface IRelatorioRepository
{
    
    IEnumerable<Relatorio> Relatorios { get; }
}