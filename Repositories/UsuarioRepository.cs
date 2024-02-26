using System.Data.SQLite;
using espacioKanban;
namespace espacioRepositories;

public class UsuarioRepository : IUsuarioRepository{
    private readonly string _cadenaConexion;

    public UsuarioRepository(string cadenaConexion){
        _cadenaConexion = cadenaConexion;
    }

    public Usuario CreateUsuario(Usuario usuario){
        var queryString = @"Insert INTO usuario (nombre_de_usuario, contrasenia, rol) VALUES(@nombre, @contrasenia, @rol);";
        using(SQLiteConnection connection = new SQLiteConnection(_cadenaConexion)){
            try{
                var command = new SQLiteCommand(queryString, connection);
                connection.Open();
                command.Parameters.Add(new SQLiteParameter("@nombre", usuario.NombreUsuario));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", usuario.Contrasenia));
                command.Parameters.Add(new SQLiteParameter("@rol", Convert.ToInt32(usuario.Rol)));
                command.ExecuteNonQuery();
            }
            catch(Exception ex){
                throw new Exception("Error: " + ex.Message, ex);
            }
            finally{
                connection.Close();
            }
        }
        return usuario;
    }

    public List<Usuario> GetAllUsuarios(){
        var queryString = @"SELECT * FROM usuario;";
        List<Usuario> usuarios = new List<Usuario>();

        using(SQLiteConnection connection = new SQLiteConnection(_cadenaConexion)){
            try{
                var command = new SQLiteCommand(queryString, connection);
                connection.Open();
                using(SQLiteDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        var user = new Usuario();
                        user.Id = Convert.ToInt32(reader["id_usuario"]);
                        user.NombreUsuario = reader["nombre_de_usuario"].ToString();
                        user.Contrasenia = reader["contrasenia"].ToString();
                        user.Rol = (Roles)Convert.ToInt32(reader["rol"]);
                        usuarios.Add(user);
                    }
                }
            }
            catch(Exception ex){
                throw new Exception("Error: " + ex.Message, ex);
            }
            finally{
                connection.Close();
            }
        }
        return usuarios;
    }

    public Usuario GetUsuarioByID(int id){
        SQLiteConnection connection = new SQLiteConnection(_cadenaConexion);
        var user = new Usuario();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM usuario WHERE id_usuario = @id;";
        command.Parameters.Add(new SQLiteParameter("@id", id));

        connection.Open();
        using(SQLiteDataReader reader = command.ExecuteReader()){
            while(reader.Read()){
                user.Id = Convert.ToInt32(reader["id_usuario"]);
                user.NombreUsuario = reader["nombre_de_usuario"].ToString();
                user.Contrasenia = reader["contrasenia"].ToString();
                user.Rol = (Roles)Convert.ToInt32(reader["rol"]);
            }
        }
        connection.Close();
        return user;
    }
    
    public bool RemoveUsuario(int id){
        bool result = false;
        var queryString = @"DELETE FROM usuario WHERE id_usuario = @id;";
        using(SQLiteConnection connection = new SQLiteConnection(_cadenaConexion)){
            try{
                var command = new SQLiteCommand(queryString, connection);
                connection.Open();
                command.Parameters.Add(new SQLiteParameter("@id", id));
                command.ExecuteNonQuery();
            }
            catch(Exception ex){
                throw new Exception("Error: " + ex.Message, ex);
            }
            finally{
                connection.Close();
            }
            result = true;
        }
        return result;
    }

    public Usuario UpdateUsuario(Usuario usuario){
        var queryString = @"UPDATE usuario SET nombre_de_usuario = @nombre, contrasenia = @contrasenia,
        rol = @rol WHERE id_usuario = @id;";
        using(SQLiteConnection connection = new SQLiteConnection(_cadenaConexion)){
            try{
                var command = new SQLiteCommand(queryString, connection);
                connection.Open();
                command.Parameters.Add(new SQLiteParameter("@nombre", usuario.NombreUsuario));
                command.Parameters.Add(new SQLiteParameter("@id", usuario.Id));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", usuario.Contrasenia));
                command.Parameters.Add(new SQLiteParameter("@rol", Convert.ToInt32(usuario.Rol)));
                command.ExecuteNonQuery();
            }
            catch(Exception ex){
                throw new Exception("Error: " + ex.Message, ex);
            }
            finally{
                connection.Close();
            }
        }
        return usuario;
    }

    public bool ExisteUsuario(string nombre){
        var queryString = @"SELECT COUNT(*) FROM usuario WHERE nombre_de_usuario = @nombre;";
        using(SQLiteConnection connection = new SQLiteConnection(_cadenaConexion)){
            try{
                var command = new SQLiteCommand(queryString, connection);
                connection.Open();
                command.Parameters.Add(new SQLiteParameter("@nombre", nombre));
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
            catch(Exception ex){
                throw new Exception("Error: " + ex.Message, ex);
            }
            finally{
                connection.Close();
            }
        }
    }
}