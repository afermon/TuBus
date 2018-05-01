using System;

namespace Entities
{
    public class Usuario : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime PasswordLastSet { get; set; }
        public DateTime PasswordExpiration { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
        public string Estado { get; set; }
        public decimal? SmsNotificationsMin { get; set; }
        public int Telefono { get; set; }
        public string ResetToken { get; set; }
        public int? TerminalId { get; set; }
    }
}
