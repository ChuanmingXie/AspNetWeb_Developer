/*****************************************************************************
*项目名称:AspNetMvc_EFCodeFirst.DAL
*项目描述:
*类 名 称:SchoolInterceptorLogging
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/2/28 10:04:38
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/

using AspNetMvc_EFCodeFirst.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace AspNetMvc_EFCodeFirst.DAL
{
    public class SchoolInterceptorLogging:DbCommandInterceptor
    {
        private ILogger logger = new Logger();
        private readonly Stopwatch stopwatch = new Stopwatch();

        public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                logger.Error(interceptionContext.Exception, "Error executing command:{0}", command.CommandText);
            }
            else
            {
                logger.TraceApi("SQL Database", "SchoolInterceptor.ScalarExceted", stopwatch.Elapsed, "Command:{0}:", command.CommandText);
            }
            base.ScalarExecuted(command, interceptionContext);
        }

        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            base.ScalarExecuting(command, interceptionContext);
            stopwatch.Restart();
        }

        public override void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                logger.Error(interceptionContext.Exception, "Error excuting command:{0}", command.CommandText);
            }
            else
            {
                logger.TraceApi("SQL Database", "SchoolInterceptor.NonQueryExecuted", stopwatch.Elapsed, "Command:{0}", command.CommandText);
            }
            base.NonQueryExecuted(command, interceptionContext);
        }

        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            base.NonQueryExecuting(command, interceptionContext);
            stopwatch.Restart();
        }

        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                logger.Error(interceptionContext.Exception, "Error excuting command:{0}", command.CommandText);
            }
            else
            {
                logger.TraceApi("SQL Database", "SchoolInterceptor.ReaderExecuted", stopwatch.Elapsed, "Command:{0}", command.CommandText);
            }
            base.ReaderExecuted(command, interceptionContext);
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            base.ReaderExecuting(command, interceptionContext);
            stopwatch.Restart();
        }
    }
}