using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace espacioViewModels;

public class LoginViewModel{
    private string nombre;
    [Required(ErrorMessage = "Campo obligatorio")]
    [Display(Name = "Nombre de usuario")]
    public string Nombre { get => nombre; set => nombre = value; }

    private string contrasenia;
    [Required(ErrorMessage = "Campo obligatorio")]
    [PasswordPropertyText]
    [Display(Name = "Contraseña")]
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }

}