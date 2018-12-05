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
    public class UsernameValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!string.IsNullOrEmpty((string)value))
            {
                if (value != null && Regex.IsMatch((string)value, @"^\w+(\.\w+)?(\-\w+)?$") && ((string) value).Length >= 5 && ((string)value).Length <= 20)
                {

                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, "Bitte beachten! 5-20 Zeichen, A-z, 0-9, -.");
                }
            }
            else
            {
                return new ValidationResult(true, "Pflichtfeld");
            }
        }

    }
}
