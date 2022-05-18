using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Linq;

namespace LawOfficeDesktopApp.Models.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class User : ObservableObject, IDataErrorInfo
    {
        public virtual bool IsValid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Login)
                       && !string.IsNullOrWhiteSpace(PhoneNumber)
                       && PhoneNumber.Length == 18
                       && !string.IsNullOrWhiteSpace(PlainPassword)
                       && PhoneNumber
                            .ToCharArray()
                            .Count(c =>
                            {
                                return char.IsDigit(c);
                            }) == 11;
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
                if (columnName == nameof(PhoneNumber) && (string.IsNullOrWhiteSpace(PhoneNumber)
                                                          || PhoneNumber.Length != 18
                                                          || PhoneNumber
                                                                .ToCharArray()
                                                                .Count(c =>
                                                                {
                                                                    return char.IsDigit(c);
                                                                }) != 11))
                    currentError = "Введите номер телефона";
                if (columnName == nameof(PlainPassword) && string.IsNullOrWhiteSpace(PlainPassword))
                    currentError = "Введите пароль";
                return currentError;
            }
        }

        public string Error => null;
    }
}
