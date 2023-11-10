using System.Data.SQLite;
using espacioKanban;
namespace espacioRepositories;

public class UsuarioRepository : IUsuarioRepository{
    private string cadenaConexion = "Data source=DB/kanban.db;Cache=Shared";

    public Usuario CreateUsuario(Usuario usuario){
        var queryString = @"Insert INTO usuario (nombre_de_usuario) VALUES(@nombre);";
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            var command = new SQLiteCommand(queryString, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@nombre", usuario.NombreUsuario));
            command.ExecuteNonQuery();
            connection.Close();
        }
        return usuario;
    }

    public List<Usuario> GetAllUsuarios(){
        var queryString = @"SELECT * FROM usuario;";
        List<Usuario> usuarios = new List<Usuario>();
        
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            var command = new SQLiteCommand(queryString, connection);
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    var user = new Usuario();
                    user.Id = Convert.ToInt32(reader["id_usuario"]);
                    user.NombreUsuario = reader["nombre_de_usuario"].ToString();
                    usuarios.Add(user);
                }
            }
            connection.Close();
        }
        return usuarios;
    }

    public Usuario GetUsuarioByID(int id){
        SQLiteConnection connection = new SQLiteConnection(cadenaConexion);
        var user = new Usuario();
        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM usuario WHERE id_usuario = @id;";
        command.Parameters.Add(new SQLiteParameter("@id", id));

        connection.Open();
        using(SQLiteDataReader reader = command.ExecuteReader()){
            while(reader.Read()){
                user.Id = Convert.ToInt32(reader["id_usuario"]);
                user.NombreUsuario = reader["nombre_de_usuario"].ToString();
            }
        }
        connection.Close();
        return user;
    }
    
    public bool RemoveUsuario(int id){
        bool result = false;
        var queryString = @"DELETE FROM usuario WHERE id_usuario = @id;";
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            var command = new SQLiteCommand(queryString, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@id", id));
            command.ExecuteNonQuery();
            connection.Close();
            result = true;
        }
        return result;
    }

    public Usuario UpdateUsuario(Usuario usuario, int id){
        var queryString = @"UPDATE usuario SET nombre_de_usuario = @nombre WHERE id_usuario = @id;";
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            var command = new SQLiteCommand(queryString, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@nombre", usuario.NombreUsuario));
            command.Parameters.Add(new SQLiteParameter("@id", id));
            command.ExecuteNonQuery();
            connection.Close();
        }
        return usuario;
    }
}