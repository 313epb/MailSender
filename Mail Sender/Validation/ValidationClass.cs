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
        public ValidationResult ValidateMail(Mail item)
        {
            ValidationResult res= new ValidationResult();
            res.result = true;
            res.errorList = new List<string>();

            if (item == null)
            {
                res.errorList.Add("Письмо is null");
                res.result = false;
            }
            else
            {
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
            else
            {

                if (!reg.IsMatch(item.Key))
                {
                    res.errorList.Add($"Введите корректный {item.KeyName}");
                    res.result = false;
                }
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
            else
            {

                if (!reg.IsMatch(item.Key))
                {
                    res.errorList.Add($"Введите корректный {item.KeyName}");
                    res.result = false;
                }


                if (item.Value.Length < 6)
                {
                    res.errorList.Add("Введенный пароль скорее всего неверен (менее 6ти символов).");
                    res.result = false;
                }
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
            else
            {

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

                if (port < 100 || port > 999)
                {
                    res.errorList.Add($"Введите корректный {item.KeyName}. От 100 до 999");
                    res.result = false;
                }
            }

            return res;
        }

        public ValidationResult ValidateSended(Sended item)
        {
            ValidationResult res = new ValidationResult();
            res.result = true;
            res.errorList = new List<string>();

            if (item==null)
            {
                res.errorList.Add("Не передан экземпляр класса");
                res.result = false;
            }
            else
            {
                ValidationResult temp;
                temp = ValidateMail(item.Mail);
                if (!temp.result)
                {
                    foreach (string s in temp.errorList)
                    {
                        res.errorList.Add(s);
                    }

                    res.result = temp.result;
                }

                foreach (SendedReceiver receiver in item.SendedReceivers)
                {
                    temp = ValidateReceiver(receiver.Receiver);
                    if (!temp.result)
                    {
                        foreach (string s in temp.errorList)
                        {
                            res.errorList.Add(s);
                        }

                        res.result = temp.result;
                    }
                }

                temp = ValidateSender(item.Sender);
                if (!temp.result)
                {
                    foreach (string s in temp.errorList)
                    {
                        res.errorList.Add(s);
                    }

                    res.result = temp.result;
                }

                temp = ValidateSMTP(item.SMTP);
                if (!temp.result)
                {
                    foreach (string s in temp.errorList)
                    {
                        res.errorList.Add(s);
                    }

                    res.result = temp.result;
                }
            }

            return res;
        }
    }
}


