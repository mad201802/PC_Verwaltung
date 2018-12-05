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
    public class EmailValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!string.IsNullOrEmpty((string)value))
            {
                if (value != null && Regex.IsMatch((string)value, ".+@.+\\..+"))
                {

                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, "Die Email stimmt nicht!");
                }
            }
            else
            {
                return new ValidationResult(true, "Pflichtfeld");
            }
        }

    }
}
