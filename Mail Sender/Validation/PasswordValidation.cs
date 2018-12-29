using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mail_Sender.Validation
{
    class PasswordValidation:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (Convert.ToString(value).Length<6) return new ValidationResult(false,"Пароль должен быть длиннее 5ти символов");
            return ValidationResult.ValidResult;
        }
    }
}
