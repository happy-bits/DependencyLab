namespace DependencyLab.Services;

public interface INotificationService
{
    void SendNotification(string recipient, string message);
} 