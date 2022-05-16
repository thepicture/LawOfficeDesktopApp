using LawOfficeDesktopApp.ViewModels;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LawOfficeDesktopApp.Services
{
    [AddINotifyPropertyChangedInterface]
    public class Navigator : ObservableObject, INavigator<ViewModelBase>
    {
        public Stack<ViewModelBase> Journal { get; set; }
            = new Stack<ViewModelBase>();
        public ViewModelBase CurrentTarget => Journal.Peek();

        public void Go<TWhere>() where TWhere : ViewModelBase
        {
            Journal.Push(
                Activator.CreateInstance<TWhere>());
        }

        public void GoBack()
        {
            Journal.Pop();
        }

        public void GoToRoot()
        {
            while (Journal.Count > 1)
            {
                GoBack();
            }
        }

        public void Go<TWhere, TParam>(TParam param)
            where TWhere : ViewModelBase
        {
            Journal.Push(
                (ViewModelBase)
                Activator.CreateInstance(typeof(TWhere),
                                         new object[] { param }));
        }

        public bool IsCanGoBack
        {
            get
            {
                if (Journal.Count < 2)
                {
                    return false;
                }
                else
                {
                    ViewModelBase viewModel = Journal
                        .ElementAt(1);
                    return !(viewModel is LoginViewModel)
                           || Journal.Peek() is CustomerRegistrationViewModel;
                }
            }
        }
    }
}
