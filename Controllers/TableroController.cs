using espacioKanban;
using espacioRepositories;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Controllers;
using espacioViewModels;
namespace espacioController;

public class TableroController : Controller{
    private readonly ILogger<TareaController> _logger;
    private readonly ITareaRepository _tareaRepository;
    private readonly ITableroRepository _tableroRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public TableroController(ILogger<TareaController> logger, ITareaRepository tareaRepository, IUsuarioRepository usuarioRepository, ITableroRepository tableroRepository){
        _logger = logger;
        _tareaRepository = tareaRepository;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index(){
        try{
            if(!isLogin()) return RedirectToAction("Index", "Login");
            int id = getId();
            List<Tablero> tableros = _tableroRepository.GetAllTablerosFromUser(id);
            if(tableros != null){
                return View(tableros);
            }
            else{
                throw new Exception("Este usuario no posee tableros");
            }
            
        }
        catch(System.Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult GestionarTableros(){
        try{
            if(!isLogin()){
                return RedirectToAction("Index", "Login");
            }
            if(!isAdmin()){
                return RedirectToAction("Index");
            }
            List<Tablero> tableros = _tableroRepository.GetAllTableros();
            return View("Index", tableros); // Usando "Index", tableros utilizo la View del indice con todos los tableros en lugar de sólo los tableros del usuario
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
            List<Usuario> listaDeUsuarios;
            listaDeUsuarios = _usuarioRepository.GetAllUsuarios();
            return View(new AgregarTableroViewModel(listaDeUsuarios));
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
            if(!ModelState.IsValid){
                return View("AgregarTablero", nuevoTableroVM);
            }
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
            List<Usuario> listaDeUsuarios;
            listaDeUsuarios = _usuarioRepository.GetAllUsuarios();
            Tablero tablero = _tableroRepository.GetTableroByID(idTablero);
            if(tablero != null){
                return View(new EditarTableroViewModel(tablero, listaDeUsuarios));
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
            if(!ModelState.IsValid){
                return View("EditarTablero", tableroAEditarVM);
            }
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
            _tareaRepository.SetNullFromBoard(idTablero);
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

    private bool isLogin(){ //Funcion que controla que exista una sesión
        return HttpContext.Session.GetString("NivelDeAcceso") == "admin" || HttpContext.Session.GetString("NivelDeAcceso") == "simple";
    }

    private bool isAdmin(){ //Función que controla que el usuario sea del tipo administrador
        return (HttpContext.Session.GetString("NivelDeAcceso") == "admin");
    }

    private int getId(){ //Funcion que devuelve el ID del usuario que se encuentra logeado
        Usuario usuario = _usuarioRepository.GetAllUsuarios().FirstOrDefault(u => u.NombreUsuario == HttpContext.Session.GetString("Usuario"));
        return usuario.Id;
    }

}