using System.ComponentModel;

namespace LawOfficeDesktopApp.Models.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class Service : IDataErrorInfo
    {
        public virtual bool IsValid => !string.IsNullOrWhiteSpace(Title)
                                       && MinimumOrBasePrice > 0
                                       && (!IsHasMaximumPrice || MaximumPrice.HasValue && MaximumPrice.Value > MinimumOrBasePrice);
        public string this[string columnName]
        {
            get
            {
                string currentError = null;
                if (columnName == nameof(Title))
                    if (string.IsNullOrWhiteSpace(Title))
                        currentError = "Введите наименование услуги";
                if (columnName == nameof(MinimumOrBasePrice))
                    if (MinimumOrBasePrice <= 0)
                        currentError = "Минимальная цена должна быть положительной";
                if (columnName == nameof(MaximumPrice))
                    if (IsHasMaximumPrice && (!MaximumPrice.HasValue || MaximumPrice <= 0 || MaximumPrice <= MinimumOrBasePrice))
                        currentError = "Максимальная цена должна быть положительной и больше минимальной цены";
                return currentError;
            }
        }

        public string Error => null;
        /// <summary>
        /// https://www.cyberforum.ru/csharp-beginners/thread1830031.html
        /// </summary>
        public string PriceAsString
        {
            get
            {
                if (!MaximumPrice.HasValue)
                {

                    return $"{MinimumOrBasePrice:F0} {GetCorrectRublesText(MinimumOrBasePrice)}";
                }
                else
                {
                    return $"{MinimumOrBasePrice:F0}-{MaximumPrice:F0} {GetCorrectRublesText(MaximumPrice.Value)}";
                }
            }
        }
        public bool IsHasMaximumPrice { get; set; }

        /// <summary>
        /// https://www.cyberforum.ru/csharp-beginners/thread1830031.html
        /// </summary>
        private string GetCorrectRublesText(decimal rubles)
        {
            int rublesRemainder = (int)rubles % 10;
            if (rublesRemainder == 0) return "рублей";
            else if (rublesRemainder == 1) return "рубль";
            else return "рубля";
        }

        public string FormattedRepresentation
        {
            get
            {
                return $"{Title}, стоимость {PriceAsString}";
            }
        }
    }
}
