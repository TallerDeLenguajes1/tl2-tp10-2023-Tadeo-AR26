namespace espacioKanban;

public class Usuario{
    private int id;
    private string nombreUsuario;
    public int Id { get => id; set => id = value; }
    public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }

    public static Usuario FromAgregarUsuarioViewModel(CrearUsuarioViewModel usuarioVM){
        return new Usuario{
            nombre = usuarioVM.Nombre,
            id = usuarioVM.Id
        };
    }

    public static Usuario FromEditarUsuarioViewModel(EditarUsuarioViewModel usuarioVM){
        return new Usuario{
            nombre = usuarioVM.Nombre,
            id = usuarioVM.Id
        };
    }
}