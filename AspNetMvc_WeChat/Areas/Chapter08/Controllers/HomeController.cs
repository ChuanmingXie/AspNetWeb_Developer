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
        /// 新增临时素材
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MediaTemporary(FormCollection form)
        {
            if (Request.Files.Count == 0||Request.Files[0].ContentLength==0)
            {
                return View();
            }
            else
            {
                HttpPostedFileBase fileBaseUpload = Request.Files[0];
                int index = Request.Files[0].FileName.LastIndexOf('.');
                string fielExt = Request.Files[0].FileName
                    .Substring(index, Request.Files[0].FileName.Length - index);
                string newFile = DateTime.Now.ToString("yyyyMMddHHmmss") + fielExt;
                string path = Server.MapPath("/Media");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                path += "\\" + newFile;
                fileBaseUpload.SaveAs(path);

                string mediaJson = WeChatMediaService.MediaUPloadParam("image", path);
                WeChatMediaUpResult mediaResult = JSONHelper.JSONToObject<WeChatMediaUpResult>(mediaJson);
                ViewBag.mediaTemporaryID = "媒体id:" + mediaResult.Media_id;
                return View();
            }
        }
        
        public ActionResult MediaTemporaryGet()
        {
            return View();
        }

        /// <summary>
        /// 获取临时素材
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MediaTemporaryGet(FormCollection form)
        {
            string image_dir = Server.MapPath("~/images/");
            if (!Directory.Exists(image_dir))
                Directory.CreateDirectory(image_dir);
            string mediaid = form["Mideiaid"];
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmddfff") + ".jpg";
            string path = image_dir + "\\" + fileName;
            WeChatMediaService.MediaTempGet(mediaid, path);
            Response.Redirect("~/images/" + fileName);
            return View();
        }

        public ActionResult MediaPermanent()
        {
            return View();
        }
    }
}