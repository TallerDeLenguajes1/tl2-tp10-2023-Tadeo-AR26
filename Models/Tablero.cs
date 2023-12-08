using espacioViewModels;

namespace espacioKanban;

public class Tablero{
    private int id;
    private int idUsuarioPropietario;
    private string nombre;
    private string descripcion;
    public int Id { get => id; set => id = value; }
    public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }

    
    public static Tablero FromAgregarUsuarioViewModel(AgregarTableroViewModel tableroVM){
        return new Tablero{
            id = tableroVM.Id,
            idUsuarioPropietario = tableroVM.IdUsuarioPropietario,
            nombre = tableroVM.Nombre,
            descripcion = tableroVM.Descripcion,
        };
    }

    public static Tablero FromEditarUsuarioViewModel(EditarTableroViewModel tableroVM){
        return new Tablero{
            id = tableroVM.Id,
            idUsuarioPropietario = tableroVM.IdUsuarioPropietario,
            nombre = tableroVM.Nombre,
            descripcion = tableroVM.Descripcion,
        };
    }
}