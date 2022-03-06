/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.APIHelper
*项目描述:
*类 名 称:JSONHelper
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/5 7:44:21
*修 改 人:
*修改时间:
*作用描述:接收微信平台返回的数据并通过JSON解析
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace AspNetMvc_WeChat_Base.APIHelper
{
    public static class JSONHelper
    {
        /// <summary>
        /// 将对象转化为JSON字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJSON(object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                return serializer.Serialize(obj);
            }
            catch (Exception ex)
            {
                throw new Exception("JSONHelper.ObjectToJSON():" + ex.Message);
            }
        }

        /// <summary>
        /// 将数据表对象转化为键值对集合
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static List<Dictionary<string, Object>> DataTableToList(DataTable dataTable)
        {
            List<Dictionary<string, Object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dataTable.Rows)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                foreach (DataColumn dc in dataTable.Columns)
                {
                    dic.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                list.Add(dic);
            }
            return list;
        }

        /// <summary>
        /// 将数据表对象转化为JSON字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJSON(DataTable dt)
        {
            return ObjectToJSON(DataTableToList(dt));
        }

        /// <summary>
        /// 将JSON字符串转化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static T JSONToObject<T>(string jsonText)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            try
            {
                return serializer.Deserialize<T>(jsonText);
            }
            catch (Exception ex)
            {
                return default(T);
                throw new Exception("JSONHelper.JSONToObject():" + ex.Message);
            }
        }
    }
}
