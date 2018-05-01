using System.Collections.Generic;

namespace Entities
{
    public class Vista : BaseEntity
    {
        public string VistaId { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Url { get; set; }

        public string Icon { get; set; }

        public string AppController { get; set; }

        public string AppControllerAction { get; set; }

        public List<AppClaim> AppClaims { get; set; }

        public bool ShowMenu { get; set; }

        public string RoleId { get; set; }
    }
}