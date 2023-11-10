using System.Data.SQLite;
using espacioKanban;
namespace espacioRepositories;

public class TableroRepository : ITableroRepository{
    private string cadenaConexion = "Data source=DB/kanban.db;Cache=Shared";

    public Tablero CreateTablero(Tablero tablero){
        var queryString = @"INSERT INTO tablero (id_usuario_propietario, nombre, descripcion)
        VALUES(@id_user, @nombre, @descripcion);";
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            var command = new SQLiteCommand(queryString, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@id_user", tablero.IdUsuarioPropietario));
            command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
            command.ExecuteNonQuery();
            connection.Close();
        }
        return tablero;
    }

    public List<Tablero> GetAllTableros(){
        var queryString = @"SELECT * FROM tablero";
        var listaTableros = new List<Tablero>();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = new SQLiteCommand(queryString, connection);
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                var tablero = new Tablero();
                while(reader.Read()){
                    tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                    listaTableros.Add(tablero);
                }
            }
            connection.Close();
        }
        return listaTableros;
    }

    public List<Tablero> GetAllTablerosFromUser(int idUsuario){
        var queryString = @"SELECT * FROM tablero WHERE id_usuario_propietario = @idUser;";
        var listaTableros = new List<Tablero>();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = new SQLiteCommand(queryString, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@idUser", idUsuario));
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    var tablero = new Tablero();
                    tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                    listaTableros.Add(tablero);
                }
            }
            connection.Close();
        } 
        return listaTableros;
    }

    public Tablero GetTableroByID(int id){
        var queryString = @"SELECT * FROM tablero WHERE id_tablero = @idTablero;";
        var tablero = new Tablero();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = new SQLiteCommand(queryString, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@idTablero", id));
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();
                }
            }
            connection.Close();
        }
        return tablero;
    }

    public bool RemoveTablero(int id){
        var result = false;
        var queryString = @"DELETE FROM tablero WHERE id_tablero = @id;";
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

    public Tablero UpdateTablero(Tablero tablero){
        var queryString = @"UPDATE tablero
        SET id_usuario_propietario = @id, nombre = @nombre, descripcion = @descripcion
        WHERE id_tablero = @id_tablero;";
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            var command = new SQLiteCommand(queryString, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@id", tablero.IdUsuarioPropietario));
            command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@id_tablero", tablero.Id));
            connection.Close();
        }
        return tablero;
    }

}