using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        public int UserId { get; set; }
        public bool firstAccess { get; set; }
        public string Dni { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string Mobile { get; set; }
        public int AddressId { get; set; }

        public Adress Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public bool Admin { get; set; }
        public string ResetToken { get; set; }
        public DateTime TokenExpiration { get; set; }
        public bool Active { get; set; }
        public bool Owner { get; set; }
        public Cart Cart { get; set; }
    }
}
