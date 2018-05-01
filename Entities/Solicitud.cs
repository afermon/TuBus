using System.Collections.Generic;

namespace Entities
{
    public class Solicitud : BaseEntity
    {
        public string SolicitudId { get; set; }

        public string NombreUsuario { get; set; }

        public string EmailSolicitante { get; set; }

        public Terminal Terminal { get; set; }

        public Convenio Convenio { get; set; }

        public string ListEmails { get; set; }

        public List<string> Emails { get; set; }

        public string Estado { get; set; }
    }
}
