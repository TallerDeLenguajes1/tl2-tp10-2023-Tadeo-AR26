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
    [PasswordPropertyText]
    [StringLength(16, MinimumLength = 6, ErrorMessage = "La contraseña debe contener entre 6 y 16 caracteres.")]
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }

    private Roles rol;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Rol")]
    public Roles Rol { get => rol; set => rol = value; }



}