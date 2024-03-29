using espacioViewModels;

namespace espacioKanban;

public enum estadoTarea{
    ToDo,
    Doing,
    Review,
    Done
}

public enum colorTarea{
    red,
    white,
    yellow,
    skyblue,
    green,
    black
}

public class Tarea{
    private int id;
    private int id_tablero;
    private string nombre;
    private estadoTarea estado;
    private string descripcion;
    private colorTarea color;
    private int idUsuarioAsignado;
    private string usuarioAsignado;
    private string tableroAsignado;

    public int Id { get => id; set => id = value; }
    public int Id_tablero { get => id_tablero; set => id_tablero = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public estadoTarea Estado { get => estado; set => estado = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public colorTarea Color { get => color; set => color = value; }
    public int IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
    public string UsuarioAsignado { get => usuarioAsignado; set => usuarioAsignado = value; }
    public string TableroAsignado { get => tableroAsignado; set => tableroAsignado = value; }


    public Tarea(){

    }
    
    public Tarea(EditarTareaViewModel tareaVM){
        id = tareaVM.Id;
        id_tablero = tareaVM.IdTablero;
        nombre = tareaVM.Nombre;
        descripcion = tareaVM.Descripcion;
        color = (colorTarea)tareaVM.Color;
        estado = (estadoTarea)tareaVM.Estado;
        idUsuarioAsignado = tareaVM.IdUsuarioAsignado;
    }

    public Tarea (AgregarTareaViewModel tareaVM){
        id = tareaVM.Id;
        id_tablero = tareaVM.IdTablero;
        nombre = tareaVM.Nombre;
        descripcion = tareaVM.Descripcion;
        color = (colorTarea)tareaVM.Color;
        estado = (estadoTarea)tareaVM.Estado;
        idUsuarioAsignado = tareaVM.IdUsuarioAsignado;
    }
}