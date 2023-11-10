using espacioKanban;
namespace espacioRepositories;

public interface IUsuarioRepository{
    public List<Usuario> GetAllUsuarios();
    public Usuario GetUsuarioByID(int id);
    public Usuario CreateUsuario(Usuario usuario);
    public bool RemoveUsuario(int id);
    public Usuario UpdateUsuario(Usuario usuario, int id);
}