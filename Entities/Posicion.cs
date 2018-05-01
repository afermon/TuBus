using System;

namespace Entities
{
    public class Posicion : BaseEntity
    {
        public int RecorridoId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
