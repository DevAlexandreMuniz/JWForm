using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using JWForm.Context;

namespace JWForm.Services;

public class GeradorDeListas
{
    private readonly Contexto db;

    public GeradorDeListas(Contexto db)
    {
        this.db = db;
    }

    public SelectList Perfis()
    {
        var lista = new List<string>();

        lista.Add(Models.Perfis.Administrador);
        lista.Add(Models.Perfis.Comum);            
        
        return new SelectList(lista);
    }        

    public async Task<SelectList> Cores()
    {
        var lista = await db.Publicadores                
            .Select(w => new {w.PublicadorId, w.Nome})
            .AsNoTracking()
            .ToListAsync();
        
        return new SelectList(lista, "PublicadorId", "Nome");
    }        
}
