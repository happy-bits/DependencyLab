namespace DependencyLab.Services;

public class ConsoleNotificationService(ILogger<ConsoleNotificationService> logger) : INotificationService
{
    public void SendNotification(string recipient, string message)
    {
        logger.LogInformation("Notification to {Recipient}: {Message}", recipient, message);
    }
}