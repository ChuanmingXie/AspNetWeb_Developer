/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.Model
*项目描述:
*类 名 称:WeChatMediaArticle
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/9 23:29:33
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AspNetMvc_WeChat_Base.Model
{
    public class WeChatMediaArticle
    {
        public string title { get; set; }

        public string thumb_media_id { get; set; }

        public string author { get; set; }

        public string digest { get; set; }

        public string show_cover_pic { get; set; }

        public string content { get; set; }

        public string content_source_url { get; set; }

    }
    public class WeChatMediaUpload
    {
        public HttpPostedFileBase uploadFile { get; set; }
    }

}
