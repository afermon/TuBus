using System;

namespace Entities
{
    public class Terminal : BaseEntity
    {
        public int Id { get; set; }
        public string TerminalName { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int CantidadLineas { get; set; }
        public string Estado { get; set; }
        public int EspaciosParqueo { get; set; }
        public int EspaciosParqueoBus { get; set; }

        public Terminal()
        {

        }

        

    }
        
}
