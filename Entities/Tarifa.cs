namespace Entities
{
    public class Tarifa : BaseEntity
    {
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public string Operator { get; set; }
        public double RegularFare { get; set; }
    }
}
