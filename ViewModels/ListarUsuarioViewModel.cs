using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using espacioKanban;
namespace espacioViewModels;

public class ListarUsuarioViewModel{
    private int id;

    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id")]
    public int Id { get => id; set => id = value; }
    
    private string nombreUsuario;
    
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Nombre")]
    public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }

    public static List<ListarUsuarioViewModel> FromUsuario(List<Usuario> usuarios){
        List<ListarUsuarioViewModel> listaUsuariosVM = new List<ListarUsuarioViewModel>();
            foreach (var usuario in usuarios){
                ListarUsuarioViewModel newUVM = new ListarUsuarioViewModel();
                newUVM.id = usuario.Id;
                newUVM.nombreUsuario = usuario.NombreUsuario;
                listaUsuariosVM.Add(newUVM);
            }
            return(listaUsuariosVM);
    }
}