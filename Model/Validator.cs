using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model
{
    public static class Validator
    {
        public static bool IsOnlyNumbers(string input)
        {

            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            decimal value;
            return decimal.TryParse(input, out value);  

        }

        public static bool IsEmpty(string input)
        {

            if (string.IsNullOrEmpty(input))
            {
                return string.IsNullOrEmpty(input);
            }

            if(input.Trim().Length == 0)
            {
                return true;
            }


            return false;
        }

        public static bool IsBiggerThanZero(string input)
        {
            int value = int.Parse(input);

            return value >= 0;
        }

    }
}
