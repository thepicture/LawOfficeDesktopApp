using System.ComponentModel;
using System.Linq;

namespace LawOfficeDesktopApp.Models.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class CustomerRequest : IDataErrorInfo
    {
        public virtual bool IsValid
        {
            get
            {
                if (User != null)
                {
                    return Service != null;
                }
                else
                {
                    return !string.IsNullOrWhiteSpace(PhoneNumber)
                       && PhoneNumber.Length == 18
                       && PhoneNumber
                            .ToCharArray()
                            .Count(c =>
                            {
                                return char.IsDigit(c);
                            }) == 11
                       && Service != null
                       && CustomerId > 0;
                }
            }
        }
        public string this[string columnName]
        {
            get
            {
                string currentError = null;
                if (columnName == nameof(Service) && Service == null)
                    currentError = "Выберите услугу";
                if (columnName == nameof(PhoneNumber) && (string.IsNullOrWhiteSpace(PhoneNumber)
                                                          || PhoneNumber.Length != 18 || PhoneNumber
                                                                .ToCharArray()
                                                                .Count(c => char.IsDigit(c)) != 11))
                    currentError = "Введите номер телефона";
                return currentError;
            }
        }

        public string Error => throw new System.NotImplementedException();
    }
}
