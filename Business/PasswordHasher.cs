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

        //Este método toma una contraseña en texto plano y devuelve su hash
        public string HashPassword(string password)
        {
            // Usa el PasswordHasher de ASP.NET Identity para hashear la contraseñ
            var hasher = new Microsoft.AspNet.Identity.PasswordHasher();

            return hasher.HashPassword(password);
        }


        ////Este método compara el hash de la contraseña almacenado con una contraseña
        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var hasher = new Microsoft.AspNet.Identity.PasswordHasher();

            return hasher.VerifyHashedPassword(hashedPassword, providedPassword);
        }

    } 
}
