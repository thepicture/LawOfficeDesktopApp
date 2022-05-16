using System.Windows;
using System.Windows.Controls;

namespace LawOfficeDesktopApp.Controls
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {


        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register("HeaderText", typeof(string), typeof(Header), new PropertyMetadata(default(string)));

        public Header()
        {
            InitializeComponent();
        }
    }
}
