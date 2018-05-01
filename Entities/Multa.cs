using System;

namespace Entities
{
    public class Multa : BaseEntity
    {
        public int Id { get; set; }
        public int Empresa { get; set; }
        public int Monto { get; set; }
        public DateTime Fecha { get; set; }
        public string Detalle { get; set; }
        public string Estado { get; set; }
        public string NombreEmpresa { get; set; }
    }
}
