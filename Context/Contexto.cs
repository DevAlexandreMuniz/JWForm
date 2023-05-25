using Microsoft.EntityFrameworkCore;
using JWForm.Models;

namespace JWForm.Context;
public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<Publicador> Publicadores { get; set; }

    public DbSet<Relatorio> Relatorios { get; set; }
}