using espacioKanban;
using espacioRepositories;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Controllers;
using espacioViewModels;
namespace espacioController;

public class TareaController : Controller{
    private readonly ILogger<TareaController> _logger;
    private readonly ITareaRepository _tareaRepository;
    private readonly ITableroRepository _tableroRepository;
    private readonly IUsuarioRepository _usuarioRepository;

    public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository, IUsuarioRepository usuarioRepository, ITableroRepository tableroRepository){
        _logger = logger;
        _tareaRepository = tareaRepository;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
    }


    public IActionResult Index(){
        try{
            if(!isLogin()){
                return RedirectToAction("Index", "Login");
            }
            int id = getId();
            List<Tarea> tareas = _tareaRepository.GetAllTareasFromUser(id);
            return View(tareas);
        }
        catch (System.Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult GestionarTareas(){
        try{
            if(!isLogin()){
                return RedirectToAction("Index", "Login");
            }
            if(!isAdmin()){
                return RedirectToAction("Index");
            }
            List<Tarea> tareas = _tareaRepository.GetAllTareas();
            return View("Index", tareas); // Usando "Index", tareas utilizo la View del indice con todas las tareas en lugar de sólo las tareas del usuario
        }
        catch(System.Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult AgregarTarea(){
        try{
            if(!isLogin()) return RedirectToAction("Index", "Login");
            return View(new AgregarTareaViewModel(getId()));
        }
        catch(System.Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult AgregarTareaFromForm([FromForm] AgregarTareaViewModel nuevaTareaVM){
        try{
            if(!isLogin()) return RedirectToAction("Index", "Login");
            if(!ModelState.IsValid) return RedirectToAction("AgregarTarea");
            _tareaRepository.CreateTarea(new Tarea(nuevaTareaVM));
            return RedirectToAction("Index", new { idTablero = nuevaTareaVM.IdTablero });
        }
        catch(System.Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult EditarTarea(int idTarea){  
        try{
            if(!isLogin()) return RedirectToAction("Index", "Login");
            Tarea tarea = _tareaRepository.GetTareaById(idTarea);
            if(tarea != null){
                return View(new EditarTareaViewModel(tarea));
            }
            else{
                return RedirectToAction("Index", new { idTablero = _tareaRepository.GetTareaById(idTarea).Id_tablero });
            }
        }
        catch(System.Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
            
        }
    }

    [HttpPost]
    public IActionResult EditarTareaFromForm([FromForm] EditarTareaViewModel tareaAEditarVM){
        try{
            if(!isLogin()) return RedirectToAction("Index", "Login");
            if(!ModelState.IsValid) return RedirectToAction("Index");
            _tareaRepository.UpdateTarea(new Tarea(tareaAEditarVM));
            return RedirectToAction("Index", new { idTablero = tareaAEditarVM.IdTablero });
        }
        catch(System.Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    public IActionResult DeleteTarea(int idTarea){
        try{
            if(!isLogin()) return RedirectToAction("Index", "Login");
            if(!isAdmin()) return RedirectToAction("Index");
            Tarea tarea = _tareaRepository.GetTareaById(idTarea);
            int idT = tarea.Id_tablero;
            _tareaRepository.RemoveTarea(idTarea);
            return RedirectToAction("Index", new { idTablero = idT });
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

    private int getId(){ //Obtiene el ID
        Usuario usuario = _usuarioRepository.GetAllUsuarios().FirstOrDefault(u => u.NombreUsuario == HttpContext.Session.GetString("Usuario"));
        return usuario.Id;
    }

}