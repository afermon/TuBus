using System;

namespace Entities
{
    public class Transaccion : BaseEntity
    {
        public int TransactionId { get; set; }

        public int Charge { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public string CardUniqueCode { get; set;}

        public int LineaId { get; set; }

        public Terminal Terminal { get; set; }

        public Usuario Usuario { get; set; }

        public string Fecha { get; set; }

        public Linea Linea { get; set; }
    }

}
