using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using espacioKanban;
namespace espacioViewModels;

public class AgregarUsuarioViewModel{
    
    private string nombreUsuario;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Nombre")]
    public string Nombre { get => nombreUsuario; set => nombreUsuario = value; }
    
    private string contrasenia;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Contraseña")]
    public string Contrasenia { get => nombreUsuario; set => nombreUsuario = value; }

    private Roles rol;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id")]
    public Roles Rol { get => rol; set => rol = value; }



}