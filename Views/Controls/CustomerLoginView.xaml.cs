using System.Windows.Controls;
using System.Windows.Input;

namespace LawOfficeDesktopApp.Views.Controls
{
    /// <summary>
    /// Interaction logic for CustomerLoginView.xaml
    /// </summary>
    public partial class CustomerLoginView : Page
    {
        public CustomerLoginView()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Keyboard.Focus(LoginBox);
        }
    }
}
