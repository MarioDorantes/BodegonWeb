namespace BodegonSemillas.Models
{
    public class VistaProducto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal? IVA { get; set; }
        public decimal? IEPSPorcentaje { get; set; }
        public decimal? PrecioImpuestos { get; set; }
        public String? Medida { get; set; }
        public String? Marca { get; set; }
        public int IdSucursal { get; set; }
        public decimal? DescuentoMonto { get; set; }
        public decimal? DescuentoPorcentaje { get; set; }
        public int? IdProductoRegalo { get; set; }
        public int? IdPoliticaVenta { get; set; }

        public string? IdLinea { get; set; }
        public string? NombreLinea { get; set; }
        public int? IdPrecio { get; set; }

        public bool? enCarrito { get; set; }
        public int? CantidadCarrito { get; set; }
        public bool? enFavoritos { get; set; }

        public int? totalCarrito { get; set; }
        public decimal? Existencia { get; set; }
    }
}
