using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using espacioKanban;
namespace espacioViewModels;

public class EditarUsuarioViewModel{
    private int id;

    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Id")]
    public int Id { get => id; set => id = value; }
    
    private string nombreUsuario;
    
    [Required(ErrorMessage = "Este campo es requerido.")]
    [Display(Name = "Nuevo nombre de Usuario")]
    public string Nombre { get => nombreUsuario; set => nombreUsuario = value; }

    private string contrasenia;
    [Required(ErrorMessage = "Campo requerido")]
    [DataType(DataType.Password)]
    [Display(Name = "contrasenia")]
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }

    private Roles rol;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Rol")]
    public Roles Rol { get => rol; set => rol = value; }

    public EditarUsuarioViewModel(){}

    public EditarUsuarioViewModel(Usuario usuario){
            nombreUsuario = usuario.NombreUsuario;
            id = usuario.Id;
            contrasenia = usuario.Contrasenia;
            rol = usuario.Rol;
    }
}