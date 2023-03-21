namespace BodegonSemillas.Services
{
    public class Credenciales
    {
        public Credenciales(string? correo, string? password)
        {
            Correo = correo;
            Password = password;
        }

        public string? Correo { get; set; }
        public string? Password { get; set; }
    }

}
