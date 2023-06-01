namespace BodegonSemillas.Models
{
    public class VistaCarrito
    {
        public int? TotalProductos { get; set; }
        public decimal? TotalCarrito { get; set; }
        public List<VistaProducto>? Productos { get; set; }

        public VistaCarrito()
        {
            TotalCarrito = 0;
        }
    }
}
