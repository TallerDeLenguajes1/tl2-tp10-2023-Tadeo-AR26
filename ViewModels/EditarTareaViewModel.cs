using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using espacioKanban;
namespace espacioViewModels;

public class EditarTareaViewModel{
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

    private string color;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Color")]
    public string Color { get => color; set => color = value; }

    private int idUsuarioAsignado;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id Usuario Asignado")]
    public int IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }

    public EditarTareaViewModel(){}

    public EditarTareaViewModel (Tarea tarea){
        id = tarea.Id;
        idTablero = tarea.Id_tablero;
        nombre = tarea.Nombre;
        estado = (espacioViewModels.estadoTarea)tarea.Estado;
        descripcion = tarea.Descripcion;
        color = tarea.Color;
        idUsuarioAsignado = tarea.IdUsuarioAsignado;
    }
}