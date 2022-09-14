using System;

namespace APIMobileEndpoint.Models
{
    public class User
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string login { get; set; }
        public string mot_de_passe { get; set; }
        public string email { get; set; }
        public string role { get; set; }
    }
}
