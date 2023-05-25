using JWForm.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace JWForm.Controllers;

public class HomeController : Controller
{
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string senha)
    {
        if (senha == "8318")
        {
            await Logar();
            return RedirectToAction("Resumo", "Relatorios");
        }

        return View();
    }

    private async Task Logar()
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Alexandre"),
                new Claim(ClaimTypes.NameIdentifier, "Alexandre")
            };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
        var authProperties = new AuthenticationProperties { IsPersistent = true };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            claimPrincipal,
            authProperties);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}