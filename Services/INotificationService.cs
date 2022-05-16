using System.Threading.Tasks;

namespace LawOfficeDesktopApp.Services
{
    public interface INotificationService
    {
        Task NotifyAsync(object information);
        Task WarnAsync(object warning);
        Task<bool> AskAsync(object question);
        Task NotifyErrorAsync(object error);
    }
}
