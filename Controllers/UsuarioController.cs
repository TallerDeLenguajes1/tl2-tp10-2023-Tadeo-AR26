using espacioKanban;
using espacioRepositories;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Controllers;
using espacioViewModels;
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
        if(!isLogin()) return RedirectToAction("Index", "Login");

        List<Usuario> listaUsuarios = repositoryUsuario.GetAllUsuarios();
        List<ListarUsuarioViewModel> listarUsuariosVM = ListarUsuarioViewModel.FromUsuario(listaUsuarios);
        return View(listarUsuariosVM);
    }

    [HttpGet]
    public IActionResult AgregarUsuario(){ //Si agrego parametros envia un bad request
        if(!isLogin()){
            return RedirectToAction("Index", "Login");
        }
        AgregarUsuarioViewModel nuevoUsuarioVM = new AgregarUsuarioViewModel();
        return View(nuevoUsuarioVM);
    }

    [HttpPost]
    public IActionResult AgregarUsuarioFromForm([FromForm] AgregarUsuarioViewModel nuevoUsuarioVM){
        if(!ModelState.IsValid){
            return RedirectToAction("Index", "Login");
        }
        if(!isLogin()){
            return RedirectToAction("Index", "Login");
        }
        Usuario nuevoUsuario = Usuario.FromAgregarUsuarioViewModel(nuevoUsuarioVM);
        repositoryUsuario.AgregarUsuario(nuevoUsuario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EditarUsuario(int idUsuario){  
        if(!isLogin()){
            return RedirectToAction("Index", "Login");
        }

        Usuario usuarioAEditar = repositoryUsuario.GetUsuarioByID(idUsuario);
        EditarUsuarioViewModel usuarioAEditarVM = EditarUsuarioViewModel.FromUsuario(usuarioAEditar);
        return View(usuarioAEditarVM);
    }

    [HttpPost]
    public IActionResult EditarUsuarioFromForm([FromForm] EditarUsuarioViewModel usuarioAEditarVM){
        if(!ModelState.IsValid){
            return RedirectToAction("Index");
        }
        if(!isLogin()){
            return RedirectToAction("Index", "Login");
        }

        Usuario usuarioAEditar = Usuario.FromEditarUsuarioViewModel(usuarioAEditarVM);
        repositoryUsuario.UpdateUsuario(usuarioAEditar);
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

    private bool isLogin(){
        return HttpContext.Session.GetString("NivelDeAcceso") == "admin" || HttpContext.Session.GetString("NivelDeAcceso") == "simple";
    }

    private bool isAdmin(){
        return (HttpContext.Session.GetString("NivelDeAcceso") == "admin");
    }

}