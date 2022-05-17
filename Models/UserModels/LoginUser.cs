using LawOfficeDesktopApp.Models.Entities;

namespace LawOfficeDesktopApp.Models.UserModels
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class LoginUser : User
    {
        public override bool IsValid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Login)
                       && !string.IsNullOrWhiteSpace(PlainPassword);
            }
        }
    }
}
