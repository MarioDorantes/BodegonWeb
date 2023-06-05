namespace BodegonSemillas.Models
{
    public class VistaDireccion
    {
        public int Iddireccion { get; set; }

        public int Idcliente { get; set; }

        public string? NombreCompleto { get; set; }

        public string? Pais { get; set; }

        public string? Estado { get; set; }

        public string? Colonia { get; set; }

        public string? NombreCalle { get; set; }

        public string? Ciudad { get; set; }

        public string? NumeroInt { get; set; }

        public string? NumeroExt { get; set; }

        public string? Cp { get; set; }
        public ulong? Seleccionada { get; set; }

        public string? NotasDireccion { get; set; }
        public ulong? EstadoActivo { get; set; }
    }
}
