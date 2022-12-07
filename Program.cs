using JWForm.Context;
using JWForm.Repositories;
using JWForm.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<JWForm.Services.GeradorDeListas>(); 
builder.Services.AddTiaIdentity()
                .AddCookie(x =>
                {
                    x.LoginPath = "/autenticacao/login";                  
                    x.AccessDeniedPath = "/autenticacao/acessonegado";                                
                });    

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Contexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("db")));

builder.Services.AddTransient<IPublicadorRepository, PublicadorRepository>();
builder.Services.AddTransient<IRelatorioRepository, RelatorioRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseTiaIdentity();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
