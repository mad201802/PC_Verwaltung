using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace ValidationRules
{
    public class NameValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!string.IsNullOrEmpty((string)value))
            {
                if (value != null && !((string)value).Contains("'") && !((string)value).Contains(" ") && ((string) value).Length >= 3)
                {

                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, "Falsche Eingabe");
                }
            }
            else
            {
                return new ValidationResult(true, "Pflichtfeld");
            }
        }

    }
}
