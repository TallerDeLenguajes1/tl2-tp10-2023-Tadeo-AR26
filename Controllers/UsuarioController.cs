using espacioKanban;
using espacioRepositories;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Controllers;
namespace espacioController;

public class UsuarioController : Controller{
    private readonly ILogger<HomeController> _logger;
    private static List<Usuario> usuarios = new List<Usuario>();
    UsuarioRepository repositoryUsuario;

    public UsuarioController(ILogger<HomeController> logger){
        _logger = logger;
        repositoryUsuario = new UsuarioRepository();
    }


    //Muestra Usuarios
    public IActionResult Index(){
        usuarios = repositoryUsuario.GetAllUsuarios();
        return View(usuarios);
    }

    [HttpGet]
    public IActionResult AgregarUsuario(){ //Si agrego parametros envia un bad request
        return View(new Usuario());
    }

    [HttpPost]
    public IActionResult AgregarUsuarioFromForm([FromForm] Usuario usuario){
        repositoryUsuario.CreateUsuario(usuario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EditarUsuario(int idUsuario){  
        return View(repositoryUsuario.GetUsuarioByID(idUsuario));
    }

    [HttpPost]
    public IActionResult EditarUsuarioFromForm([FromForm] Usuario usuario, int id){
        repositoryUsuario.UpdateUsuario(usuario, id);
        return RedirectToAction("Index");
    }

    public IActionResult DeleteUsuario(int idUsuario){
        repositoryUsuario.RemoveUsuario(idUsuario);
        return RedirectToAction("Index");
    }


    public IActionResult Privacy(){
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(){
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}