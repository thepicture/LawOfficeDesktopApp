﻿using LawOfficeDesktopApp.Controls;
using Microsoft.Toolkit.Mvvm.Messaging;
using System.Windows.Controls;

namespace LawOfficeDesktopApp.Views.Controls
{
    /// <summary>
    /// Interaction logic for MyAccountView.xaml
    /// </summary>
    public partial class MyAccountView : Page
    {
        public MyAccountView()
        {
            InitializeComponent();
            StrongReferenceMessenger.Default
                .Register<MyAccountView, string, string>(this,
                                                         nameof(InitializePassword),
                                                         InitializePassword);
        }

        private void InitializePassword(object recipient, object message)
        {
          
        }
    }
}
