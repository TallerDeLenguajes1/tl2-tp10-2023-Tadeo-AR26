using espacioKanban;
namespace espacioRepositories;

public interface ITableroRepository{
    public List<Tablero> GetAllTableros();
    public Tablero GetTableroByID(int id);
    public Tablero CreateTablero(Tablero tablero);
    public bool RemoveTablero(int id);
    public void UpdateTablero(Tablero tablero);
    public List<Tablero> GetAllTablerosFromUser(int idUsuario);
    public void RemoveTableroFromUser(int idUsuario);
}