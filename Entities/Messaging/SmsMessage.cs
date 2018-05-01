using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Messaging
{
    /// <summary>Object for sending SMS </summary>
    public class SmsMessage : BaseEntity
    {
        public string DestinationNumber { get; set; }

        public string SourceNumber { get; set; }

        public string Message { get; set; }
    }
}
