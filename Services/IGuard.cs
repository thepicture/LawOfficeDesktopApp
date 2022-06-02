namespace LawOfficeDesktopApp.Services
{
    public interface IGuard<TVerificationTarget>
    {
        bool Verify(TVerificationTarget target);
    }
}
