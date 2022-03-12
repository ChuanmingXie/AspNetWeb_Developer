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
}
