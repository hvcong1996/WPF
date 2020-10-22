using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Utilities
    {
        public const string serviceName = "WCFService";
        public const string updateName = "UpdateCtrl";
        public const string activeName = "ActivateCtrl";
        public const string localHost = "net.pipe://localhost";

        public enum UpdateResult : int
        {
            Unknown = 0,
            Update_OK,
            Update_Fail
        }

        public enum UpdateSettingResult : int
        {
            Unknown = 0,
            UpdateSetting_OK,
            UpdateSetting_Fail
        }

        public static async void ServiceLog(string text)
        {
            string filePath = @"C:\Log\ServiceLog.txt";
            using (StreamWriter log = new StreamWriter(filePath, true, Encoding.UTF8))
            {
                try
                {
                    string logMessage = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + " : " + text;
                    await log.WriteLineAsync(logMessage);
                }
                catch
                {
                }
            }
        }

        public static async void GUILog(string text)
        {
            string filePath = @"C:\Log\GUILog.txt";
            using (StreamWriter log = new StreamWriter(filePath, true, Encoding.UTF8))
            {
                try
                {
                    string logMessage = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + " : " + text;
                    await log.WriteLineAsync(logMessage);
                }
                catch
                {
                }
            }
        }
    }
}
