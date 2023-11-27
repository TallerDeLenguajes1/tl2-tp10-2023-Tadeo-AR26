using espacioKanban;
using espacioRepositories;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Controllers;
namespace espacioController;
using espacioViewModels;

public class LoginController : Controller{
    List<Login> listaDeTiposDelogins = new List<Login>();
    private readonly ILogger<LoginController> _logger;
    public LoginController(ILogger<LoginController> logger){
        _logger = logger;

        Login loginAdmin = new Login();
        loginAdmin.Nombre = "admin";
        loginAdmin.Contrasenia = "admin";
        loginAdmin.Nivel = nivelDeAcceso.admin;

        Login loginSimple = new Login();
        loginSimple.Nombre = "simple";
        loginSimple.Contrasenia = "simple";
        loginSimple.Nivel = nivelDeAcceso.simple;

        Login loginSimple2 = new Login();
        loginSimple.Nombre = "simple2";
        loginSimple.Contrasenia = "simple2";
        loginSimple.Nivel = nivelDeAcceso.simple;

        listaDeTiposDelogins.Add(loginAdmin);
        listaDeTiposDelogins.Add(loginSimple);
        listaDeTiposDelogins.Add(loginSimple2);
    }

    public IActionResult Index(){
        LoginViewModel login = new LoginViewModel();
        return View(login);
    }

    public IActionResult Login(Login login){
        Login usuarioPorLoguear = null;
        usuarioPorLoguear = listaDeTiposDelogins.FirstOrDefault(u => u.Nombre == login.Nombre && u.Contrasenia == login.Contrasenia);
        if (usuarioPorLoguear == null){
            return RedirectToAction("Index");
        }
        logearUsuario(usuarioPorLoguear);
        return RedirectToAction("Index", "Usuario");
    }

    private void logearUsuario(Login usuarioPorLoguear){
        HttpContext.Session.SetString("Nombre", usuarioPorLoguear.Nombre);
        HttpContext.Session.SetString("Contrasenia", usuarioPorLoguear.Contrasenia);
        HttpContext.Session.SetString("NivelDeAcceso", Convert.ToString(usuarioPorLoguear.Nivel));
    }
}