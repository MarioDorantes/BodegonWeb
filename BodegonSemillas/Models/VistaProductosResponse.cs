namespace BodegonSemillas.Models
{
    //Clase creada para manejar la respuesta a las propiedades adicionales del JSON de respuesta en vistaProductos
    public class VistaProductosResponse
    {
        public int TotalProductos { get; set; }
        public List<VistaProducto>? Products { get; set; }
    }
}
