using System;

namespace APIMobileEndpoint.Models
{
    public class Metrique
    {
        public int id { get; set; }
        public int equipmentId { get; set; }
        public string movementTime { get; set; }
        public string position { get; set; }
        public DateTime datetime { get; set; }
    }
}
