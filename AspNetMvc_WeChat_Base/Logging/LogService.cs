/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.Logging
*项目描述:
*类 名 称:LogService
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/4 22:34:45
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.IO;

namespace AspNetMvc_WeChat_Base.Logging
{
    public static class LogService
    {
        static string logFileName = "log";

        static string logFile = string.Empty;

        public static void RecordLog(string text)
        {
            string logDirectory = AppDomain.CurrentDomain.BaseDirectory + "//" + logFileName;
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            logFile = logDirectory + "//" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            File.AppendAllText(logFile, DateTime.Now.ToString("\r\n"+"[yyyy-MM-dd HH:mm:ss]:") + text+"\r\n");
        }
    }
}
