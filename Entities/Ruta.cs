using System.Collections.Generic;

namespace Entities
{
    public class Ruta : BaseEntity
    {
        public int Id { get; set; }
        public string RutaName { get; set; }
        public string RutaDescripcion { get; set; }
        public string JsonRoute { get; set; }
        public double DestinoLatitude { get; set; }
        public double DestinoLongitude { get; set; }
        public double Distancia { get; set; }
        public int LineaId { get; set; }
        public int TerminalId { get; set; }
        public int EmpresaId { get; set; }
        public int TarifaId { get; set;}
        public double CostoKm { get; set; }
        public double CostoTotal { get; set; }
        public string Estado { get; set; }
        public List<Horario> Horarios { get; set; }
    }
}
