namespace BodegonSemillas.Models
{
    public class Cliente
    {

        public int Id { get; set; }

        public string? Clave { get; set; }

        public string? Rfc { get; set; }

        public string? Paterno { get; set; }

        public string? Materno { get; set; }

        public string? Nombre { get; set; }

        public string? Empresa { get; set; }

        public string? Idpais { get; set; }

        public string? Idestado { get; set; }

        public string? NombreEstado { get; set; }

        public string? Idciudad { get; set; }

        public string? NombreCiudad { get; set; }

        public string? Colonia { get; set; }

        public string? Cp { get; set; }

        public string? Domicilio { get; set; }

        public string? NumExt { get; set; }

        public string? NumInt { get; set; }

        public string? Congregacion { get; set; }

        public string? IdtipoEntrega { get; set; }

        public string? NombreTipoEntrega { get; set; }

        public string? Idperiodo { get; set; }

        public string? NombrePeriodo { get; set; }

        public string? NombreGiro { get; set; }

        public ulong? PersonaFisica { get; set; }

        public string? Telefono { get; set; }

        public string? Banco { get; set; }

        public string? Cuenta { get; set; }

        public string? Clabe { get; set; }

        public string? Divisa { get; set; }

        public string? Iddivisa { get; set; }

        public string? Descripcion { get; set; }

        public int? Idsucursal { get; set; }

        public DateTime? FechaAlta { get; set; }

        public ulong? Ieps { get; set; }

        public string? Email { get; set; }

        public string? Email2 { get; set; }

        public string? NoLista { get; set; }

        public decimal? LimiteCredito { get; set; }

        public int? DiasCredito { get; set; }

        public decimal? SaldoDeudor { get; set; }

        public DateTime? Modificacion { get; set; }

        public decimal? SaldoAnticipos { get; set; }

        public int? Idlocal { get; set; }

        public int? Idmunicipio { get; set; }

        public int? Idlocalidad { get; set; }

        public int? Idcolonia { get; set; }

        public int? IdmunicipioSat { get; set; }

        public int? IdlocalidadSat { get; set; }

        public int? IdcoloniaSat { get; set; }

        public int? IdregimenFiscal { get; set; }

        public string? Password { get; set; }

    }
}
