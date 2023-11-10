using System.Data.SQLite;
using espacioKanban;
namespace espacioRepositories;

public class TareaRepository : ITareaRepository{
    private string cadenaConexion = "Data source=DB/kanban.db;Cache=Shared";

    public Tarea CreateTarea(Tarea tarea){
        var queryString = @"INSERT INTO tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado)
        VALUES(@id_tablero, @nombre, @estado, @descripcion, @color, @idUser);";
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            var command = new SQLiteCommand(queryString, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@idUser", tarea.IdUsuarioAsignado));
            command.Parameters.Add(new SQLiteParameter("@id_tablero", tarea.Id_tablero));
            command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
            command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));                
            command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
            command.ExecuteNonQuery();
            connection.Close();
        }
        return tarea;
    }

    public List<Tarea> GetAllTareasFromTablero(int id){
        var queryString = @"SELECT * FROM tarea WHERE id_tablero = @idTablero;";
        var listaTareas = new List<Tarea>();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = new SQLiteCommand(queryString, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@idTablero", id));
            using(SQLiteDataReader reader = command.ExecuteReader()){
                var tarea = new Tarea();
                while(reader.Read()){
                    tarea.Id = Convert.ToInt32(reader["id_tarea"]);
                    tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    if(Enum.TryParse(typeof(estadoTarea), reader["estadoTarea"].ToString(), out var estado)){
                        tarea.Estado = (estadoTarea)estado;
                    }
                    else{
                        tarea.Estado = estadoTarea.ToDo;
                    }
                    listaTareas.Add(tarea);
                }
            }
            connection.Close();
        }
        return listaTareas;
    }

    public List<Tarea> GetAllTareas(){
        var queryString = @"SELECT * FROM tarea;";
        var listaTareas = new List<Tarea>();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = new SQLiteCommand(queryString, connection);
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                var tarea = new Tarea();
                while(reader.Read()){
                    tarea.Id = Convert.ToInt32(reader["id_tarea"]);
                    tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    if(Enum.TryParse(typeof(estadoTarea), reader["estadoTarea"].ToString(), out var estado)){
                        tarea.Estado = (estadoTarea)estado;
                    }
                    else{
                        tarea.Estado = estadoTarea.ToDo;
                    }
                    listaTareas.Add(tarea);
                }
            }
            connection.Close();
        }
        return listaTareas;
    }

    public List<Tarea> GetAllTareasFromUser(int id){
        var queryString = @"SELECT * FROM tarea WHERE id_usuario_asignado = @idUser;";
        var listaTareas = new List<Tarea>();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = new SQLiteCommand(queryString, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@idUser", id));
            using(SQLiteDataReader reader = command.ExecuteReader()){
                var tarea = new Tarea();
                while(reader.Read()){
                    tarea.Id = Convert.ToInt32(reader["id_tarea"]);
                    tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    if(Enum.TryParse(typeof(estadoTarea), reader["estadoTarea"].ToString(), out var estado)){
                        tarea.Estado = (estadoTarea)estado;
                    }
                    else{
                        tarea.Estado = estadoTarea.ToDo;
                    }
                    listaTareas.Add(tarea);
                }
            }
            connection.Close();
        }
        return listaTareas;
    }

    public Tarea GetTareaById(int id){
        var queryString = @"SELECT * FROM tarea WHERE id_tarea = @idTarea;";
        var tarea = new Tarea();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = new SQLiteCommand(queryString, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@idTarea", id));
            using(SQLiteDataReader reader = command.ExecuteReader()){
                while(reader.Read()){
                    tarea.Id = Convert.ToInt32(reader["id_tarea"]);
                    tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    if(Enum.TryParse(typeof(estadoTarea), reader["estadoTarea"].ToString(), out var estado)){
                        tarea.Estado = (estadoTarea)estado;
                    }
                    else{
                        tarea.Estado = estadoTarea.ToDo;
                    }
                }
            }
            connection.Close();
        }
        return tarea;
    }

    public bool RemoveTarea(int id){
        var result = false;
        var queryString = @"DELETE FROM tarea WHERE id_tarea = @id;";
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

    public int cantidadDeTareasEnEstado(estadoTarea estado){
        var queryString = @"SELECT * FROM tarea
        WHERE estado = @estado;";
        var listaTareas = new List<Tarea>();
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            SQLiteCommand command = new SQLiteCommand(queryString, connection);
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader()){
                var tarea = new Tarea();
                while(reader.Read()){
                    tarea.Id = Convert.ToInt32(reader["id_tarea"]);
                    tarea.Id_tablero = Convert.ToInt32(reader["id_tablero"]);
                    tarea.Nombre = reader["nombre"].ToString();
                    tarea.Descripcion = reader["descripcion"].ToString();
                    tarea.Color = reader["color"].ToString();
                    tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                    if(Enum.TryParse(typeof(estadoTarea), reader["estadoTarea"].ToString(), out var est)){
                        tarea.Estado = (estadoTarea)est;
                    }
                    else{
                        tarea.Estado = estadoTarea.ToDo;
                    }
                    listaTareas.Add(tarea);
                }
            }
            connection.Close();
        }
        return listaTareas.Count;
    }

    public Tarea UpdateTarea(Tarea tarea){
        var queryString = @"UPDATE tarea SET id_tablero = @idTablero, nombre = @nombre,
        estado = @estado, descripcion = @descripcion, color = @color, id_usuario_asignado = @idUser
        WHERE id_tarea = @id;";
        using(SQLiteConnection connection = new SQLiteConnection(cadenaConexion)){
            var command = new SQLiteCommand(queryString, connection);
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@id", tarea.Id));
            command.Parameters.Add(new SQLiteParameter("@idUser", tarea.IdUsuarioAsignado));
            command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
            command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
            command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@idTablero", tarea.Id_tablero));
            command.ExecuteNonQuery();
            connection.Close();
        }
        return tarea;
    }
    
    public bool AssingTask(int idUsuario, int idTarea){
        throw new NotImplementedException();
    }
}