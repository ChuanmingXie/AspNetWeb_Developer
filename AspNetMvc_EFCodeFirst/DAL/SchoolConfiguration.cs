/*****************************************************************************
*项目名称:AspNetMvc_EFCodeFirst.DAL
*项目描述:
*类 名 称:SchoolConfiguration
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/2/28 7:34:36
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;

namespace AspNetMvc_EFCodeFirst.DAL
{

    public class SchoolConfiguration:DbConfiguration
    {
        public SchoolConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}