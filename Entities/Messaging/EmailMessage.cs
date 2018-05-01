namespace Entities.Messaging
{
    /// <summary>Object used for sending Mail </summary>
    public class EmailMessage : BaseEntity
    {
        /// <summary>Destination User </summary>
        public string To { get; set; }
        
        /// <summary>Email Message </summary>
        public string Message { get; set; }
       
        /// <summary> Required for Initialize Cards </summary>
        public string Url { get; set; }

        /// <summary> Add UserName </summary>
        public string UserName { get; set; }

        /// <summary> Add EmpresaNombre </summary>
        public string EmpresaNombre { get; set; }
    }
}
