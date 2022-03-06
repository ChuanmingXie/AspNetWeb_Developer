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
        public static string Create(string menuJson)
        {
            string menuContent = File.ReadAllText(menuJson, Encoding.GetEncoding("GB2312"));
            string url = "https://api.weixin.qq.com/cgi-bin/menu/create?" +
                "access_token=" + WeChatTookenService.Access_token;
            string result = HttpService.Post(url, menuContent);
            return result;
        }
    }
}
