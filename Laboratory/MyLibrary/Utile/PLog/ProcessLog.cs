using Serilog;

namespace MyLibrary.Utile.PLog
{
    public class ProcessLog
    {
        public ILogger Logger = null;

        public ProcessLog(string logPath)
        {
            Logger = new LoggerConfiguration()
                  .MinimumLevel.Debug()
                  .WriteTo.File("Log\\"+logPath, rollingInterval: RollingInterval.Day)
                  .CreateLogger();
        }
    }
}
