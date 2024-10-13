using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace CapaNegocio
{
    public class NLogger
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void LogInfo(string message)
        {
            Logger.Info(message);   
        }

        public static void LogWarn(string message) 
        { 
            Logger.Warn(message); 
        }

        public static void LogError(string message, Exception ex = null) 
        {
            if (ex != null)
            {
                Logger.Error(message);
            }
            else
            {
                Logger.Error(message); 
            }
        }

        public static void LogDebug(string message)
        {
            Logger.Debug(message);
        }
       
        public static void LoFatal(string message) 
        {
            Logger.Fatal(message);  
        }

    }
}
