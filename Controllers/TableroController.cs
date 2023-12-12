using espacioKanban;
using espacioRepositories;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Controllers;
using espacioViewModels;
namespace espacioController;

public class TableroController : Controller{
    private readonly ILogger<TableroController> _logger;
    private readonly ITableroRepository _tableroRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public TableroController(ILogger<TableroController> logger, IUsuarioRepository usuarioRepository, ITableroRepository tableroRepository){
        _logger = logger;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index(){
        try{
            if(!isLogin()) return RedirectToAction("Index", "Login");
            if(isAdmin()){
                List<Tablero> tableros = _tableroRepository.GetAllTableros();
                return View(tableros);
            }
            else{
                Usuario usuario = _usuarioRepository.GetAllUsuarios().FirstOrDefault(u => u.NombreUsuario == HttpContext.Session.GetString("Usuario") && u.Contrasenia == HttpContext.Session.GetString("Password"));
                Tablero tablero = _tableroRepository.GetTableroByID(usuario.Id);
                return View(tablero);
            }
        }
        catch(System.Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult AgregarTablero(){
        try{
            if(!isLogin()) return RedirectToAction("Index", "Login");
            if(!isAdmin()) return RedirectToAction("Index");
            return View(new AgregarTableroViewModel());
        }
        catch(System.Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult AgregarTableroFromForm([FromForm] AgregarTableroViewModel nuevoTableroVM){
        try{
            if(!isLogin()) return RedirectToAction("Index", "Login");
            if(!isAdmin()) return RedirectToAction("Index");
            if(!ModelState.IsValid) return RedirectToAction("AgregarTablero");
            _tableroRepository.CreateTablero(new Tablero(nuevoTableroVM));
            return RedirectToAction("Index");
        }
        catch(System.Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult EditarTablero(int idTablero){  
        try{
            if(!isLogin()) return RedirectToAction("Index", "Login");
            Tablero tablero = _tableroRepository.GetTableroByID(idTablero);
            if(tablero != null){
                return View(new EditarTableroViewModel(tablero));
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
    public IActionResult EditarTableroFromForm([FromForm] EditarTableroViewModel tableroAEditarVM){
        try{
            if(!isLogin()) return RedirectToAction("Index", "Login");
            Console.WriteLine("Test de Login");
            if(!isAdmin()) return RedirectToAction("Index");
            Console.WriteLine("Test de Admin");
            if(!ModelState.IsValid) return RedirectToAction("EditarTablero");
            Console.WriteLine("Test de ModelValid");
            _tableroRepository.UpdateTablero(new Tablero(tableroAEditarVM));
            return RedirectToAction("Index");
        }
        catch(System.Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    public IActionResult DeleteTablero(int idTablero){
        try{
            if(!isLogin()) return RedirectToAction("Index", "Login");
            if(!isAdmin()) return RedirectToAction("Index");
            _tableroRepository.RemoveTablero(idTablero);
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