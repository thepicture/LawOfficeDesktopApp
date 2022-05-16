using LawOfficeDesktopApp.Models.Entities;

namespace LawOfficeDesktopApp.Models.UserModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class CustomerLoginUser : User
    {
        public override bool IsValid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Login)
                       && !string.IsNullOrWhiteSpace(PlainPassword)
                       && PasswordHash != null;
            }
        }
    }
}
