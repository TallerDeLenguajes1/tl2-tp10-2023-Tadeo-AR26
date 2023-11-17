using espacioKanban;
using espacioRepositories;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Controllers;
namespace espacioController;

[ApiController]
[Route("[controller]")]
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
    public IActionResult agregarUsuario(){ //Si agrego parametros envia un bad request
        return View(new Usuario());
    }

    [HttpPost]
    public IActionResult agregarUsuarioFromForm([FromForm] Usuario usuario){
        repositoryUsuario.CreateUsuario(usuario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult editarUsuario(int idUsuario){  
        return View(repositoryUsuario.GetUsuarioByID(idUsuario));
    }

    [HttpPost]
    public IActionResult editarUsuarioFromForm([FromForm] Usuario usuario, int id){
        repositoryUsuario.UpdateUsuario(usuario, id);
        return RedirectToAction("Index");
    }

    public IActionResult DeleteUser(int idUsuario){
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