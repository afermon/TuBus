using System.Collections.Generic;

namespace Entities
{
    public class Report : BaseEntity
    {
        public string Label { get; set; }

        public int Value { get; set; }

        public string AdditionalValue { get; set; }
    }
}
