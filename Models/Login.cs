using espacioViewModels;
namespace espacioKanban;

public enum nivelDeAcceso{
    simple,
    admin
}

public class Login{
    private nivelDeAcceso nivel;
    private string nombre;
    private string contrasenia;
    
    public string Nombre { get => nombre; set => nombre = value; }
    public nivelDeAcceso Nivel { get => nivel; set => nivel = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }

    public Login(){}

    public Login(LoginViewModel loginViewModel){
        nombre = loginViewModel.Nombre;
        contrasenia = loginViewModel.Contrasenia;
    }
}