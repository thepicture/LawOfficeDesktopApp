using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Xaml.Behaviors.Core;
using System.Windows.Input;

namespace LawOfficeDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class NavigatorViewModel : ViewModelBase
    {
        public NavigatorViewModel()
        {
            Ioc.Default.GetService<INavigator<ViewModelBase>>().Navigated += () =>
            {
                OnPropertyChanged(
                    nameof(CurrentTarget));
                OnPropertyChanged(
                    nameof(Navigator.IsCanGoBack));
            };
        }

        public ViewModelBase CurrentTarget =>
            Ioc.Default.GetService<INavigator<ViewModelBase>>().CurrentTarget;
    }
}
