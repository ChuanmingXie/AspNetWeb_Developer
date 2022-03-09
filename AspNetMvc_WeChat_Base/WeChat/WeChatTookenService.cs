/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.WeChat
*项目描述:
*类 名 称:WeChatTockenService
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/4 23:03:07
*修 改 人:
*修改时间:
*作用描述: 微信平台开始开发-获取 许可令牌和IP地址
*https://developers.weixin.qq.com/doc/offiaccount/Basic_Information/Access_Overview.html
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/
using AspNetMvc_WeChat_Base.FuncHelper;
using AspNetMvc_WeChat_Base.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace AspNetMvc_WeChat_Base.WeChat
{
    public static class WeChatTookenService
    {
        /// <summary>
        /// 通过属性获取Token令牌
        /// </summary>
        public static string Token
        {
            get { return GetAppConfig("token"); }
        }

        public static string EncodingAESKey
        {
            get { return GetAppConfig("encodingAESKey"); }
        }

        /// <summary>
        /// 通过属性从web.Configs获取AppID
        /// </summary>
        public static string AppID
        {
            get { return GetAppConfig("appid"); }
        }

        /// <summary>
        /// 通过属性从web.Configs获取AppSecret
        /// </summary>
        public static string AppSecret
        {
            get { return GetAppConfig("appsecret"); }
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
                if (key == appSettingKey)
                {
                    return ConfigurationManager.AppSettings[appSettingKey];
                }
            }
            return null;
        }

        /// <summary>
        /// 加密生成signature
        /// </summary>
        public static string MakeSignature(string timestamp, string nonce)
        {
            var paramArray = new[] { Token, timestamp, nonce }.OrderBy(z => z).ToArray();
            var arrayString = string.Join("", paramArray);
            var shal = System.Security.Cryptography.SHA1.Create();
            var shalArray = shal.ComputeHash(Encoding.UTF8.GetBytes(arrayString));
            StringBuilder builderSignature = new StringBuilder();
            foreach (var elem in shalArray)
            {
                builderSignature.AppendFormat("{0:x2}", elem);
            }
            return builderSignature.ToString();
        }

        public static DateTime tokenValidateTime = DateTime.Now.AddDays(-1);
        private static string access_token;
        /// <summary>
        /// 通过属性获取微信平台的access_token
        /// </summary>
        public static string Access_token
        {
            get
            {
                if (tokenValidateTime <= DateTime.Now)
                {
                    string url = "https://api.weixin.qq.com/cgi-bin/token?" +
                        "grant_type=client_credential" +
                        "&appid=" + AppID +
                        "&secret=" + AppSecret;
                    access_token = HttpService.Get(url);
                }
                //return access_token;
                WeChatAccessToken token = JSONHelper.JSONToObject<WeChatAccessToken>(access_token);
                tokenValidateTime = DateTime.Now.AddSeconds(token.expires_in);
                return token.access_token;
            }
        }

        /// <summary>
        /// 微信服务器Json格式的IP列表
        /// </summary>
        /// <returns></returns>
        public static List<string> GetCallbackIP()
        {
            string url = "https://api.weixin.qq.com/cgi-bin/get_api_domain_ip?" +
                "access_token=" + Access_token;
            string listIPJson = HttpService.Get(url);
            WeChatCallbackIP weChatCallback = JSONHelper.JSONToObject<WeChatCallbackIP>(listIPJson);
            return weChatCallback.ip_list;
        }
    }
}
