/*****************************************************************************
*项目名称:AspNetMvc_EFCodeFirst.Logging
*项目描述:
*类 名 称:Logger
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/2/28 9:03:47
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;

namespace AspNetMvc_EFCodeFirst.Logging
{
    public class Logger : ILogger
    {
        public void Error(string message)
        {
            Trace.TraceError(message);
        }

        public void Error(string fmt, params object[] vars)
        {
            Trace.TraceError(fmt, vars);
        }

        public void Error(Exception exception, string fmt, params object[] vars)
        {
            Trace.TraceError(FormatExceptionMesage(exception, fmt, vars));
        }

        public void Information(string message)
        {
            Trace.TraceInformation(message);
        }

        public void Information(string fmt, params object[] vars)
        {
            Trace.TraceInformation(fmt, vars);
        }

        public void Information(Exception exception, string fmt, params object[] vars)
        {
            Trace.TraceInformation(FormatExceptionMesage(exception, fmt, vars));
        }

        public void TraceApi(string componentName, string method, TimeSpan timeSpan)
        {
            TraceApi(componentName, method, timeSpan, "");
        }

        public void TraceApi(string componentName, string method, TimeSpan timeSpan, string properties)
        {
            string message = string.Concat("Component:", componentName, ";Method:", method, ";Timespan:", timeSpan.ToString(), ";Properties:", properties);
            Trace.TraceInformation(message);
        }

        public void TraceApi(string componentName, string method, TimeSpan timeSpan, string fmt, params object[] vars)
        {
            TraceApi(componentName, method, timeSpan, string.Format(fmt,vars));
        }

        public void Waring(Exception exception, string fmt, params object[] vars)
        {
            Trace.TraceWarning(FormatExceptionMesage(exception, fmt, vars));
        }

        public void Warning(string message)
        {
            Trace.TraceWarning(message);
        }

        public void Warning(string fmt, params object[] vars)
        {
            Trace.TraceWarning(fmt, vars);
        }

        private static string FormatExceptionMesage(Exception exception,string fmt,object[] vars)
        {
            var sb = new StringBuilder();
            sb.Append(string.Format(fmt, vars));
            sb.Append(" Exception: ");
            sb.Append(exception.ToString());
            return sb.ToString();
        }
    }
}