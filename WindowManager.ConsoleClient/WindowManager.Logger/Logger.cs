using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowManager.Logging
{
    public class Logger
    {
        private const string LogFileName = "WMLogger.txt";
        private const long FileSizeLimit = 50000;

        private string logLocation;
        private string logFile;

        public Logger(string logLocation)
        {
            if (logLocation.EndsWith(@"\"))
            {
                logLocation = logLocation.Substring(0, logLocation.Length - 1);
            }

            this.logLocation = logLocation;
            this.logFile = $"{logLocation}\\{LogFileName}";

            if (!Directory.Exists(logLocation))
            {
                Directory.CreateDirectory(logLocation);
            }

            if (!File.Exists(logFile))
            {
                File.Create(logFile).Close();
            }
        }

        public void LogInformation(string log)
        {
            Log(log, "INFO");
        }

        public void LogError(string log)
        {
            Log(log, "ERROR");
        }
        public void LogWarning(string log)
        {
            Log(log, "WARNING");
        }

        private void Log(string log, string level)
        {
            string timeStamp = DateTime.Now.ToString();

            if (new FileInfo(logFile).Length >= FileSizeLimit)
            {
                string newFileName = $"{Path.GetFileNameWithoutExtension(logFile)}_{timeStamp}.txt";
                File.Move(logFile, newFileName);
            }

            File.AppendAllText(logFile, $"[{timeStamp}][{level, 5}]: {log}{Environment.NewLine}");
        }
    }
}
