namespace EmailSender.Core.Application.Interfaces;

public interface ILoggerService
{
    void LogDebug(string message);
    void LogError(string message);
    void LogInformation(string message);
    void LogWarning(string message);
}