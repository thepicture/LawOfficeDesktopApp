using System.ComponentModel;

namespace LawOfficeDesktopApp.Models.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class CustomerRequest : IDataErrorInfo
    {
        private string maskedPhoneNumber;
        public virtual bool IsValid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(QuestionText)
                       && !string.IsNullOrWhiteSpace(PhoneNumber)
                       && PhoneNumber.Length == 11;
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
                if (columnName == nameof(PhoneNumber) && (string.IsNullOrWhiteSpace(PhoneNumber) || PhoneNumber.Length != 11))
                    currentError = "Введите номер телефона";
                return currentError;
            }
        }

        public string Error => throw new System.NotImplementedException();

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
