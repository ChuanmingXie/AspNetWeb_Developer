/*****************************************************************************
*项目名称:AspNetMvc_WeChat.Models
*项目描述:
*类 名 称:People
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/3 17:16:11
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMvc_WeChat.Models
{
    public class People
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
    }
}