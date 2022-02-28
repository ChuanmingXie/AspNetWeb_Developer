/*****************************************************************************
*项目名称:AspNetMvc_EFCodeFirst.Logging
*项目描述:
*类 名 称:ILogger
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/2/28 8:55:33
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetMvc_EFCodeFirst.Logging
{
    public interface ILogger
    {
        void Information(string message);
        void Information(string fmt, params object[] vars);
        void Information(Exception exception, string fmt, params object[] vars);

        void Warning(string message);
        void Warning(string fmt, params object[] vars);
        void Waring(Exception exception, string fmt, params object[] vars);

        void Error(string message);
        void Error(string fmt, params object[] vars);
        void Error(Exception exception, string fmt, params object[] vars);

        void TraceApi(string componentName, string method, TimeSpan timeSpan);
        void TraceApi(string componentName, string method, TimeSpan timeSpan,string properties);
        void TraceApi(string componentName, string method, TimeSpan timeSpan,string fmt,params object[] vars);
    }
}
