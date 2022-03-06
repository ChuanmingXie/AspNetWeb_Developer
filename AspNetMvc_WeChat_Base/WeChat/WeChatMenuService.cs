/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.WeChat
*项目描述:
*类 名 称:WeChatMenuService
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/6 8:15:24
*修 改 人:
*修改时间:
*作用描述:微信平公众台自定义菜单
*https://developers.weixin.qq.com/doc/offiaccount/Custom_Menus/Creating_Custom-Defined_Menu.html
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/
using AspNetMvc_WeChat_Base.APIHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetMvc_WeChat_Base.WeChat
{
    public static class WeChatMenuService
    {
        /// <summary>
        /// 创建自定义菜单
        /// </summary>
        /// <param name="menuJson"></param>
        /// <returns></returns>
        public static string Create(string menuJson)
        {
            string menuContent = File.ReadAllText(menuJson, Encoding.GetEncoding("GB2312"));
            string url = "https://api.weixin.qq.com/cgi-bin/menu/create" +
                "?access_token=" + WeChatTookenService.Access_token;
            string result = HttpService.Post(url, menuContent);
            return result;
        }

        /// <summary>
        /// 查询自定义菜单
        /// </summary>
        /// <returns></returns>
        public static string Search()
        {
            string url = "https://api.weixin.qq.com/cgi-bin/menu/get" +
                "?access_token=" + WeChatTookenService.Access_token;
            string result = HttpService.Get(url);
            return result;
        }

        /// <summary>
        /// 删除自定义菜单
        /// </summary>
        /// <returns></returns>
        public static string Delete()
        {
            string url = "https://api.weixin.qq.com/cgi-bin/menu/delete" +
                "?access_token=" + WeChatTookenService.Access_token;
            string result = HttpService.Get(url);
            return result;
        }

        /// <summary>
        /// 获取自定义菜单配置
        /// </summary>
        /// <returns></returns>
        public static string Param()
        {
            string url = "https://api.weixin.qq.com/cgi-bin/get_current_selfmenu_info" +
                "?access_token="+WeChatTookenService.Access_token;
            string result = HttpService.Get(url);
            return result;
        }

        /// <summary>
        /// 创建个性化菜单
        /// </summary>
        /// <param name="menuFile"></param>
        /// <returns></returns>
        public static string CreatePersonalMenu(string menuFile)
        {
            string menuContent = File.ReadAllText(menuFile, Encoding.GetEncoding("GB2312"));
            string url = "https://api.weixin.qq.com/cgi-bin/menu/addconditional" +
                "?access_token="+WeChatTookenService.Access_token;
            string result = HttpService.Post(url, menuContent);
            return result;
        }

        /// <summary>
        /// 删除个性化菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static string DeletePersonalMenu(string menuId)
        {
            string menuIdJson = "{\"menuid\":\"" + menuId + "\"}";
            string url = "https://api.weixin.qq.com/cgi-bin/menu/delconditional" +
                "?access_token="+WeChatTookenService.Access_token;
            string result = HttpService.Post(url, menuIdJson);
            return result;
        }

        /// <summary>
        /// 匹配个性化菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string MatchPersonalMenu(string userId)
        {
            string userIdJson = "{\"user_id\":\"" + userId + "\"}";
            string url = "https://api.weixin.qq.com/cgi-bin/menu/trymatch" +
                "?access_token="+WeChatTookenService.Access_token;
            string result = HttpService.Post(url, userIdJson);
            return result;
        }
    }
}
