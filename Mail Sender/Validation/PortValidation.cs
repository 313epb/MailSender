using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mail_Sender.Validation
{
    class PortValidation:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int port;
            try
            {
                port = Convert.ToInt32(value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false,"Вводите только цифры");
            }

            if ((port<100)&&(port>999)) return new ValidationResult(false,"Введите корректное значение порта");
            return ValidationResult.ValidResult;
        }
    }
}
