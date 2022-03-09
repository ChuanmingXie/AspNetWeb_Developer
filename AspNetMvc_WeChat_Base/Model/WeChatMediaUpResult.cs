﻿/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.Model
*项目描述:
*类 名 称:WeChatMediaUpResult
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/9 14:38:38
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
    public  class WeChatMediaUpResult
    {
        /// <summary>
        /// 新增素材的类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 新增素材的唯一标识
        /// </summary>
        public string Media_id { get; set; }

        /// <summary>
        /// 新增素材的时间戳
        /// </summary>
        public int Created_at { get; set; }
    }
}