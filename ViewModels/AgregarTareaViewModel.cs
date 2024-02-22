using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using espacioKanban;
namespace espacioViewModels;

public class AgregarTareaViewModel{
    private int id;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id")]
    public int Id { get => id; set => id = value; }

    private int idTablero;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id Tablero")]
    public int IdTablero { get => idTablero; set => idTablero = value; }

    private string nombre;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Nombre")]
    public string Nombre { get => nombre; set => nombre = value; }

    private estadoTarea estado;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Estado")]
    public estadoTarea Estado { get => estado; set => estado = value; }

    private string? descripcion;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Descripcion")]
    public string Descripcion { get => descripcion; set => descripcion = value; }

    private string? color;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Color")]
    public string Color { get => color; set => color = value; }

    private int idUsuarioAsignado;
    [Required(ErrorMessage = "Campo requerido")]
    [Display(Name = "Id Usuario Asignado")]
    public int IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }

    private List<Usuario> usuarios;
    [Display(Name = "Usuarios")]
    public List<Usuario> Usuarios { get => usuarios; set => usuarios = value; }

    private List<Tablero> tableros;
    [Display(Name = "Tablero")]
    public List<Tablero> Tableros { get => tableros; set => tableros = value; }

    public AgregarTareaViewModel(){}

    public AgregarTareaViewModel(int idUsuario){
        idUsuarioAsignado = idUsuario;
    }

    public AgregarTareaViewModel(List<Tablero> listaTableros, List<Usuario> listaUsuarios){
        usuarios = listaUsuarios;
        tableros = listaTableros;
    }

}