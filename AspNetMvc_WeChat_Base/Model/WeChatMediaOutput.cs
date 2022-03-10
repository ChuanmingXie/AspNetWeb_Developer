/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.Model
*项目描述:
*类 名 称:WeChatMediaOutput
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/10 22:47:35
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetMvc_WeChat_Base.Model
{
    /// <summary>
    /// 输出素材总数的Json对象
    /// </summary>
    public class WeChatMediaCount
    {
        [Display(Name ="音频数")]
        public int voice_count;

        [Display(Name ="视频数")]
        public int video_count;

        [Display(Name ="图片数")]
        public int image_count;

        [Display(Name ="新闻数")]
        public int news_count;
    }

    #region 图文素材模型

    /// <summary>
    /// 返回图文素材的Json对象
    /// </summary>
    public class WeChatNewsList
    {
        public int  total_count;

        public int item_count;

        public List<WeChatNewItem> item;

    }

    /// <summary>
    /// 图文素材的元素对象
    /// </summary>
    public class WeChatNewItem
    {
        public string media_id;

        public List<ContentNewsItem> content;

        public DateTime update_time;
    }

    /// <summary>
    /// 图文素材元素对象的内容元素
    /// </summary>
    public class ContentNewsItem
    {
        public List<WeChatMediaArticle> news_item;
    }

    #endregion

    #region 语音|图片|视频素材模型

    /// <summary>
    /// 返回 语音 图片 视频 素材的Json对象
    /// </summary>
    public class WeChatMaterialList
    {
        public int total_count;

        public int item_count;

        public List<WeChatMaterialItem> item;
    }

    /// <summary>
    /// 语音 图片 视频 素材的元素对象
    /// </summary>
    public class WeChatMaterialItem
    {
        public string media_id;

        public string name;

        public string update_time;

        public string url;
    }
    #endregion
}
