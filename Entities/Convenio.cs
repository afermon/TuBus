using System.Collections.Generic;

namespace Entities
{
    public class Convenio : BaseEntity
    {
        public int CedulaJuridica { get; set; }

        public string NombreInstitucion { get; set; }

        public string EmailEncargado { get; set; }

        public int DescuentoTarifa { get; set; }

        public string Estado { get; set; }

        public Terminal Terminal { get; set; }

        public List<Empresa> Empresas { get; set; }

        public List<int> TerminalesId { get; set; }
    }
}
