namespace Entities
{
    public class AppClaim : BaseEntity
    {
        public string AppClaimId { get; set; }
        public string Type { get; set; }
        public string ApiController { get; set; }
        public string ApiContollerAction { get; set; }
    }
}