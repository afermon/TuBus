using System;

namespace Entities
{
    public class HistorialContrasena : BaseEntity
    {
        public DateTime Fecha { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int Count { get; set; }
    }
}