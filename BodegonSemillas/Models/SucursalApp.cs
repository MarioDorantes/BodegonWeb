namespace BodegonSemillas.Models
{
    public class SucursalApp
    {
        public int Id { get; set; }
        public string? NombreSucursal { get; set; }
        public string? Domicilio { get; set; }
        public string? Telefono { get; set; }
        public string? Estado { get; set; }
        public string? Ciudad { get; set; }
        public string? CodigoPostal { get; set; }

        public bool SucursalSeleccionada { get; set; }
    }
}
