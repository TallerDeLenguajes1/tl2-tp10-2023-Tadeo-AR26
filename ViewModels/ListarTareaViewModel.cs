using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using espacioKanban;
namespace espacioViewModels;

public enum estadoTarea{
    ToDo,
    Doing,
    Review,
    Done
}

public enum colorTarea{
    red,
    white,
    yellow,
    skyblue,
    green,
    black
}

public class ListarTareaViewModel{
    private int id;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id")]
    public int Id { get => id; set => id = value; }

    private int idTablero;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id Tablero")]
    public int IdTablero { get => idTablero; set => idTablero = value; }

    private string nombre;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Nombre Tarea")]
    public string Nombre { get => nombre; set => nombre = value; }

    private estadoTarea estado;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Estado")]
    public estadoTarea Estado { get => estado; set => estado = value; }

    private string descripcion;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Descripcion")]
    public string Descripcion { get => descripcion; set => descripcion = value; }

    private colorTarea color;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Color")]
    public colorTarea Color { get => color; set => color = value; }

    private int idUsuarioAsignado;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id Usuario Asignado")]
    public int IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }

    private string usuarioAsignado;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Usuario Asignado")]
    public string UsuarioAsignado { get => usuarioAsignado; set => usuarioAsignado = value; }

    private string tableroAsignado;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Usuario Asignado")]
    public string TableroAsignado { get => tableroAsignado; set => tableroAsignado = value; }


    public static List<ListarTareaViewModel> FromTarea(List<Tarea> tareas)
    {
        List<ListarTareaViewModel> listaTareasVM = new List<ListarTareaViewModel>();
        
            foreach (var tarea in tareas)
            {
                ListarTareaViewModel newTVM = new ListarTareaViewModel();
                newTVM.id = tarea.Id;
                newTVM.idTablero = tarea.Id_tablero;
                newTVM.nombre = tarea.Nombre;
                newTVM.estado = (espacioViewModels.estadoTarea)tarea.Estado;
                newTVM.descripcion = tarea.Descripcion;
                newTVM.color = (espacioViewModels.colorTarea)tarea.Color;
                newTVM.idUsuarioAsignado = tarea.IdUsuarioAsignado;
                listaTareasVM.Add(newTVM);
            }
            return(listaTareasVM);
    }

    public ListarTareaViewModel(){}

    public ListarTareaViewModel(List<Tarea> tareas){
        foreach (var tarea in tareas){
            id = tarea.Id;
            idTablero = tarea.Id_tablero;
            nombre = tarea.Nombre;
            estado = (espacioViewModels.estadoTarea)tarea.Estado;
            descripcion = tarea.Descripcion;
            color = (espacioViewModels.colorTarea)tarea.Color;
            idUsuarioAsignado = tarea.IdUsuarioAsignado;
            usuarioAsignado = tarea.UsuarioAsignado;
            tableroAsignado = tarea.TableroAsignado;
        }
    }
    
}