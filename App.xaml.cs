using LawOfficeDesktopApp.Services;
using LawOfficeDesktopApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System.Windows;

namespace LawOfficeDesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                    .AddSingleton<INavigator<ViewModelBase>, Navigator>()
                    .AddSingleton<INotificationService, NotificationService>()
                    .AddTransient<NavigatorViewModel>()
                    .AddTransient<LoginViewModel>()
                    .AddTransient<RegistrationViewModel>()
                    .BuildServiceProvider());
            Ioc.Default
                .GetService<INavigator<ViewModelBase>>()
                .Go<RegistrationViewModel>();
        }
    }
}
