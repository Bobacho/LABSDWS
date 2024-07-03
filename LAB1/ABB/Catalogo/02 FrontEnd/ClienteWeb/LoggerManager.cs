using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;


public interface ILoggerManager
{
    void LogInfo(string message);
    void LogWarn(string message);
    void LogDebug(string message);
    void LogError(string message);
}

public class LoggerManager : ILoggerManager
{
    private static readonly ILog log = LogManager.GetLogger(typeof(LoggerManager));

    public void LogInfo(string message)
    {
        log.Info(message);
    }

    public void LogWarn(string message)
    {
        log.Warn(message);
    }

    public void LogDebug(string message)
    {
        log.Debug(message);
    }

    public void LogError(string message)
    {
        log.Error(message);
    }
}
