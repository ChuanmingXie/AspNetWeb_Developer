/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.Model
*项目描述:
*类 名 称:WeChatMessageOutput
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/12 8:30:01
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

namespace AspNetMvc_WeChat_Base.Model
{
    public class WeChatMsgIndustry 
    {
        public WeChatIndustry primary_industry;
        public WeChatIndustry secondary_industry;
    }

    public class WeChatIndustry
    {
        public string first_class;
        public string second_class;
    }

    public class WeChatMsgTemplateResult
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public string errcode;

        /// <summary>
        /// 返回信息
        /// </summary>
        public string errmsg;

        /// <summary>
        /// 模板id
        /// </summary>
        public string template_id;
    }

    public class WeChatTemplateList
    {
        public List<WeChatTemplateItem> template_list;
    }

    public class WeChatTemplateItem
    {
        /// <summary>
        /// 模板id
        /// </summary>
        public string template_id;
        /// <summary>
        /// 模板标题
        /// </summary>
        public string title;
        /// <summary>
        /// 模板所属主行业
        /// </summary>
        public string primary_industry;
        /// <summary>
        /// 模板所属副业
        /// </summary>
        public string deputy_industary;
        /// <summary>
        /// 模板内容
        /// </summary>
        public string content;
        /// <summary>
        /// 模板示例
        /// </summary>
        public string example;
    }
    
}
