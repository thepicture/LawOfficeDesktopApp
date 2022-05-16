using LawOfficeDesktopApp.ViewModels;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System.Windows;

namespace LawOfficeDesktopApp.Views.Containers
{
    /// <summary>
    /// Interaction logic for NavigatorView.xaml
    /// </summary>
    public partial class NavigatorView : Window
    {
        public NavigatorView()
        {
            InitializeComponent();
            DataContext = Ioc.Default.GetService<NavigatorViewModel>();
        }
    }
}
