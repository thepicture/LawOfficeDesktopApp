using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;

namespace LawOfficeDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class NavigatorViewModel : ViewModelBase
    {
        public ViewModelBase CurrentTarget =>
            Ioc.Default.GetService<INavigator<ViewModelBase>>().CurrentTarget;
    }
}
