using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Business
{
    public class PasswordHasher : IPasswordHasher
    {

        private static readonly PasswordHasher hasher = new PasswordHasher();

      
        public string HashPassword(string password)
        {
           
            var hasher = new Microsoft.AspNet.Identity.PasswordHasher();

            return hasher.HashPassword(password);
        }


     
        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var hasher = new Microsoft.AspNet.Identity.PasswordHasher();

            return hasher.VerifyHashedPassword(hashedPassword, providedPassword);
        }

    } 
}
