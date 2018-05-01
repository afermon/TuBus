using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PagoPendiente : BaseEntity
    {
        public int IdPago { get; set; }

        public int LineaId { get; set; }

        public int Monto { get; set; }

        public int EmpresaId { get; set; }

        public string EstadoPago { get; set; }

        public string Fecha { get; set; }

        public string Descripcion { get; set; }

        public  Linea Linea { get; set; }
    }
}
