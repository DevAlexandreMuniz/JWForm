using Microsoft.AspNetCore.Authentication.Cookies;
using JWForm.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using JWForm.Repositories;
using JWForm.Context;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<JWForm.Services.GeradorDeListas>();   

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = "/Home/Login";
        o.ExpireTimeSpan = new System.TimeSpan(5,0,0,0);
    });

builder.Services.AddAuthorization();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Contexto>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("db")));

builder.Services.AddTransient<IPublicadorRepository, PublicadorRepository>();
builder.Services.AddTransient<IRelatorioRepository, RelatorioRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Relatorios}/{action=Criar}/{id?}");

app.Run();
