namespace LawOfficeDesktopApp.Models.Entities
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public partial class Service
    {
        public string PriceAsString
        {
            get
            {
                if (!MaximumPrice.HasValue)
                {
                    return $"{MinimumOrBasePrice:F0} рублей";
                }
                else
                {
                    return $"{MinimumOrBasePrice:F0}-{MaximumPrice:F0} рублей";
                }
            }
        }
    }
}
