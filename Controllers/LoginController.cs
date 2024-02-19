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
    private readonly IUsuarioRepository _usuarioRepository;
    public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository){
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    // Función que crea la sesión, se llama en el LoginUser
    private void CreateSession(Usuario usuarioLogeado){
        HttpContext.Session.SetString("Usuario", usuarioLogeado.NombreUsuario);
        HttpContext.Session.SetString("Contrasenia", usuarioLogeado.Contrasenia);
        HttpContext.Session.SetString("NivelDeAcceso", usuarioLogeado.Rol.ToString());
    }

    public IActionResult Index(){
        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult LoginUser(LoginViewModel usuarioVM){
        try{
            if (!ModelState.IsValid) return RedirectToAction("Index");
            var usuarios = _usuarioRepository.GetAllUsuarios();
            var user = usuarios.FirstOrDefault(u => u.NombreUsuario == usuarioVM.Nombre && u.Contrasenia == usuarioVM.Contrasenia);
            if (user == null){
                _logger.LogWarning("Intento de acceso invalido - Usuario:" + usuarioVM.Nombre + " Clave ingresada: " + usuarioVM.Contrasenia);
                return RedirectToAction("Index");
            }

            _logger.LogInformation("el usuario " + user.NombreUsuario + " ingreso correctamente");
            CreateSession(user); //Invoca a la función para crear la sesión

            return RedirectToRoute(new { controller = "Tablero", action = "Index" });
        }
        catch (Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest("Fallo el inicio de sesion");
        }
    }

    public IActionResult LogOut(){
    if(HttpContext.Session.GetString("Usuario") != null) {
        HttpContext.Session.Remove("Usuario");
    }
    if(HttpContext.Session.GetString("Contrasenia") != null) {
        HttpContext.Session.Remove("Contrasenia");
    }
    if(HttpContext.Session.GetString("NivelDeAcceso") != null) {
        HttpContext.Session.Remove("NivelDeAcceso");
    }
    return RedirectToAction("Index", "Login");
    }

    
}