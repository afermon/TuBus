using System;

namespace Entities
{
    public class Horario : BaseEntity
    {
        public int HorarioId { get; set; }
        public int RutaId { get; set; }
        public int Dia { get; set; }
        public TimeSpan Hora { get; set; }
    }
}