using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Security
{
    public static class SessionSecurity
    {
        public static bool ActiveSession(object obj)
        {
            //validamos si hay una persona logueada
            // 
            User user = obj != null ? (User)obj : null;
            if (user != null && user.UserId != 0)
                return true;
            else
                return false;
        }


        public static bool isAdmin(object obj)
        {
            User user = obj != null ? (User)obj : null;
            return user != null ? user.Admin : false;
        }
    }
}
