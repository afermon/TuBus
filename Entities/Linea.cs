namespace Entities
{
    public class Linea : BaseEntity
    {
        public int LineaId { get; set; }

        public string NombreLinea { get; set; }

        public int EspaciosParqueo { get; set; }

        public string Estado { get; set; }

        public Terminal Terminal { get; set; }

        public Empresa Empresa { get; set; }
    }
}
