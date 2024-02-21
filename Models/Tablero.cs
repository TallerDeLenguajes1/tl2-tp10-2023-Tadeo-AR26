using espacioViewModels;

namespace espacioKanban;

public class Tablero{
    private int id;
    private int idUsuarioPropietario;
    private string nombre;
    private string descripcion;
    private string propietario;
    public int Id { get => id; set => id = value; }
    public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string Propietario { get => propietario; set => propietario = value; }

    
    public Tablero(){}

    public Tablero(AgregarTableroViewModel tableroVM){
            id = tableroVM.Id;
            idUsuarioPropietario = tableroVM.IdUsuarioPropietario;
            nombre = tableroVM.Nombre;
            descripcion = tableroVM.Descripcion;
    }

    public Tablero(EditarTableroViewModel tableroVM){
            id = tableroVM.Id;
            idUsuarioPropietario = tableroVM.IdUsuarioPropietario;
            nombre = tableroVM.Nombre;
            descripcion = tableroVM.Descripcion;
        }
}