using LawOfficeDesktopApp.Services;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.Xaml.Behaviors.Core;
using System.Windows.Input;

namespace LawOfficeDesktopApp.ViewModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public abstract class ViewModelBase : ObservableObject
    {
        public string Title { get; set; }
        public bool IsRefreshing { get; set; }
        public INavigator<ViewModelBase> Navigator =>
            Ioc.Default.GetService<INavigator<ViewModelBase>>();
        private ActionCommand goBack;

        public ICommand GoBack
        {
            get
            {
                if (goBack == null)
                    goBack = new ActionCommand(PerformGoBack);

                return goBack;
            }
        }

        private void PerformGoBack()
        {
            Navigator.GoBack();
        }
    }
}