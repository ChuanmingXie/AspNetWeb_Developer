﻿/*****************************************************************************
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
        /*

                <li>@Html.ActionLink("【例8-1】新增临时素材", "MediaTemporary", "Home", new { area = "Chapter08" }, null)</li>
                <li>@Html.ActionLink("【例8-2】获取临时素材", "MediaTemporaryGet", "Home", new { area = "Chapter08" }, null)</li>
                <li>@Html.ActionLink("【例8-3】新增永久素材封面", "MediaPermanentCover", "Home", new { area = "Chapter08" })</li>
                <li>@Html.ActionLink("【例8-4】新增永久素材", "MediaPermanent", "Home", new { area = "Chapter08" }, null)</li>
                <li>@Html.ActionLink("【例8-5】新增永久图文素材", "MediaPermanentNewImg", "Home", new { area = "Chapter08" }, null)</li>
                <li>@Html.ActionLink("【例8-6】新增其他永久素材", "MediaMaterial", "Home", new { area = "Chapter08" }, null)</li>
                <li>@Html.ActionLink("【例8-7】获取永久素材", "MediaMaterialGet", "Home", new { area = "Chapter08" }, null)</li>
                <li>@Html.ActionLink("【例8-8】修改永久素材", "MediaPermanentUpdate", "Home", new { area = "Chapter08" }, null)</li>
                <li>@Html.ActionLink("【例8-9】删除永久素材", "MediaMaterialDelete", "Home", new { area = "Chapter08" }, null)</li>
                <li>@Html.ActionLink("【例8-10】获取素材总数", "MediaMaterialCount", "Home", new { area = "Chapter08" }, null)</li>
                <li>@Html.ActionLink("【例8-11】获取素材列表", "MediaMaterialBatch", "Home", new { area = "Chapter08" }, null)</li>

         */

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
            string url = "https://api.weixin.qq.com/cgi-bin/material/add_news" +
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
    }
}
