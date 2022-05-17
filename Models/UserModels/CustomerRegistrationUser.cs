using LawOfficeDesktopApp.Models.Entities;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace LawOfficeDesktopApp.Models.UserModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class CustomerRegistrationUser : User
    {
        public bool IsNew => Id == 0;
    }
}
