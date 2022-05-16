namespace LawOfficeDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public abstract class ViewModelBase
    {
        public string Title { get; set; }
        public bool IsRefreshing { get; set; }
    }
}