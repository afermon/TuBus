using System.Collections.Generic;

namespace Entities
{
    public class Role : BaseEntity
    {
        public string RoleId { get; set; }
        public string Descripcion { get; set; }
        public string Homepage { get; set; }
        public List<Vista> Vistas { get; set; }
        public string Estado { get; set; }
    }
} 