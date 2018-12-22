namespace MailSender.Domain.Constants
{
        public static class ClassNamesConstants
        {
        #region SenderConstants
            public static string SenderClassName = "Отправитель";

            public static string SenderKeyName = "Email";

            public static string SenderValueName = "Пароль";
        #endregion

            #region ReceiverConstants

            public static string ReceiverClassName = "Получатель";

            public static string ReceiverKeyName = "Email";

            public static string ReceiverValueName = "Имя";

        #endregion

            #region SMTPContants

            public static string SMTPClassName = "SMTP Сервер";

            public static string SMTPKeyName = "Email";

            public static string SMTPValueName = "Email";

        #endregion

        public static string SendedClassName = "Отправленная рассылка";



            public static string MailClassName = "Письмо";

        }
}