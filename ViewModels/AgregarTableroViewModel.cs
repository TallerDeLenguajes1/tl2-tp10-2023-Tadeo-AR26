using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using espacioKanban;
namespace espacioViewModels;

public class AgregarTableroViewModel{
    private int id;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id")]

    private int idUsuarioPropietario;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id del Usuario Propietario")]
    private string nombre;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Nombre del Tablero")]
    private string descripcion;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Descripcion")] 

    public int Id { get => id; set => id = value; }
    public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
}