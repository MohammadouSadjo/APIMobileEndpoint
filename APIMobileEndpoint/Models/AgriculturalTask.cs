using System;

namespace APIMobileEndpoint.Models
{
    public class AgriculturalTask
    {
        public int id { get; set; }
        public int equipmentId { get; set; }
        public int intervenantId { get; set; }
        public string description { get; set; }
        public DateTime dateExecution { get; set; }
    }
}
