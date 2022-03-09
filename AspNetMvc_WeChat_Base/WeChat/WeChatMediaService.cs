/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.WeChat
*项目描述:
*类 名 称:WeChatMediaService
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/9 15:36:25
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/
using AspNetMvc_WeChat_Base.FuncHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AspNetMvc_WeChat_Base.WeChat
{
    public static class WeChatMediaService
    {
        public static void MediaTempGet(string media_id,string path)
        {
            string url = " https://api.weixin.qq.com/cgi-bin/media/get" +
                "?access_token=" + WeChatTookenService.Access_token+
                "&media_id=" + media_id;
            HttpService.PostDownLoad(url, path);
        }

        /// <summary>
        /// 将图片上传至微信服务器
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string MediaUPloadParam(string type,string path)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/media/upload" +
                "?access_token=" + WeChatTookenService.Access_token +
                "&type=" + type;
            string result = HttpService.HttpUPloadFile(url, path);
            return result;
        } 
    }
}
