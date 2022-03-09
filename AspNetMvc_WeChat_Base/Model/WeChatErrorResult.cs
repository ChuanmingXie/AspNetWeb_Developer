/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.Model
*项目描述:
*类 名 称:WeChatResult
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/6 11:53:19
*修 改 人:
*修改时间:
*作用描述: 创建与接口获取状态(成功或失败)有关的Model
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetMvc_WeChat_Base.Model
{
    public class WeChatErrorResult
    {
        /// <summary>
        /// 字段名为特定的 JSON 键值的键，不可变更
        /// 错误代码
        /// </summary>
        public string errcode;

       /// <summary>
       /// 错误信息
       /// </summary>
        public string errmsg;
    }
}
