using espacioKanban;
using espacioRepositories;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Controllers;
namespace espacioController;

public class TareaController : Controller{
    private readonly ILogger<HomeController> _logger;
    private static List<Tarea> tareas = new List<Tarea>();
    TareaRepository tareaRepository;

    public TareaController(ILogger<HomeController> logger){
        _logger = logger;
        tareaRepository = new TareaRepository();
    }


    public IActionResult Index(int? idTablero){
        List<Tarea> listaTareas = null;
        if(!isLogin()){
            return RedirectToAction("Index", "Login");
        }
        if(isAdmin()){
            listaTareas = tareaRepository.GetAllTareas();
        }
        else if(idTablero.HasValue){
            listaTareas = tareaRepository.GetAllTareasFromTablero(idTablero);
        }
        else{
            return NotFound();
        }

        List<ListarTareaViewModel> listarTareaVM = ListarTareaViewModel.FromTarea(listaTareas);
        return View(listarTareaVM);
    }

    [HttpGet]
    public IActionResult AgregarTarea(){ //Si agrego parametros envia un bad request
        if(!isLogin()){
            return RedirectToAction("Index", "Login");
        }
        AgregarTareaViewModel nuevaTareaVM = new AgregarTareaViewModel();
        return View(nuevaTareaVM);
    }

    [HttpPost]
    public IActionResult AgregarTareaFromForm([FromForm] AgregarTareaViewModel nuevaTareaVM){
        if(!ModelState.IsValid){
            return RedirectToAction("Index");
        }
        if(!isLogin()){
            return RedirectToAction("Index", "Login");
        }

        Tarea nuevaTarea = Tarea.FromAgregarTareaViewModel(nuevaTareaVM);
        TareaRepository.UpdateTarea(nuevaTarea);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EditarTarea(int idTarea){  
        if(!isLogin()){
            return RedirectToAction("Index", "Login");
        }
        
        Tarea tareaAEditar = tareaRepository.GetTareaById(idTarea);
        EditarTareaViewModel tareaAEditarVM = EditarTareaViewModel.FromTarea(tareaAEditar);
        return View(tareaAEditarVM);
    }

    [HttpPost]
    public IActionResult EditarTareaFromForm([FromForm] EditarTareaViewModel tareaAEditarVM){
        if(!ModelState.IsValid){
            return RedirectToAction("Index");
        } 
        if(!isLogin()){
            return RedirectToAction("Index","Login"); 
        }

        Tarea tareaAEditar = Tarea.FromEditarTareaViewModel(tareaAEditarVM);
        tareaRepository.UpdateTarea(tarea);
        return RedirectToAction("Index");
    }

    public IActionResult DeleteTarea(int idTarea){
        tareaRepository.RemoveTarea(idTarea);
        return RedirectToAction("Index");
    }

    public IActionResult Privacy(){
        return View();
    }

    private bool isLogin(){
        return HttpContext.Session.GetString("NivelDeAcceso") == "admin" || HttpContext.Session.GetString("NivelDeAcceso") == "simple";
    }

    private bool isAdmin(){
        return (HttpContext.Session.GetString("NivelDeAcceso") == "admin");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(){
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}