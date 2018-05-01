using System;

namespace Entities
{
    public class Sancion  : BaseEntity
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Multa { get; set; }
        public string Estado { get; set; }
        public int TerminalId { get; set; }
        public DateTime Fecha { get; set; }
        public string Suspencion { get; set; }
        public DateTime FechaReactivacion { get; set; }
        public int Empresa { get; set; }
        public string NombreEmpresa { get; set; }
    }
}
