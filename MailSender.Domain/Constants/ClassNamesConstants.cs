namespace MailSender.Domain.Constants
{
    public static class ClassNamesConstants
    {
        #region SenderConstants

        public static string SenderClassName{get => "Отправитель";}

        public static string SenderKeyName { get => "Email"; } 

        public static string SenderValueName { get => "Пароль"; } 

        #endregion

        #region ReceiverConstants

        public static string ReceiverClassName { get => "Получатель"; } 

        public static string ReceiverKeyName { get => "Email"; } 

        public static string ReceiverValueName { get => "Имя"; } 

        #endregion

        #region SMTPContants

        public static string SMTPClassName { get => "SMTP Сервер"; } 

        public static string SMTPKeyName { get => "Имя SMTP"; } 

        public static string SMTPValueName { get => "Порт"; }
        #endregion

        public static string SendedClassName { get => "Отправленная рассылка"; }

        public static string MailClassName { get => "Письмо"; }

        public static string Topic { get => "Тема"; }

    }
}