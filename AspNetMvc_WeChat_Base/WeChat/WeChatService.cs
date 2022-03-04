/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.WeChat
*项目描述:
*类 名 称:WeChatService
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/4 23:03:07
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetMvc_WeChat_Base.WeChat
{
    public static class WeChatService
    {
        /// <summary>
        /// 通过属性获取Token令牌
        /// </summary>
        private static string Token
        {
            get { return GetAppConfig("token"); }
        }

        /// <summary>
        /// 获取appsettings中的键值
        /// </summary>
        /// <param name="appSettingKey"></param>
        /// <returns></returns>
        private static string GetAppConfig(string appSettingKey)
        {
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key== appSettingKey)
                {
                    return ConfigurationManager.AppSettings[appSettingKey];
                }
            }
            return null;
        }

        /// <summary>
        /// 加密生成signature
        /// </summary>
        public static string MakeSignature(string timestamp,string nonce)
        {
            var paramArray = new[] { Token, timestamp, nonce }.OrderBy(z => z).ToArray();
            var arrayString = string.Join("",paramArray);
            var shal = System.Security.Cryptography.SHA1.Create();
            var shalArray = shal.ComputeHash(Encoding.UTF8.GetBytes(arrayString));
            StringBuilder builderSignature = new StringBuilder();
            foreach (var elem in shalArray)
            {
                builderSignature.AppendFormat("{0:x2}", elem);
            }
            return builderSignature.ToString();
        }
    }
}
