using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PC_Verwaltung
{
    public class Field_Validation
    {

        public static string validateName(string name)
        {
            string trimmed = name.Trim();
            
            if (Regex.IsMatch(trimmed, @"[a-zA-Z]+\S+"))
            {
                return trimmed;
            }
            else
            {
                return null;
            }
            
        }

    }
}
