/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.WeChat
*项目描述:
*类 名 称:WeChatCryptography
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/8 10:40:25
*修 改 人:
*修改时间:
*作用描述: 消息加密和解密服务
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/
using AspNetMvc_WeChat_Base.FuncHelper;
using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace AspNetMvc_WeChat_Base.WeChat
{
    enum WeChatMsgCryptErrorCode
    {
        WeChatMsgCrypt_OK = 0,
        WeChatMsgCrypt_ValidateSignature_Error = -40001,// 签名验证错误
        WeChatMsgCrypt_ParseXml_Error = -40002,         // xml解析失败
        WeChatMsgCrypt_ComputeSignature_Error = -40003, // sha加密生成签名失败
        WeChatMsgCrypt_IllegalAesKey = -40004,          // AESKey 非法
        WeChatMsgCrypt_ValidateAppid_Error = -40005,    // appid 校验错误
        WeChatMsgCrypt_EncryptAES_Error = -40006,       // AES 加密失败
        WeChatMsgCrypt_DecryptAES_Error = -40007,       // AES 解密失败
        WeChatMsgCrypt_IllegalBuffer = -40008,          // 解密后得到的buffer非法
        WeChatMsgCrypt_EncodeBase64_Error = -40009,     // base64加密异常
        WeChatMsgCrypt_DecodeBase64_Error = -40010      // base64解密异常
    };

    public class WeChatCryptography
    {
        /// <summary>
        /// 微信公众平台的令牌
        /// </summary>
        readonly string crypt_Token;

        /// <summary>
        /// 消息加密密匙
        /// </summary>
        readonly string crypt_EncodingAESKey;

        /// <summary>
        /// 微信公众号AppID
        /// </summary>
        readonly string crypt_AppID;

        public WeChatCryptography(string token, string encdingAESKey, string appID)
        {
            crypt_AppID = appID;
            crypt_Token = token;
            crypt_EncodingAESKey = encdingAESKey;
        }

        /// <summary>
        /// 验证消息的真实性,并且获取解密后的明文
        /// </summary>
        /// <param name="sMsgSignature">签名字符串,对应URL参数的msg_signature</param>
        /// <param name="sTimeStamp">时间戳,对应URL参数的timestamp</param>
        /// <param name="sNonce">随机字符串,对应URL参数的nonce</param>
        /// <param name="sPostData">密文,对应POST请求的数据</param>
        /// <param name="sMsg">解密后的原文,当return返回0时有效</param>
        /// <returns></returns>
        public int DecryptMsg(string msgSignature, string msgTimestamp, string msgNonce, string msgPostData, ref string messge)
        {
            if (crypt_EncodingAESKey.Length != 43)
            {
                return (int)WeChatMsgCryptErrorCode.WeChatMsgCrypt_IllegalAesKey;
            }

            //获取微信平台发送的密文内容
            string msgEncrypt;
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(msgPostData);
                XmlNode root = xmlDocument.FirstChild;
                msgEncrypt = root["Encrypt"].InnerText;
            }
            catch (Exception)
            {
                return (int)WeChatMsgCryptErrorCode.WeChatMsgCrypt_ParseXml_Error;
            }

            //验证签名字符串
            int checkResult = CheckSignature(crypt_Token, msgTimestamp, msgNonce, msgEncrypt, msgSignature);
            if (checkResult != 0)
            {
                return checkResult;
            }

            //输出message解密信息
            string msgAppId = string.Empty;
            try
            {
                messge = Cryptography.AES_decrypt(msgEncrypt, crypt_EncodingAESKey, ref msgAppId);
            }
            catch (FormatException)
            {
                return (int)WeChatMsgCryptErrorCode.WeChatMsgCrypt_DecodeBase64_Error;
            }
            catch (Exception)
            {
                return (int)WeChatMsgCryptErrorCode.WeChatMsgCrypt_DecryptAES_Error;
            }
            if (msgAppId != crypt_AppID)
            {
                return (int)WeChatMsgCryptErrorCode.WeChatMsgCrypt_ValidateAppid_Error;
            }
            return 0;
        }

        /// <summary>
        /// 将企业号回复用户的消息加密打包
        /// </summary>
        /// <param name="replayMsg">企业号待回复用户的消息</param>
        /// <param name="msgTimestamp">时间戳,可以自己生成,也可以用URL参数的timestamp</param>
        /// <param name="msgNonce">随机字符串,可以自己生成,也可以用URL参数的nonce</param>
        /// <param name="msgEncrypt">加密后的可以直接回复用户的密文,包括msg_signature</param>
        /// <returns></returns>
        public int EncryptMsg(string msgReplay, string msgTimestamp, string msgNonce, ref string msgEncrypt)
        {
            if (crypt_EncodingAESKey.Length != 43)
            {
                return (int)WeChatMsgCryptErrorCode.WeChatMsgCrypt_IllegalAesKey;
            }

            //将回复的内容加密
            string encryptContent;
            try
            {
                encryptContent = Cryptography.AES_encrypt(msgReplay, crypt_EncodingAESKey, crypt_AppID);
            }
            catch (Exception)
            {
                return (int)WeChatMsgCryptErrorCode.WeChatMsgCrypt_EncryptAES_Error;
            }

            //验证签名字符串
            string msgSignature = string.Empty;
            int reuslt = GenarateSignature(crypt_Token, msgTimestamp, msgNonce, encryptContent, ref msgSignature);
            if (reuslt != 0)
                return reuslt;

            //输出带有xml格式的msgEncrypt加密后
            string nodeEncrypt = "<Encrypt><![CDATA[" + encryptContent + "]]></Encrypt>";
            string nodeMsgSignature = "<MsgSignature><![CDATA[" + msgSignature + "]]></MsgSignature>";
            string nodeTimestamp = "<TimeStamp><![CDATA[" + msgTimestamp + "]]></TimeStamp>";
            string nodewNonce = "<Nonce><![CDATA[" + msgNonce + "]]></Nonce>";
            msgEncrypt = "<xml>" + nodeEncrypt + nodeMsgSignature + nodeTimestamp + nodewNonce + "</xml>";
            return 0;
        }

        /// <summary>
        /// 检查签名字符串
        /// </summary>
        /// <param name="crypt_Token"></param>
        /// <param name="msgTimestamp"></param>
        /// <param name="msgNonce"></param>
        /// <param name="msgEncrypt"></param>
        /// <param name="msgSignature"></param>
        /// <returns></returns>
        private int CheckSignature(string crypt_Token, string msgTimestamp, string msgNonce, string msgEncrypt, string msgSignature)
        {
            string signatureHash = string.Empty;
            int reuslt = GenarateSignature(crypt_Token, msgTimestamp, msgNonce, msgEncrypt, ref signatureHash);
            if (reuslt != 0)
            {
                return reuslt;
            }
            if (signatureHash == msgSignature)
            {
                return 0;
            }
            else
            {
                return (int)WeChatMsgCryptErrorCode.WeChatMsgCrypt_ValidateSignature_Error;
            }
        }

        /// <summary>
        /// 创建验证性的字符串
        /// </summary>
        /// <param name="msgToken"></param>
        /// <param name="msgTimestamp"></param>
        /// <param name="msgNonce"></param>
        /// <param name="msgEncrypt"></param>
        /// <param name="msgSignature"></param>
        /// <returns></returns>
        private int GenarateSignature(string msgToken, string msgTimestamp, string msgNonce, string msgEncrypt, ref string msgSignature)
        {
            ArrayList arrayList = new ArrayList() {
                msgToken,msgTimestamp,msgNonce,msgEncrypt,msgSignature
            };
            arrayList.Sort(new DictionarySort());
            var strJoin = string.Join("", arrayList);

            string signatureHash;
            try
            {
                var shal = new SHA1CryptoServiceProvider();
                var encoding = new ASCIIEncoding();
                byte[] dataToHash = encoding.GetBytes(strJoin);
                byte[] dataHashed = shal.ComputeHash(dataToHash);
                signatureHash = BitConverter.ToString(dataHashed).Replace("-", "").ToLower();
            }
            catch (Exception)
            {
                return (int)WeChatMsgCryptErrorCode.WeChatMsgCrypt_ComputeSignature_Error;
            }
            msgSignature = signatureHash;
            return 0;
        }
    }

    /// <summary>
    /// 比较两个字符串的顺序
    /// </summary>
    internal class DictionarySort : IComparer
    {
        public int Compare(object x, object y)
        {
            string leftStr = x as string;
            string rightStr = y as string;
            int index = 0;
            while (index < leftStr.Length && index < rightStr.Length)
            {
                if (leftStr[index] < rightStr[index]) return -1;
                else if (leftStr[index] > rightStr[index]) return 1;
                else index++;
            }
            return leftStr.Length - rightStr.Length;

        }
    }
}
