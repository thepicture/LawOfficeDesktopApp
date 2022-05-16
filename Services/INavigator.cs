namespace LawOfficeDesktopApp.Services
{
    public interface INavigator<TTarget>
    {
        TTarget CurrentTarget { get; }
        void GoBack();
        bool IsCanGoBack { get; }
        void Go<TWhere>() where TWhere : TTarget;
        void Go<TWhere, TParam>(TParam param) where TWhere : TTarget;
        void GoToRoot();
    }
}
