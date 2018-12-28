using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Mail_Sender.Validation
{
    class TopicValidation:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if ((Convert.ToString(value)).Length==0) return new ValidationResult(false,"Тема не должна быть пустой");
            return ValidationResult.ValidResult;
        }
    }
}
