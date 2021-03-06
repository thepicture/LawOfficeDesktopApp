using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace LawOfficeDesktopApp.Controls
{
    /// <summary>
    /// Interaction logic for ExtendedPasswordBox.xaml
    /// </summary>
    public partial class ExtendedPasswordBox : UserControl, IDataErrorInfo
    {


        public bool IsCanShowPassword
        {
            get { return (bool)GetValue(IsCanShowPasswordProperty); }
            set { SetValue(IsCanShowPasswordProperty, value); }
        }

        public static readonly DependencyProperty IsCanShowPasswordProperty =
            DependencyProperty.Register("IsCanShowPassword",
                                        typeof(bool),
                                        typeof(ExtendedPasswordBox),
                                        new PropertyMetadata(default(bool)));



        public string BindablePassword
        {
            get { return (string)GetValue(BindablePasswordProperty); }
            set { SetValue(BindablePasswordProperty, value); }
        }

        public static readonly DependencyProperty BindablePasswordProperty =
            DependencyProperty.Register("BindablePassword",
                                        typeof(string),
                                        typeof(ExtendedPasswordBox),
                                        new FrameworkPropertyMetadata(null,
                                                                      FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordChanged));

        /// <summary>
        /// Initializes the password for the first time.
        /// </summary>
        private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;
            if (((ExtendedPasswordBox)d).PBoxHidden.Password == "" && ((ExtendedPasswordBox)d).PBoxVisible.Text == "")
            {
                ((ExtendedPasswordBox)d).PBoxHidden.Password = (string)e.NewValue;
            }
        }

        public bool IsPasswordVisible
        {
            get { return (bool)GetValue(IsPasswordVisibleProperty); }
            set { SetValue(IsPasswordVisibleProperty, value); }
        }

        public static readonly DependencyProperty IsPasswordVisibleProperty =
            DependencyProperty.Register("IsPasswordVisible",
                                        typeof(bool),
                                        typeof(ExtendedPasswordBox),
                                        new FrameworkPropertyMetadata(default(bool),
                                                                      FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// https://stackoverflow.com/a/18824921
        /// </summary>
        #region IDataErrorInfo Members

        public string Error
        {
            get
            {
                if (Validation.GetHasError(this))
                    return string.Join(Environment.NewLine, Validation.GetErrors(this).Select(e => e.ErrorContent));

                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (Validation.GetHasError(this))
                {
                    var error = Validation.GetErrors(this).FirstOrDefault(e => ((BindingExpression)e.BindingInError).TargetProperty.Name == columnName);
                    if (error != null)
                        return error.ErrorContent as string;
                }

                return null;
            }
        }

        #endregion

        public ExtendedPasswordBox()
        {
            InitializeComponent();
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            SetValue(BindablePasswordProperty, PBoxHidden.Password);
        }

        private void OnChecked(object sender, RoutedEventArgs e)
        {
            PBoxVisible.Text = PBoxHidden.Password;
        }

        private void OnUnchecked(object sender, RoutedEventArgs e)
        {
            PBoxHidden.Password = PBoxVisible.Text;
        }

        /// <summary>
        /// Shows an error message on the loaded event if the error exists.
        /// </summary>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 2; i++)
            {
                IsPasswordVisible = !IsPasswordVisible;
            }
        }
    }
}
