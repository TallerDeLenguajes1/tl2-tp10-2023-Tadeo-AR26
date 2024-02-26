using System.Data.SQLite;
using espacioKanban;
namespace espacioRepositories;

public class TableroRepository : ITableroRepository{
    private readonly string _cadenaConexion;

    public TableroRepository(string cadenaConexion){
        _cadenaConexion = cadenaConexion;
    }

    public Tablero CreateTablero(Tablero tablero){
        var queryString = @"INSERT INTO tablero (id_usuario_propietario, nombre, descripcion)
        VALUES(@id_user, @nombre, @descripcion);";
        using(SQLiteConnection connection = new SQLiteConnection(_cadenaConexion)){
            try{
                var command = new SQLiteCommand(queryString, connection);
                connection.Open();
                command.Parameters.Add(new SQLiteParameter("@id_user", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
                command.ExecuteNonQuery();
            }
            catch(Exception ex){
                throw new Exception("Error: " + ex.Message, ex);
            }
            finally{
                connection.Close();
            }
        }
        return tablero;
    }

    public List<Tablero> GetAllTableros(){
        var queryString = @"SELECT t.id_tablero, t.id_usuario_propietario, t.nombre, t.descripcion, u.nombre_de_usuario
                            FROM tablero t
                            INNER JOIN usuario u ON t.id_usuario_propietario = u.id_usuario;";
        var listaTableros = new List<Tablero>();
        using(SQLiteConnection connection = new SQLiteConnection(_cadenaConexion)){
            try{
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();
                using(SQLiteDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tablero.Propietario = reader["nombre_de_usuario"].ToString();
                        listaTableros.Add(tablero);
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
        return listaTableros;
    }


    public List<Tablero> GetAllTablerosFromUser(int idUsuario){
        var queryString = @"SELECT t.id_tablero, t.id_usuario_propietario, t.nombre, t.descripcion, u.nombre_de_usuario
                            FROM tablero t
                            INNER JOIN usuario u ON t.id_usuario_propietario = u.id_usuario
                            WHERE t.id_usuario_propietario = @idUser;";
        var listaTableros = new List<Tablero>();
        using (SQLiteConnection connection = new SQLiteConnection(_cadenaConexion)){
            try{
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter("@idUser", idUsuario));
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader()){
                    while (reader.Read()){
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tablero.Propietario = reader["nombre_de_usuario"].ToString();
                        listaTableros.Add(tablero);
                    }
                }
            }
            catch (Exception ex){
                throw new Exception("Error: " + ex.Message, ex);
            }
            finally{
                connection.Close();
            }
        }
        return listaTableros;
    }

    public Tablero GetTableroByID(int id){
        var queryString = @"SELECT t.id_tablero, t.id_usuario_propietario, t.nombre, t.descripcion, u.nombre_de_usuario
                            FROM tablero t
                            INNER JOIN usuario u ON t.id_usuario_propietario = u.id_usuario
                            WHERE id_tablero = @idTablero;";
        var tablero = new Tablero();
        using(SQLiteConnection connection = new SQLiteConnection(_cadenaConexion)){
            try{
                SQLiteCommand command = new SQLiteCommand(queryString, connection);
                connection.Open();
                command.Parameters.Add(new SQLiteParameter("@idTablero", id));
                using(SQLiteDataReader reader = command.ExecuteReader()){
                    while(reader.Read()){
                        tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tablero.Propietario = reader["nombre_de_usuario"].ToString();
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
        return tablero;
    }

    public bool RemoveTablero(int id){
        var result = false;
        var queryString = @"DELETE FROM tablero WHERE id_tablero = @id;";
        using(SQLiteConnection connection = new SQLiteConnection(_cadenaConexion)){
            try{
                var command = new SQLiteCommand(queryString, connection);
                connection.Open();
                command.Parameters.Add(new SQLiteParameter("@id", id));
                command.ExecuteNonQuery();
                result = true;
            }
            catch(Exception ex){
                throw new Exception("Error: " + ex.Message, ex);
            }
            finally{
                connection.Close();
            }
        }
        return result;
    }

    public void UpdateTablero(Tablero tablero){
        Console.WriteLine($"Id del tablero a editar: {tablero.Id}");
        var queryString = @"UPDATE tablero
        SET id_usuario_propietario = @id, nombre = @nombre, descripcion = @descripcion
        WHERE id_tablero = @id_tablero;";
        using(SQLiteConnection connection = new SQLiteConnection(_cadenaConexion)){
            try{
                var command = new SQLiteCommand(queryString, connection);
                connection.Open();
                command.Parameters.Add(new SQLiteParameter("@id", tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@id_tablero", tablero.Id));
                command.ExecuteNonQuery();
            }
            catch(Exception ex){
                throw new Exception("Error: " + ex.Message, ex);
            }
            finally{
                connection.Close();
            }
        }
    }

    public void RemoveTableroFromUser(int idUsuario){
        var queryString = @"DELETE FROM tablero WHERE id_usuario_propietario = @idUsuario;";
        using(SQLiteConnection connection = new SQLiteConnection(_cadenaConexion)){
            try{
                var command = new SQLiteCommand(queryString, connection);
                connection.Open();
                command.Parameters.Add(new SQLiteParameter("@idUsuario", idUsuario));
                command.ExecuteNonQuery();
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