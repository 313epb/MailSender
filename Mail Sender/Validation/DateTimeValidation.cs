using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mail_Sender.Validation
{
    class DateTimeValidation:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                Convert.ToDateTime(value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false,"Введите корректную дату и время в формате MM:DD:YY HH:MM AM/PM");
            }
            return ValidationResult.ValidResult;
        }
    }
}
