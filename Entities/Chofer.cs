using System;

namespace Entities
{
    public class Chofer : BaseEntity
    {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }
        public string NumeroLicencia { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public int Empresa { get; set; }
        public string Estado { get; set; }
        public string NombreCompleto
        {
            get
            {
                return Nombre + " " + Apellidos;
      
            }
        }
    }
}
