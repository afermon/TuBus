using System.Collections.Generic;

namespace Entities
{
    public class Bus : BaseEntity
    {
        public string Id { get; set; }

        public int CapacidadSentado { get; set; }

        public int CapacidadDePie { get; set; }

        public int AsientoDiscapacitado { get; set; }

        public string Estado { get; set; }

        public string Empresa { get; set; }

        public int EmpresaId { get; set; }

        public List<Requisito> Requisitos { get; set; }
    }
}
