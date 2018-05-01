using System;

namespace Entities
{
    public class RegistroParqueo : BaseEntity
    {
        public int RegistroId { get; set; }

        public int TerminalId { get; set; }

        public int CostoTotal { get; set; }

        public string CostoParqueoDia { get; set; }

        public string CostoParqueoHora { get; set; }

        public string TarjetaId { get; set; }

        public int EspaciosDisponibles { get; set; }

        public int EspaciosTotales { get; set; }

        public DateTime HoraInicio { get; set; }

        public DateTime HoraFin { get; set; }
    }
}
