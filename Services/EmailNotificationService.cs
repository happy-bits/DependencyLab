namespace DependencyLab.Services;

public class EmailNotificationService(ILogger<EmailNotificationService> logger) : INotificationService
{
    public void SendNotification(string recipient, string message)
    {
        // Simulera email-utskick
        logger.LogInformation("📧 Email sent to {Recipient}", recipient);
        logger.LogInformation("📧 Subject: Library Notification");
        logger.LogInformation("📧 Message: {Message}", message);
    }
} 