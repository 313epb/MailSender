using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mail_Sender.Validation
{
    class EmailValidation:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex reg=new Regex("^[-._a-z0-9]+@(?:[a-z0-9][-a-z0-9]+\\.)+[a-z]{2,6}$");
            if (!reg.IsMatch(Convert.ToString(value)))
                return new ValidationResult(false, "Введите корректный почтовый адрес");
            return ValidationResult.ValidResult;
        }
    }
}
