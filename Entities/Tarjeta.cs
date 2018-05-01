namespace Entities
{
    public class Tarjeta : BaseEntity
    {
        public decimal SaldoDisponible { get; set; } 

        public string CodigoUnico { get; set; }

        public Terminal Terminal { get; set; }

        public Usuario Usuario { get; set; }

        public TipoTarjeta TipoTarjeta { get; set; }

        public  Convenio Convenio { get; set; }

        public EstadoTarjeta EstadoTarjeta { get; set; }

        public string NombreTarjeta { get; set; }
    }
}
