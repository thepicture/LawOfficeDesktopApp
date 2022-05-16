using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace LawOfficeDesktopApp.Models.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class User : IDataErrorInfo
    {
        public bool IsValid { get; set; }

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
                IsValid = currentError == null;
                return currentError;
            }
        }

        public string Error => throw new System.NotImplementedException();

        private string plainPassword;
        private string maskedPhoneNumber;

        public string PlainPassword
        {
            get => plainPassword; set
            {
                plainPassword = value;
                if (value != null)
                {
                    PasswordHash = MD5
                        .Create()
                        .ComputeHash(
                            Encoding.UTF8.GetBytes(PlainPassword));
                }
            }
        }

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
