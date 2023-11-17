using espacioKanban;
using espacioRepositories;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Controllers;
namespace espacioController;

[ApiController]
[Route("[controller]")]
public class TableroController : Controller{
    private readonly ILogger<HomeController> _logger;
    private static List<Tablero> tableros = new List<Tablero>();
    TableroRepository tableroRepository;

    public TableroController(ILogger<HomeController> logger){
        _logger = logger;
        tableroRepository = new TableroRepository();
    }


    //Muestra Usuarios
    public IActionResult Index(){
        tableros = tableroRepository.GetAllTableros();
        return View(tableros);
    }

    [HttpGet]
    public IActionResult agregarTablero(){ //Si agrego parametros envia un bad request
        return View(new Tablero());
    }

    [HttpPost]
    public IActionResult agregarTableroFromForm([FromForm] Tablero tablero){
        tableroRepository.CreateTablero(tablero);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult editarTablero(int idTablero){  
        return View(tableroRepository.GetTableroByID(idTablero));
    }

    [HttpPost]
    public IActionResult editarTableroFromForm([FromForm] Tablero tablero){
        tableroRepository.UpdateTablero(tablero);
        return RedirectToAction("Index");
    }

    public IActionResult deleteTablero(int idTablero){
        tableroRepository.RemoveTablero(idTablero);
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