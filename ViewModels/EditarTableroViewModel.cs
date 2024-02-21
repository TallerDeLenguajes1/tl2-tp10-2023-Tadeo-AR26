using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using espacioKanban;
namespace espacioViewModels;

public class EditarTableroViewModel{
    private int id;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Id")]
    
    private int idUsuarioPropietario;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Id Usuario Propietario")]
    
    private string? nombre;
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Nombre Tablero")]

    private string? descripcion;  
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Descripcion")]

    private List<Usuario> usuarios;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Descripcion")]

    public int Id { get => id; set => id = value; }
    public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }

    public EditarTableroViewModel(){}

    public EditarTableroViewModel(Tablero tablero){
        id = tablero.Id;
        idUsuarioPropietario = tablero.IdUsuarioPropietario;
        nombre = tablero.Nombre;
        descripcion = tablero.Descripcion;
    }

    public EditarTableroViewModel(Tablero tablero, List<Usuario> listaUsuarios){
        id = tablero.Id;
        idUsuarioPropietario = tablero.IdUsuarioPropietario;
        nombre = tablero.Nombre;
        descripcion = tablero.Descripcion;
        usuarios = listaUsuarios;
    }
}