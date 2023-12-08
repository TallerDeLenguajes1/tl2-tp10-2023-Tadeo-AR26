namespace espacioKanban;
using espacioViewModels;

public enum Roles{
    admin,
    simple
}

public class Usuario{
    private int id;
    private string nombreUsuario;
    private string contrasenia;
    private Roles rol;
    public int Id { get => id; set => id = value; }
    public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
    public Roles Rol { get => rol; set => rol = value; }

    public Usuario(){}

    public Usuario(AgregarUsuarioViewModel usuarioVM){
            nombreUsuario = usuarioVM.Nombre;
            contrasenia = usuarioVM.Contrasenia;
            rol = usuarioVM.Rol;
    }

    public Usuario(EditarUsuarioViewModel usuarioVM){
            NombreUsuario = usuarioVM.Nombre;
            Contrasenia = usuarioVM.Contrasenia;
            Rol = usuarioVM.Rol;
    }
}