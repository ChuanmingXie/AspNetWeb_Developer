/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.APIHelper
*项目描述:
*类 名 称:PropertyValue
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/7 14:48:12
*修 改 人:
*修改时间:
*作用描述:定义一个反射功能类,将模型中的数据通过反射获取
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/
using AspNetMvc_WeChat_Base.Logging;
using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace AspNetMvc_WeChat_Base.APIHelper
{
    public static class ReflectionHelper
    {
        /// <summary>
        /// 反射获取模型单一属性的值
        /// </summary>
        /// <param name="proName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetModelValue(string proName, object model)
        {
            try
            {
                Type type = model.GetType();
                object valueObj = type.GetProperty(proName).GetValue(model, null);
                string value = Convert.ToString(valueObj);
                if (string.IsNullOrEmpty(value))
                    return string.Empty;
                return value;
            }
            catch (Exception ex)
            {
                LogService.RecordLog("反射转换属性值错误:" + ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// 反射设置模型单一属性的值
        /// </summary>
        /// <param name="proName"></param>
        /// <param name="proValue"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool SetModelValue(string proName, string proValue, object model)
        {
            try
            {
                Type type = model.GetType();
                object valueObj = Convert.ChangeType(proValue, type.GetProperty(proName).PropertyType);
                type.GetProperty(proName).SetValue(model, valueObj, null);
                return true;
            }
            catch (Exception ex)
            {
                LogService.RecordLog("反射设置属性值错误:" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 反射获取全部属性值
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetModelAllValue(object model)
        {
            string allInfoString = string.Empty;
            try
            {
                var list = model.GetType().GetProperty("dictionary").GetValue(model, null);
                foreach (object m in (list as IEnumerable))
                {
                    var itemKey = m.GetType().GetProperty("Key").GetValue(m, null);
                    allInfoString += itemKey;
                    var itemValue = m.GetType().GetProperty("Value").GetValue(m, null);
                    foreach (object subItem in (itemValue as IEnumerable))
                        allInfoString += subItem + "\r\n";
                }
            }
            catch (Exception ex)
            {
                LogService.RecordLog("反射获取属性值错误:" + ex.Message);
            }
            return allInfoString;
        }

        public static string GetModelByGeneric<T>(T t)
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                PropertyInfo[] propertyInfos = t.GetType().GetProperties();
                foreach (var proInfo in propertyInfos)
                {
                    //属性不为空且示泛型类型
                    if (proInfo != null && proInfo.PropertyType.IsGenericType)
                    {
                        string modelName = proInfo.PropertyType.GetGenericArguments()[0].Name;
                        stringBuilder.Append("  " + proInfo.Name + "\n");
                        object subObj = proInfo.GetValue(t, null);
                        if (subObj != null)
                        {
                            //因为是反射返回的数据，无法直接转换为List使用，
                            //针对这种数据，反射机制对这种属性值提供了
                            //"Count"列表长度、"Item"子元素等属性；
                            int count = Convert.ToInt32(subObj.GetType().GetProperty("Count").GetValue(subObj, null));
                            for (int i = 0; i < count; i++)
                            {
                                stringBuilder.Append(" " + "  {\n");
                                object item = subObj.GetType().GetProperty("Item").GetValue(subObj, new object[] { i });
                                GetModelByGeneric(item);
                                stringBuilder.Append(" " + "  },\n");
                            }
                        }
                    }
                    else
                    {
                        stringBuilder.Append(" " + "  " + proInfo.Name + ":" + proInfo.GetValue(t, null).ToString() + "\n");
                    }
                }
            }
            catch (Exception ex)
            {
                LogService.RecordLog("反射获取属性值错误:" + ex.Message);
            }
            return stringBuilder.ToString();
        }
    }
}
