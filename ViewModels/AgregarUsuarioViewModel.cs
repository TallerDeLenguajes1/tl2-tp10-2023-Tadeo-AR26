using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using espacioKanban;
namespace espacioViewModels;

public class AgregarUsuarioViewModel{
    private int id;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id")]
    public int Id { get => id; set => id = value; }
    
    private string nombreUsuario;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Nombre")]
    public string Nombre { get => nombreUsuario; set => nombreUsuario = value; }

}