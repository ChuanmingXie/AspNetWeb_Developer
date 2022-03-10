using AspNetMvc_WeChat_Base.FuncHelper;
using AspNetMvc_WeChat_Base.Model;
using AspNetMvc_WeChat_Base.WeChat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMvc_WeChat.Areas.Chapter08.Controllers
{
    public class HomeController : Controller
    {
        // GET: Chapter08/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MediaTemporary()
        {
            return View();
        }
        /// <summary>
        /// 1.新增临时素材
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MediaTemporary(WeChatMediaUpload mediaUpload)
        {
            //if (Request.Files.Count == 0 || Request.Files[0].ContentLength == 0)
            if(!string.IsNullOrEmpty(mediaUpload.uploadFile.FileName))
            {
                string path = Server.MapPath("/Media");
                WeChatMediaService.UPloadPath(mediaUpload.uploadFile, path);
                string mediaJson = WeChatMediaService.AddTemporaryMedia("image", path);
                WeChatMediaUpResult mediaResult = JSONHelper.JSONToObject<WeChatMediaUpResult>(mediaJson);
                ViewBag.mediaTemporaryID = "Media_id:" + mediaResult.Media_id;
                return View();
            }
            return View();
        }


        public ActionResult MediaTemporaryGet()
        {
            return View();
        }

        /// <summary>
        /// 2.获取临时素材
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MediaTemporaryGet(FormCollection form)
        {
            //创建自己获取文件后需要保存到服务器位置的全路径
            string mediaid = form["Mediaid"];
            string savePath = Server.MapPath("~/Download/");
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmddfff") + ".jpg";
            string path = WeChatMediaService.GetFilePath(savePath, fileName);
            WeChatMediaService.GetTemporaryMedia(mediaid, path);
            Response.Redirect("~/Download/" + fileName);
            return View();
        }


        public ActionResult MediaPermanentCover()
        {
            return View();
        }

        /// <summary>
        /// 3.新增永久素材封面
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MediaPermanentCover(FormCollection form)
        {
            if (Request.Files.Count == 0 || Request.Files[0].ContentLength == 0)
            {
                return View();
            }
            else
            {
                string path = Server.MapPath("/Media");
                WeChatMediaService.UPloadPath(Request.Files[0], path);
                string mediaJson = WeChatMediaService.AddMediaPermanentCover("thumb", path);
                WeChatMediaUpResult mediaResult = JSONHelper.JSONToObject<WeChatMediaUpResult>(mediaJson);
                ViewBag.mediaThumbCoverID = "Media_id:" + mediaResult.Media_id;
                return View();
            }
        }


        public ActionResult MediaPermanent()
        {
            return View();
        }

        /// <summary>
        /// 4.新增永久素材
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MediaPermanent([Bind(Include = "title," +
            "thum_media_Id,author,digest,show_cover_pic,content," +
            "content_source_url")] WeChatMediaArticle article)
        {
            string modelArticle = "{\"articeles\":[" + JSONHelper.ObjectToJSON(article) + "]}";
            string mediaJson = WeChatMediaService.AddMediaPermanent(modelArticle);
            WeChatMediaUpResult mediaUpResult = JSONHelper.JSONToObject<WeChatMediaUpResult>(mediaJson);
            ViewBag.mediaPermanentID = "Media_id:" + mediaUpResult.Media_id;
            return View();
        }


        public ActionResult MediaPermanentNewImg()
        {
            return View();
        }

        /// <summary>
        /// 5.新增永久图文素材
        /// </summary>
        /// <returns></returns>
        [HttpPost, ActionName("MediaPermanentNewImg")]
        [ValidateAntiForgeryToken]
        public ActionResult MediaPermanentNewImgPost()
        {
            if (Request.Files.Count == 0 || Request.Files[0].ContentLength == 0)
            {
                return View();
            }
            else
            {
                string path = Server.MapPath("~/Media");
                WeChatMediaService.UPloadPath(Request.Files[0], path);
                string mediaJson = WeChatMediaService.AddMediaNewImg(path);
                WeChatUPloadimgResult uploadimgResult = JSONHelper.JSONToObject<WeChatUPloadimgResult>(mediaJson);
                ViewBag.uploadImgUrl = uploadimgResult.url;
                return View();
            }
        }


        /// <summary>
        /// 新增其他永久素材
        /// </summary>
        /// <returns></returns>
        public ActionResult MediaMaterial()
        {
            return View();
        }


        /// <summary>
        /// 获取永久素材
        /// </summary>
        /// <returns></returns>
        public ActionResult MediaMaterialGet()
        {
            return View();
        }


        /// <summary>
        /// 修改永久素材
        /// </summary>
        /// <returns></returns>
        public ActionResult MediaPermanentUpdate()
        {
            return View();
        }


        /// <summary>
        /// 删除永久素材
        /// </summary>
        /// <returns></returns>
        public ActionResult MediaMaterialDelete()
        {
            return View();
        }


        /// <summary>
        /// 获取素材总数
        /// </summary>
        /// <returns></returns>
        public ActionResult MediaMaterialCount()
        {
            return View();
        }


        /// <summary>
        /// 获取素材列表
        /// </summary>
        /// <returns></returns>
        public ActionResult MediaMaterialBatch()
        {
            return View();
        }
    }
}