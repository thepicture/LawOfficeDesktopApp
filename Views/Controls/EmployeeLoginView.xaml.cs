using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LawOfficeDesktopApp.Views.Controls
{
    /// <summary>
    /// Interaction logic for EmployeeLoginView.xaml
    /// </summary>
    public partial class EmployeeLoginView : Page
    {
        public EmployeeLoginView()
        {
            InitializeComponent();
        }

        private void OnVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                LoginBox.Focus();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Keyboard.Focus(LoginBox);
        }
    }
}
