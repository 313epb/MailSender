using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mail_Sender.Validation
{
    class NameValidation:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (Convert.ToString(value).Length < 2)
                return new ValidationResult(false, "Имя не может быть короче 2х символов");
                return ValidationResult.ValidResult;
        }
    }
}
