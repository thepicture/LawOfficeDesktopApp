using System.ComponentModel;

namespace LawOfficeDesktopApp.Models.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class Consultation : IDataErrorInfo
    {
        public virtual bool IsValid
        {
            get
            {
                return User != null && Service != null;
            }
        }
        public string this[string columnName]
        {
            get
            {
                string currentError = null;
                if (columnName == nameof(User))
                    if (User == null)
                        currentError = "Выберите клиента";
                if (columnName == nameof(Service))
                    if (Service == null)
                        currentError = "Выберите услугу";
                return currentError;
            }
        }

        public string Error => throw new System.NotImplementedException();
    }
}
