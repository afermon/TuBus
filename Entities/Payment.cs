namespace Entities
{
    public class Payment : BaseEntity
    {
        public string CardToken { get; set; }

        public int AmoutCrc { get; set; }

        public string Description { get; set; }

        public string UserCardUnicode { get; set; }

        public int LineaId { get; set; }

        public string UserMail { get; set; }

        public int IdPago { get; set; }
    }
}
