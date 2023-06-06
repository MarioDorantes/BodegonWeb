namespace BodegonSemillas.Models
{
    public class PedidoCarritoInfo
    {
        public int IdCliente { get; set; }
        public int IdSucursal { get; set; }
        public int IdDireccion { get; set; }

        public List<ProductoPedido> ProductosPedido { get; set; }
    }
}
