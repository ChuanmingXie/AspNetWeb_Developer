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
        /// <summary>
        /// 测试请求的参数
        /// </summary>
        public string EchoStr { get; set; }

        /// <summary>
        /// 明文模式下的原始签名
        /// </summary>
        public string SignatureOrigin { get; set; }

        /// <summary>
        /// 明文模式下的签名验证(自行生成)
        /// </summary>
        public string SignatureConfirm { get; set; } = string.Empty;

        /// <summary>
        /// 事件戳
        /// </summary>
        public string Timestamp { get; set; }

        /// <summary>
        /// 安全模式下的加密类型AES
        /// </summary>
        public string Encrypt_Type { get; set; }

        /// <summary>
        /// 安全模式下的原始加密签名
        /// </summary>
        public string Msg_SignatureOrigin { get; set; }

        /// <summary>
        /// 安全模式下的加密签名验证(自行生成)
        /// </summary>
        public string Msg_SignatureConfirm { get; set; } = string.Empty;

        public string Nonce { get; set; }
    }
}
