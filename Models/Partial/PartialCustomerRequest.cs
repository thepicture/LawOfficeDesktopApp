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
                return !string.IsNullOrWhiteSpace(QuestionText)
                       && !string.IsNullOrWhiteSpace(PhoneNumber)
                       && PhoneNumber.Length == 18
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
                if (columnName == nameof(QuestionText))
                    if (string.IsNullOrWhiteSpace(QuestionText))
                        currentError = "Введите ваш вопрос";
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
