namespace Entities
{
    public class TipoTarjeta : BaseEntity
    {
        public int TipoTarjetaId { get; set; }

        public string Nombre { get; set; }

        public string Estado { get; set; } 

        public int DiscountPercentage { get; set; }
    }
}
