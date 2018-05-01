using System;
namespace Entities
{
    public class Recorrido : BaseEntity
    {
        public int RecorridoId { get; set; }
        public string BusPlaca { get; set; }
        public int RutaId { get; set; }
        public int CantidadPasajeros { get; set; }
        public string ChoferCedula { get; set; }
        public TimeSpan Horario { get; set; }
        public DateTime HoraSalida { get; set; }
        public int MinutosTarde { get; set; }
        public DateTime HoraLlegada { get; set; }
        public Posicion Posicion { get; set; }
    }
}
