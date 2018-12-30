using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MailSender.Domain.Entities;

namespace Mail_Sender.Validation
{
    public class ValidationClass
    {
        public object syncObj= new object();

        public ValidationResult ValidateMail(Mail item)
        {
            ValidationResult res= new ValidationResult();
            res.result = true;
            res.errorList = new List<string>();

            lock (syncObj)
            {
                if (item == null)
                {
                    res.errorList.Add("Письмо is null");
                    res.result = false;
                }
                if (string.IsNullOrEmpty(item.Topic))
                {
                    res.errorList.Add("Тема письма не должна быть пустой.");
                    res.result = false;
                }
            }
            return res;
        }

        public ValidationResult ValidateReceiver(Receiver item)
        {
            ValidationResult res = new ValidationResult();
            res.result = true;
            res.errorList = new List<string>();
            Regex reg = new Regex("^[-._a-z0-9]+@(?:[a-z0-9][-a-z0-9]+\\.)+[a-z]{2,6}$");


            if (item == null)
            {
                res.errorList.Add($"{item.ClassName} не выбран");
                res.result = false;
            }

            if (!reg.IsMatch(item.Key))
            {
                res.errorList.Add($"Введите корректный {item.KeyName}");
                res.result = false;
            }

            return res;
        }

        public ValidationResult ValidateSender(Sender item)
        {
            ValidationResult res = new ValidationResult();
            res.result = true;
            res.errorList = new List<string>();
            Regex reg= new Regex("^[-._a-z0-9]+@(?:[a-z0-9][-a-z0-9]+\\.)+[a-z]{2,6}$");

            if (item == null)
            {
                res.errorList.Add($"{item.ClassName} не выбран");
                res.result = false;
            }

            if (!reg.IsMatch(item.Key))
            {
                res.errorList.Add($"Введите корректный {item.KeyName}");
                res.result = false;
            }


            if (item.Value.Length<6)
            {
                res.errorList.Add("Введенный пароль скорее всего неверен (менее 6ти символов).");
                res.result = false;
            }

            return res;
        }

        public ValidationResult ValidateSMTP(SMTP item)
        {
            ValidationResult res = new ValidationResult();
            res.result = true;
            res.errorList = new List<string>();
            Regex reg= new Regex("^[a-zA-Z0-9.!£#$%&'^_`{}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)*$");
            int port = 0;

            if (item == null)
            {
                res.errorList.Add($"{item.ClassName} не выбран");
                res.result = false;
            }

            if (!reg.IsMatch(item.Key))
            {
                res.errorList.Add($"Введите корректный {item.KeyName}");
                res.result = false;
            }

            if (!Int32.TryParse(item.Value, out port))
            {
                res.errorList.Add($"Введите корректный {item.ValueName}. Это должно быть число.");
                res.result = false;
            }

            if (port<100 || port>999)
            {
                res.errorList.Add($"Введите корректный {item.KeyName}. От 100 до 999");
                res.result = false;
            }
            return res;
        }
    }
}


