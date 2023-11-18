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


    //Muestra Usuarios
    public IActionResult Index(){
        tareas = tareaRepository.GetAllTareas();
        return View(tareas);
    }

    [HttpGet]
    public IActionResult AgregarTarea(){ //Si agrego parametros envia un bad request
        return View(new Tarea());
    }

    [HttpPost]
    public IActionResult AgregarTareaFromForm([FromForm] Tarea tarea){
        tareaRepository.CreateTarea(tarea);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult EditarTarea(int idTarea){  
        return View(tareaRepository.GetTareaById(idTarea));
    }

    [HttpPost]
    public IActionResult EditarTareaFromForm([FromForm] Tarea tarea){
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(){
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}