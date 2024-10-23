using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        public int Id { get; set; }
        public int Dni { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Passsword { get; set; }
        public string UrlProfileImg { get; set; }
        public bool Admin { get; set; }
        public string CellPhone { get; set; }
        public Adress Adress { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime SignInDate { get; set;}
        public bool status { get; set; } //si ponemos esto tenemos que manejar la opcion de darse de baja temporalmente y recuperar la cuenta
    }
}
