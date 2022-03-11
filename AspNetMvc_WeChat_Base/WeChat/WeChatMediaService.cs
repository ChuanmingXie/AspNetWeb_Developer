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
using System.Web;

namespace AspNetMvc_WeChat_Base.WeChat
{
    public static class WeChatMediaService
    {
        /// <summary>
        /// 创建自己上传文件后需要保存到服务器位置的全路径
        /// </summary>
        /// <param name="upLoadFile"></param>
        /// <param name="path"></param>
        public static string UPloadPath(HttpPostedFileBase upLoadFile, string path)
        {
            int index = upLoadFile.FileName.LastIndexOf('.');
            var fileExt = upLoadFile.FileName.Substring(index, upLoadFile.FileName.Length - index);
            var newFile = DateTime.Now.ToString("yyyyMMddHHmmss") + fileExt;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            string filePath = path + "\\" + newFile;
            upLoadFile.SaveAs(filePath);
            return filePath;
        }

        /// <summary>
        /// 创建自己获取文件后需要保存到服务器位置的全路径
        /// </summary>
        public static string GetFilePath(string serverPath, string fileName)
        {
            if (!Directory.Exists(serverPath))
                Directory.CreateDirectory(serverPath);
            return serverPath + "\\" + fileName;
        }

        /// <summary>
        /// 新增临时素材 | 永久封面素材
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string AddTemporaryMedia(string type, string file)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/media/upload" +
                "?access_token=" + WeChatTookenService.Access_token +
                "&type=" + type;
            string result = HttpService.HttpUPloadFile(url, file);
            return result;
        }

        /// <summary>
        /// 获取临时素材
        /// </summary>
        /// <param name="media_id"></param>
        /// <param name="file"></param>
        public static void GetTemporaryMedia(string media_id, string file)
        {
            string url = " https://api.weixin.qq.com/cgi-bin/media/get" +
                "?access_token=" + WeChatTookenService.Access_token +
                "&media_id=" + media_id;
            HttpService.PostDownLoad(url, file);
        }

        /// <summary>
        /// 新增永久性图文素材-封面
        /// </summary>
        /// <param name="type"></param>
        /// <param name="file"></param>
        /// <returns>mediaid</returns>
        public static string AddMediaPermanentCover(string type, string file)
        {
            return AddTemporaryMedia(type, file);
        }

        /// <summary>
        /// 新增永久图文素材
        /// </summary>
        /// <param name="content"></param>
        /// <returns>mediaid</returns>
        public static string AddMediaPermanent(string content)
        {
            string url = " https://api.weixin.qq.com/cgi-bin/material/add_news" +
                "?access_token=" + WeChatTookenService.Access_token;
            string result = HttpService.Post(url, content);
            return result;
        }

        /// <summary>
        /// 新增永久图文素材-图片
        /// </summary>
        /// <param name="file"></param>
        /// <returns>上传图文消息内的图片,获取URL</returns>
        public static string AddMediaNewImg(string file)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/media/uploadimg" +
                "?access_token=" + WeChatTookenService.Access_token;
            string result = HttpService.HttpUPloadFile(url, file);
            return result;
        }

        /// <summary>
        /// 新增其他类型永久素材
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string AddMediaMaterial(string type, string file)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/material/add_material" +
                "?access_token=" + WeChatTookenService.Access_token +
                "&type=" + type;
            string result = HttpService.HttpAddMaterial(url, file);
            return result;
        }

        /// <summary>
        /// 获取永久素材
        /// </summary>
        /// <param name="media_id"></param>
        /// <param name="file"></param>
        public static void GetMediaMaterial(string media_id, string file)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/material/get_material" +
                "?access_token=" + WeChatTookenService.Access_token;
            HttpService.HttpGetMaterial(url, media_id, file);
        }

        public static string DeleteMaterial(string media_id)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/material/del_material" +
                "?access_token=" + WeChatTookenService.Access_token;
            string result = HttpService.Post(url, media_id);
            return result;
        }

        public static string GetMaterialCount()
        {
            string url = "https://api.weixin.qq.com/cgi-bin/material/get_materialcount" +
                "?access_token=" + WeChatTookenService.Access_token;
            string result = HttpService.Get(url);
            return result;
        }

        public static string GetMaterialList(string param)
        {
            string url = "https://api.weixin.qq.com/cgi-bin/material/batchget_material" +
                "?access_token=" + WeChatTookenService.Access_token;
            string result = HttpService.Post(url, param);
            return result;
        }
    }
}
