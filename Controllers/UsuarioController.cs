using espacioKanban;
using espacioRepositories;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Controllers;
using espacioViewModels;
namespace espacioController;

public class UsuarioController : Controller{
    private readonly ILogger<TareaController> _logger;
    private readonly ITareaRepository _tareaRepository;
    private readonly ITableroRepository _tableroRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private static List<Usuario> usuarios = new List<Usuario>();

    public UsuarioController(ILogger<TareaController> logger, ITareaRepository tareaRepository, IUsuarioRepository usuarioRepository, ITableroRepository tableroRepository){
        _logger = logger;
        _tareaRepository = tareaRepository;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
    }


    //Muestra Usuarios
    public IActionResult Index(){
        try{
            if(!isLogin()) return RedirectToAction("Index", "Login");

            if(isAdmin()){
                usuarios = _usuarioRepository.GetAllUsuarios();
            }
            else{
                Usuario usuario = _usuarioRepository.GetAllUsuarios().FirstOrDefault(u => u.NombreUsuario == HttpContext.Session.GetString("Usuario") && u.Contrasenia == HttpContext.Session.GetString("Password"));
                usuarios.Add(usuario);
            }
            return View(usuarios);
        }
        catch(System.Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult AgregarUsuario(){ //Si agrego parametros envia un bad request
        try{
            if(!isLogin()) return RedirectToAction("Index", "Login");
            if(!isAdmin()) return RedirectToAction("Index");
            return View(new AgregarUsuarioViewModel());
        }
        catch(System.Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult AgregarUsuarioFromForm([FromForm] AgregarUsuarioViewModel nuevoUsuarioVM){
        try{
            if(!isLogin()) return RedirectToAction("Index", "Login");
            if(!isAdmin()) return RedirectToAction("Index");
            if(!ModelState.IsValid){
                return View("AgregarUsuario", nuevoUsuarioVM);
            }
            if (_usuarioRepository.ExisteUsuario(nuevoUsuarioVM.Nombre)){
                ModelState.AddModelError("Nombre", "Ya existe un usuario con este nombre de usuario");
                Console.WriteLine("Ya existe el nombre");
                return View("AgregarUsuario", nuevoUsuarioVM);
            }
            _usuarioRepository.CreateUsuario(new Usuario(nuevoUsuarioVM));
            return RedirectToAction("Index");
        }
        catch(System.Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult EditarUsuario(int idUsuario){  
        try{
            if(!isLogin()) return RedirectToAction("Index", "Login");
            Usuario usuario = _usuarioRepository.GetUsuarioByID(idUsuario);
            if(usuario != null){
                return View(new EditarUsuarioViewModel(usuario));
            }
            else{
                return RedirectToAction("Index");
            }
        }
        catch(System.Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
            
        }
    }

    [HttpPost]
    public IActionResult EditarUsuarioFromForm([FromForm] EditarUsuarioViewModel usuarioAEditarVM){
        try{
            if(!isLogin()) return RedirectToAction("Index", "Login");
            if(!isAdmin()) return RedirectToAction("Index");
            if(!ModelState.IsValid){
                return View("EditarUsuario", usuarioAEditarVM);
            }
            if (_usuarioRepository.ExisteUsuario(usuarioAEditarVM.Nombre)){
                ModelState.AddModelError("Nombre", "Ya existe un usuario con este nombre de usuario");
                Console.WriteLine("Ya existe el nombre");
                return View("EditarUsuario", usuarioAEditarVM);
            }
            _usuarioRepository.UpdateUsuario(new Usuario(usuarioAEditarVM));
            return RedirectToAction("Index");
        }
        catch(System.Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    public IActionResult DeleteUsuario(int idUsuario){
        try{
            if(!isLogin()) return RedirectToAction("Index", "Login");
            if(!isAdmin()) return RedirectToAction("Index");
            _tareaRepository.SetNullFromUser(idUsuario);
            _tableroRepository.RemoveTableroFromUser(idUsuario);
            _usuarioRepository.RemoveUsuario(idUsuario);
            return RedirectToAction("Index");
        }
        catch(System.Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
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