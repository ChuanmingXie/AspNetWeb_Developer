/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.WeChat
*项目描述:
*类 名 称:WeChatBeginAPI
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/7 14:32:51
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

namespace AspNetMvc_WeChat_Base.WeChat
{
    public class WeChatBeginAPI
    {
        public string EchoStr { get; set; }

        public string Signature { get; set; }

        public string SignatureTemp { get; set; }

        public string Timestamp { get; set; }

        public string Nonce { get; set; }

        //public string Encrypt_Type { get; set; }
    }
}
