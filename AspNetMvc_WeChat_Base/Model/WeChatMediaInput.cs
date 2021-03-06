/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.Model
*项目描述:
*类 名 称:WeChatMediaArticle
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/9 23:29:33
*修 改 人:
*修改时间:
*作用描述：素材管理提交参数，包括图文素材定义，上传内容参数定义
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AspNetMvc_WeChat_Base.Model
{
    /// <summary>
    /// 获取素材时提供media_id参数
    /// </summary>
    public class mediaIDParam
    {
        public string media_id { get; set; }
    }

    /// <summary>
    /// 永久图文素材-参数定义(需转化为Json)
    /// </summary>
    public class WeChatMediaArticle
    {
        [Display(Name ="图文标题")]
        public string title { get; set; }

        [Display(Name = "封面thumb Id(需先上传封面)")]
        public string thumb_media_id { get; set; }

        [Display(Name = "作者")]
        public string author { get; set; }

        [Display(Name = "数字签名")]
        public string digest { get; set; }

        //[Display(Name = "是否显示封面")]
        //public int show_cover_pic { get; set; }

        [Display(Name = "内容")]
        public string content { get; set; }

        [Display(Name = "图文援引")]
        public string content_source_url { get; set; }

        public int need_open_comment { get; set; } = 1;

        public int only_fans_can_comment { get; set; } = 1;

    }

    /// <summary>
    /// 永久其他素材-视频描述参数定义(需转化为Json)
    /// </summary>
    public class WeChatMaterialVideo
    {
        [Display(Name ="视频标题")]
        public string title { get; set; }

        [Display(Name ="视频描述")]
        public string introduction { get; set; }

        //"title":VIDEO_TITLE,
        //"introduction":INTRODUCTION
    }
        

    /// <summary>
    /// 上传文件对象参数定义
    /// </summary>
    public class WeChatMediaUpload
    {
        public MaterialType materialType { get; set; }

        public HttpPostedFileBase uploadFile { get; set; }

        public WeChatMaterialVideo materialVideo { get; set; }
    }

    public enum MaterialType
    {
        [Display(Name ="图片")]
        image,
        [Display(Name = "语音")]
        voice,
        [Display(Name = "视频")]
        video,
        [Display(Name = "封面")]
        thumb
    }

    /// <summary>
    /// 获取永久素材参数(需转化为Json)
    /// </summary>
    public class WeChatMediaListInput
    {
        public string type { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
    }
}
