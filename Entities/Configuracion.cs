namespace Entities
{
    public class Configuracion : BaseEntity
    {
        public int DiasExpiracionContrasena { get; set; }
        public int CantCaracteresContrasena { get; set; }
        public int CantContrasenasAnteriores { get; set; }
    }
}