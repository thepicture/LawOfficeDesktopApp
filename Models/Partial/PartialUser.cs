using System.ComponentModel;

namespace LawOfficeDesktopApp.Models.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class User : IDataErrorInfo
    {
        public virtual bool IsValid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Login)
                       && !string.IsNullOrWhiteSpace(PhoneNumber)
                       && PhoneNumber.Length == 11
                       && !string.IsNullOrWhiteSpace(PlainPassword);
            }
        }
        public string this[string columnName]
        {
            get
            {
                string currentError = null;
                if (columnName == nameof(Login))
                    if (string.IsNullOrWhiteSpace(Login))
                        currentError = "Введите логин";
                if (columnName == nameof(PhoneNumber) && (string.IsNullOrWhiteSpace(PhoneNumber) || PhoneNumber.Length != 11))
                    currentError = "Введите номер телефона";
                if (columnName == nameof(PlainPassword) && string.IsNullOrWhiteSpace(PlainPassword))
                    currentError = "Введите пароль";
                return currentError;
            }
        }

        public string Error => throw new System.NotImplementedException();

        private string maskedPhoneNumber;

        public string MaskedPhoneNumber
        {
            get => maskedPhoneNumber; set
            {
                maskedPhoneNumber = value;
                if (value != null)
                {
                    PhoneNumber = value
                        .Replace("+", "")
                        .Replace("(", "")
                        .Replace(")", "")
                        .Replace("-", "")
                        .Replace(" ", "")
                        .Replace("_", "");
                }
            }
        }
    }
}
