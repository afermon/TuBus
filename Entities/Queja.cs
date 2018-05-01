using System;

namespace Entities
{
    public class Queja : BaseEntity
    {
        public int Id { get; set; }
        public string DetalleQueja { get; set; }
        public int Ruta { get; set; }
        public string Chofer { get; set; }
        public string Placa { get; set; }
        public DateTime Hora { get; set; }
        public string Estado { get; set; }
    }
}
