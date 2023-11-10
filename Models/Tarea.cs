namespace espacioKanban;

public enum estadoTarea{
    ToDo,
    Doing,
    Review,
    Done
}
public class Tarea{
    private int id;
    private int id_tablero;
    private string nombre;
    private estadoTarea estado;
    private string descripcion;
    private string color;
    private int idUsuarioAsignado;

    public int Id { get => id; set => id = value; }
    public int Id_tablero { get => id_tablero; set => id_tablero = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public estadoTarea Estado { get => estado; set => estado = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string Color { get => color; set => color = value; }
    public int IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
}