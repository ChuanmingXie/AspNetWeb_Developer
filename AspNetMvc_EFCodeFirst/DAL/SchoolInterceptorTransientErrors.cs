/*****************************************************************************
*项目名称:AspNetMvc_EFCodeFirst.DAL
*项目描述:
*类 名 称:SchoolInterceptorTransientErrors
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/2/28 10:17:50
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
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace AspNetMvc_EFCodeFirst.DAL
{
    public class SchoolInterceptorTransientErrors : DbCommandInterceptor
    {
        private int counter = 0;
        private ILogger logger = new Logger();

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            bool throwTransientErrors = false;
            if (command.Parameters.Count > 0 && command.Parameters[0].Value.ToString() == "%Throw%")
            {
                throwTransientErrors = true;
                command.Parameters[0].Value = "%an%";
                command.Parameters[1].Value = "%an%";
            }
            if (throwTransientErrors && counter < 4)
            {
                logger.Information("Returning transient error for command:{0}", command.CommandText);
                counter++;
                interceptionContext.Exception = CreateDummySqlException();
            }
            base.ReaderExecuting(command, interceptionContext);
        }

        private Exception CreateDummySqlException()
        {
            var sqlErrorNumber = 20;
            var sqlErrorCtor = typeof(SqlError)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(c => c.GetParameters().Count() == 7).Single();
            var sqlError = sqlErrorCtor.Invoke(new object[] { sqlErrorNumber, (byte)0, (byte)0, "", "", "", 1 });

            var errorCollection = Activator.CreateInstance(typeof(SqlErrorCollection), true);
            var addMethod=typeof(SqlErrorCollection).GetMethod("Add",BindingFlags.Instance|BindingFlags.NonPublic);
            addMethod.Invoke(errorCollection, new[] { sqlError });

            var sqlExceptionCtor = typeof(SqlException)
                .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(c => c.GetParameters().Count() == 4).Single();
            var sqlException = (SqlException)sqlExceptionCtor.Invoke(new object[] { "Dummy",errorCollection,null,Guid.NewGuid() });
            return sqlException;
        }
    }
}