using espacioKanban;
namespace espacioRepositories;

public interface ITareaRepository{
    public List<Tarea> GetAllTareas();
    public Tarea GetTareaById(int id);
    public Tarea CreateTarea(Tarea tarea);
    public bool RemoveTarea(int id);
    public Tarea UpdateTarea(Tarea tarea);
    public List<Tarea> GetAllTareasFromUser(int id);
    public List<Tarea> GetAllTareasFromTablero(int id);
    public bool AssingTask(int idUsuario, int idTarea);
    public int cantidadDeTareasEnEstado(estadoTarea estado);
}