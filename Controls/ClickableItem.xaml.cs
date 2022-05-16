using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LawOfficeDesktopApp.Controls
{
    /// <summary>
    /// Interaction logic for ClickableItem.xaml
    /// </summary>
    public partial class ClickableItem : UserControl
    {


        public ICommand ItemCommand
        {
            get { return (ICommand)GetValue(ItemCommandProperty); }
            set { SetValue(ItemCommandProperty, value); }
        }

        public static readonly DependencyProperty ItemCommandProperty =
            DependencyProperty.Register("ItemCommand", typeof(ICommand), typeof(ClickableItem), new PropertyMetadata(null));


        public PackIconKind IconKind
        {
            get { return (PackIconKind)GetValue(IconKindProperty); }
            set { SetValue(IconKindProperty, value); }
        }

        public static readonly DependencyProperty IconKindProperty =
            DependencyProperty.Register("IconKind", typeof(PackIconKind), typeof(ClickableItem), new PropertyMetadata(null));



        public string ItemText
        {
            get { return (string)GetValue(ItemTextProperty); }
            set { SetValue(ItemTextProperty, value); }
        }

        public static readonly DependencyProperty ItemTextProperty =
            DependencyProperty.Register("ItemText", typeof(string), typeof(ClickableItem), new PropertyMetadata(default(string)));


        public ClickableItem()
        {
            InitializeComponent();
        }
    }
}
