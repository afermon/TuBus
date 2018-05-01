namespace Entities
{
    public class Empresa : BaseEntity
    {
        public int CedulaJuridica { get; set; }

        public string NombreEmpresa { get; set; }

        public string Estado { get; set; }

        public string EmailEncargado { get; set; }

        public string Telefono { get; set; }

        public string Rechazo { get; set; }

        public Usuario Usuario { get; set; }
    }
}
