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
            /*
             * mediaUpload.uploadFile 等价于 Request.Files[0] 均需要指定为 HttpPostedFileBase 对象
             * Request.Files.Count == 0 || Request.Files[0].ContentLength == 0 可以使用 
             * mediaUpload.uploadFile.FileName 替代
             */
            if (!string.IsNullOrEmpty(mediaUpload.uploadFile.FileName))
            {
                string path = Server.MapPath("/Media/temporary");
                string filePath = WeChatMediaService.UPloadPath(mediaUpload.uploadFile, path);
                string mediaJson = WeChatMediaService.AddTemporaryMedia("image", filePath);
                WeChatMediaUpResult mediaResult = JSONHelper.JSONToObject<WeChatMediaUpResult>(mediaJson);
                ViewBag.mediaTemporaryID = "Media_id:" + mediaResult.media_id;
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
        //[ValidateAntiForgeryToken]
        public ActionResult MediaTemporaryGet(FormCollection form)
        {
            //创建自己获取文件后需要保存到服务器位置的全路径
            string mediaid = form["media_id"];
            string savePath = Server.MapPath("~/Download/temporary/");
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmddfff") + ".jpg";
            string filepath = WeChatMediaService.GetFilePath(savePath, fileName);
            WeChatMediaService.GetTemporaryMedia(mediaid, filepath);
            Response.Redirect("~/Download/temporary/" + fileName);
            return View();
        }


        public ActionResult MediaPermanentNews()
        {
            return View();
        }

        /// <summary>
        /// 3.新增永久图文-素材
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MediaPermanentNews(WeChatMediaArticle article)
        {
            string modelArticle = "{\"articles\":[" + JSONHelper.ObjectToJSON(article) + "]}";
            string mediaJson = WeChatMediaService.AddMediaPermanent(modelArticle);
            WeChatMediaUpResult mediaUpResult = JSONHelper.JSONToObject<WeChatMediaUpResult>(mediaJson);
            ViewBag.mediaPermanentID = "Media_id:" + mediaUpResult.media_id;
            return View();
        }

        public ActionResult MediaPermanentCover()
        {
            return View();
        }

        /// <summary>
        /// 4.新增永久图文-素材封面
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MediaPermanentCover(WeChatMediaUpload mediaUpload)
        {
            /*
             * mediaUpload.uploadFile 等价于 Request.Files[0] 均需要指定为 HttpPostedFileBase 对象
             * Request.Files.Count == 0 || Request.Files[0].ContentLength == 0 可以使用 
             * mediaUpload.uploadFile.FileName 替代
             */
            if (!string.IsNullOrEmpty(mediaUpload.uploadFile.FileName))
            {
                string path = Server.MapPath("/Media/Permanent");
                string filePath = WeChatMediaService.UPloadPath(Request.Files[0], path);
                string mediaJson = WeChatMediaService.AddMediaPermanentCover("thumb", filePath);
                WeChatThumbReuslt mediaResult = JSONHelper.JSONToObject<WeChatThumbReuslt>(mediaJson);
                ViewBag.mediaThumbCoverID = "Media_id:" + mediaResult.thumb_media_id;
            }
            return View();

        }


        public ActionResult MediaPermanentNewImg()
        {
            return View();
        }

        /// <summary>
        /// 5.新增永久图文-素材图片
        /// </summary>
        /// <returns>上传图文消息内的图片获取URL</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MediaPermanentNewImg(WeChatMediaUpload mediaUpload)
        {
            /*
             * Request.Files.Count == 0 || Request.Files[0].ContentLength == 0 可以使用 
             * mediaUpload.uploadFile.FileName 替代
             */
            if (!string.IsNullOrEmpty(mediaUpload.uploadFile.FileName))
            {
                string path = Server.MapPath("~/Media/Permanent");
                /*
                 * mediaUpload.uploadFile 等价于 Request.Files[0] 均需要指定为 HttpPostedFileBase 对象
                 */
                string filePath = WeChatMediaService.UPloadPath(mediaUpload.uploadFile, path);
                string mediaJson = WeChatMediaService.AddMediaNewImg(filePath);
                WeChatUPloadimgResult uploadimgResult = JSONHelper.JSONToObject<WeChatUPloadimgResult>(mediaJson);
                ViewBag.uploadImgUrl = uploadimgResult.url;
            }
            return View();
        }

        public ActionResult MediaMaterialByType()
        {
            ViewBag.typeVideo = false;
            return View();
        }

        /// <summary>
        /// 6.新增其他永久素材
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MediaMaterialByType(WeChatMediaUpload mediaUpload)
        {
            /*
             * Request.Files.Count == 0 || Request.Files[0].ContentLength == 0 可以使用 
             * mediaUpload.uploadFile.FileName 替代
             * mediaUpload.uploadFile 等价于 Request.Files[0] 均需要指定为 HttpPostedFileBase 对象
            */
            if (!string.IsNullOrEmpty(mediaUpload.uploadFile.FileName))
            {
                string path = Server.MapPath("~/Media/Permanent");
                string filePath = WeChatMediaService.UPloadPath(mediaUpload.uploadFile, path);
                string mediaJson = WeChatMediaService.AddMediaMaterial(mediaUpload.materialType.ToString(), filePath);
                WeChatAddMaterialResult uploadimgResult = JSONHelper.JSONToObject<WeChatAddMaterialResult>(mediaJson);
                ViewBag.media_id = "media_id:" + uploadimgResult.media_id;
                ViewBag.url = "url" + uploadimgResult.url;
            }
            ViewBag.typeVideo = true;
            return View();
        }


        public ActionResult MediaMaterialGet()
        {
            return View();
        }
        /// <summary>
        /// 7.获取永久素材
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult MediaMaterialGet(mediaIDParam param)
        {
            string savePath = Server.MapPath("~/Download/Permanent/");
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            string filePath = WeChatMediaService.GetFilePath(savePath, fileName);
            string mediaJson = JSONHelper.ObjectToJSON(param);
            WeChatMediaService.GetMediaMaterial(mediaJson, filePath);
            Response.Redirect("~/Download/Permanent/" + fileName);
            return View();
        }


        /// <summary>
        /// 8.修改永久素材
        /// </summary>
        /// <returns></returns>
        public ActionResult MediaMaterialUpdate()
        {
            return View();
        }


        public ActionResult MediaMaterialDelete()
        {
            return View();
        }

        /// <summary>
        /// 9.删除永久素材
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult MediaMaterialDelete(mediaIDParam param)
        {
            var mediaJson = JSONHelper.ObjectToJSON(param);
            WeChatErrorResult result = JSONHelper.JSONToObject<WeChatErrorResult>(WeChatMediaService.DeleteMaterial(mediaJson));
            ViewBag.deleteResult = result.errcode == "0" ? "删除操作成功" : "删除操作失败";
            return View();
        }

        /// <summary>
        /// 10.获取素材总数
        /// </summary>
        /// <returns></returns>
        
        public ActionResult MediaMaterialCount()
        {
            string resultJson = WeChatMediaService.GetMaterialCount();
            WeChatMediaCount mediaCount = JSONHelper.JSONToObject<WeChatMediaCount>(resultJson);
            ViewBag.MediaCount = "您的公众号共有:<br />" +
                mediaCount.voice_count + "个语音消息<br/>" +
                mediaCount.video_count + "个视频消息<br />" +
                mediaCount.image_count + "个图片消息<br />" +
                mediaCount.news_count + "个图文消息<br />";
            return View();
        }


        /// <summary>
        /// 11.获取素材列表
        /// </summary>
        /// <returns></returns>
        public ActionResult MediaMaterialList()
        {
            WeChatMediaListInput mediaListInput = new WeChatMediaListInput()
            {
                count = 10,
                offset = 0,
                type = "image"
            };
            string paramJson = JSONHelper.ObjectToJSON(mediaListInput);
            string resultJson = WeChatMediaService.GetMaterialList(paramJson);
            WeChatMaterialList materialList = JSONHelper.JSONToObject<WeChatMaterialList>(resultJson);
            ViewBag.listShow = "您的公众号共有: <b>" +
                materialList.total_count + " </b>个图片素材.其中:<br /><b> " +
                materialList.item_count + " </b>个素材具体信息被获取:<br /><br />";
            for (int i = 0; i < materialList.item.Count; i++)
            {
                ViewBag.listShow += "素材Id：" + materialList.item[i].media_id + "<br />";
                ViewBag.listShow += "素材名称：" + materialList.item[i].name + "<br />";
                ViewBag.listShow += "素材最后更新：" + materialList.item[i].update_time + "<br />";
                ViewBag.listShow += "url：" + materialList.item[i].url + "<br /><br />";
            }
            return View();
        }
    }
}