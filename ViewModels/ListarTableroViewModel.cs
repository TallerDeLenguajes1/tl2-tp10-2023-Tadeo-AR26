using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using espacioKanban;
namespace espacioViewModels;

public class ListarTableroViewModel{
    private int id;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id")]
    public int Id { get => id; set => id = value; }

    private int idUsuarioPropietario;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id Usuario Propietario")]
    public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }

    private string nombre;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Nombre Tablero")]
    public string Nombre { get => nombre; set => nombre = value; }

    private string descripcion;  
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Descripcion")] 
    public string Descripcion { get => descripcion; set => descripcion = value; }

    public ListarTableroViewModel(){}

    public ListarTableroViewModel(Tablero tablero){
        id = tablero.Id;
        idUsuarioPropietario = tablero.IdUsuarioPropietario;
        nombre = tablero.Nombre;
        descripcion = tablero.Descripcion;
    }

    public ListarTableroViewModel(List<Tablero> tableros) {
        foreach (var tablero in tableros){
            id = tablero.Id;
            idUsuarioPropietario = tablero.IdUsuarioPropietario;
            nombre = tablero.Nombre;
            descripcion = tablero.Descripcion;
        }
    }
}