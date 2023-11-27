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

    public static EditarUsuarioViewModel FromUsuario(Usuario usuario){
        return new EditarUsuarioViewModel{
            nombreUsuario = usuario.NombreUsuario,
            id = usuario.Id
        };
    }
}