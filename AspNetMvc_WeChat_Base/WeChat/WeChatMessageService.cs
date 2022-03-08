/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.WeChat
*项目描述:
*类 名 称:WeChatMessageService
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/7 17:19:51
*修 改 人:
*修改时间:
*作用描述:根据数据类型的不同返回不同的接口调试日志信息
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/
using AspNetMvc_WeChat_Base.Logging;
using AspNetMvc_WeChat_Base.Model;
using System;
using System.Web;

namespace AspNetMvc_WeChat_Base.WeChat
{
    public static class WeChatMessageService
    {
        /// <summary>
        /// 用于记录显示粉丝发送的数据内容
        /// </summary>
        /// <param name="weChatData"></param>
        public static void ShowMessage(WeChatMessage weChatData)
        {
            switch (weChatData.MsgType.ToLower())
            {
                case "text":
                    LogService.RecordLog("\r\n收到来自【" + weChatData.FromUserName + "】的消息。\n" +
                        "消息类型：" + weChatData.MsgType + ",\n" +
                        "消息id：" + weChatData.MsgId + ",\n" +
                        "消息内容：" + weChatData.Content + ",\n" +
                        "消息时间：" + weChatData.CreateTime); break;
                case "image":
                    LogService.RecordLog("\r\n收到来自【" + weChatData.FromUserName + "】的消息。\n" +
                        "消息类型：" + weChatData.MsgType + ",\n" +
                        "消息id：" + weChatData.MsgId + ",\n" +
                        "图片地址：" + weChatData.PicUrl + ",\n" +
                        "媒体id：" + weChatData.Media_id + ",\n" +
                        "消息时间：" + weChatData.CreateTime); break;
                case "voice":
                    LogService.RecordLog("\r\n收到来自【" + weChatData.FromUserName + "】的消息。\n" +
                        "消息类型：" + weChatData.MsgType + ",\n" +
                        "消息id：" + weChatData.MsgId + ",\n" +
                        "音频格式：" + weChatData.Format + ",\n" +
                        "媒体id：" + weChatData.Media_id + ",\n" +
                        "消息时间：" + weChatData.CreateTime); break;
                case "video":
                case "shortvideo":
                    LogService.RecordLog("\r\n收到来自【" + weChatData.FromUserName + "】的消息。\n" +
                        "消息类型：" + weChatData.MsgType + ",\n" +
                        "消息id：" + weChatData.MsgId + ",\n" +
                        "ThumdMediaId：" + weChatData.ThumdMediaId + ",\n" +
                        "媒体id：" + weChatData.Media_id + ",\n" +
                        "消息时间：" + weChatData.CreateTime); break;
                case "location":
                    LogService.RecordLog("\r\n收到来自【" + weChatData.FromUserName + "】的消息。\n" +
                        "消息类型：" + weChatData.MsgType + ",\n" +
                        "消息id：" + weChatData.MsgId + ",\n" +
                        "地点信息：" + weChatData.Label + "(" + weChatData.Location_X + "," + weChatData.Location_Y + ")\n" +
                        "地图缩放：" + weChatData.Scale + ",\n" +
                        "消息时间：" + weChatData.CreateTime); break;
                case "link":
                    LogService.RecordLog("\r\n收到来自【" + weChatData.FromUserName + "】的消息。\n" +
                        "消息类型：" + weChatData.MsgType + ",\n" +
                        "消息id：" + weChatData.MsgId + ",\n" +
                        "标题：" + weChatData.Title + ",\n" +
                        "消息描述：" + weChatData.Scale + ",\n" +
                        "消息地址：" + weChatData.Url + ",\n" +
                        "消息时间：" + weChatData.CreateTime); break;
                case "event":
                    switch (weChatData.Event.ToLower())
                    {
                        case "subscribe":
                            LogService.RecordLog("\r\n收到来自【" + weChatData.FromUserName + "】的消息。\n" +
                                "消息类型：" + weChatData.MsgType + ",\n" +
                                "消息id：" + weChatData.MsgId + ",\n" +
                                "事件类型：" + weChatData.Event + ",\n" +
                                "消息时间：" + weChatData.CreateTime); break;
                        case "scan":
                        case "unsubscribe":
                            if (!string.IsNullOrEmpty(weChatData.EventKey) && weChatData.EventKey.Contains("qrscene"))
                                LogService.RecordLog("\r\n收到来自【" + weChatData.FromUserName + "】的消息。\n" +
                                    "消息类型：" + weChatData.MsgType + ",\n" +
                                    "消息id：" + weChatData.MsgId + ",\n" +
                                    "事件类型：" + weChatData.Event + ",\n" +
                                    "事件标识：" + weChatData.EventKey + ",\n" +
                                    "二维码Ticket：" + weChatData.Ticket + ",\n" +
                                    "消息时间：" + weChatData.CreateTime);
                            else
                                LogService.RecordLog("\r\n收到来自【" + weChatData.FromUserName + "】的消息。\n" +
                                        "消息类型：" + weChatData.MsgType + ",\n" +
                                        "消息id：" + weChatData.MsgId + ",\n" +
                                        "事件类型：" + weChatData.Event + ",\n" +
                                        "消息时间：" + weChatData.CreateTime);
                            break;
                        case "location":
                            LogService.RecordLog("\r\n收到来自【" + weChatData.FromUserName + "】的消息。\n" +
                                "消息类型：" + weChatData.MsgType + ",\n" +
                                "消息id：" + weChatData.MsgId + ",\n" +
                                "事件类型：" + weChatData.Event + ",\n" +
                                "位置精度：" + weChatData.Precision + ",\n" +
                                "地点信息：" + "(" + weChatData.Latitude + ",\n" + weChatData.Longitude + ")" +
                                "消息时间：" + weChatData.CreateTime); break;
                        case "click":
                        case "view":
                            LogService.RecordLog("\r\n收到来自【" + weChatData.FromUserName + "】的消息。\n" +
                                "消息类型：" + weChatData.MsgType + ",\n" +
                                "消息id：" + weChatData.MsgId + ",\n" +
                                "事件类型：" + weChatData.Event + ",\n" +
                                "事件标识：" + weChatData.EventKey + ",\n" +
                                "消息时间：" + weChatData.CreateTime); break;
                    }
                    break;
            }
        }

        /// <summary>
        /// 用于根据粉丝的留言测试回复消息
        /// </summary>
        /// <param name="messageType"></param>
        public static void ReplyMessage(WeChatMessage weChatData)
        {
            switch (weChatData.Content)
            {
                case "文本":SendTextMessage(weChatData.FromUserName, "您好,新时代中国特色社会主义国家永远是人民群众的国家"); break;
                case "图片":SendImageMessage(weChatData.FromUserName, "Vl8EK2Lavr0p20sWiCpu1mEiGB00AwZK60Xzv9z3ByNAGv_xwClGmWDES2Q8Q6Sh"); break;
                case "语音": SendVoiceMessage(weChatData.FromUserName, ""); break;
                case "视频":SendVideoMessage(weChatData.FromUserName, "", "视频", "回复的视频"); break;
                case "音乐":SendMusicMessage(weChatData.FromUserName, "九州同", "中国风音乐"
                    , "http://101.132.152.252/Media/%E4%B9%9D%E5%B7%9E%E5%90%8C.mp3"
                    , "http://101.132.152.252/Media/%E4%B9%9D%E5%B7%9E%E5%90%8C.mp3"
                    , "zetp9PuX5LisLjqXI8ZKHOiQ2Sr7uB6U6MbtWQK2QEZzcflSSDMEjnopEIb19Nwj"); break;
            }
        }

        /// <summary>
        /// 发送文本消息
        /// </summary>
        public static void SendTextMessage(string toUserName, string content)
        {
            string FromUserName = "gh_ec5776f68745";
            string xmlMsg = "<xml>" +
                "<ToUserName><![CDATA[" + toUserName + "]]></ToUserName>" +
                "<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>" +
                "<CreateTime>" + GetCreateTime() + "</CreateTime>" +
                "<MsgType><![CDATA[text]]></MsgType>" +
                "<Content><![CDATA[" + content + "]]></Content>" +
                "</xml> ";
            SendMessage(xmlMsg);
        }

        /// <summary>
        /// 回复图片消息
        /// </summary>
        /// <param name="toUserName"></param>
        /// <param name="media_id"></param>
        public static void SendImageMessage(string toUserName, string media_id)
        {
            string FromUserName = "gh_ec5776f68745";
            string xmlMsg = "<xml>" +
                "<ToUserName><![CDATA[" + toUserName + "]]></ToUserName>" +
                "<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>" +
                "<CreateTime>" + GetCreateTime() + "</CreateTime>" +
                "<MsgType><![CDATA[image]]></ MsgType>" +
                "<Image><MediaId><![CDATA[" + media_id + "]]></MediaId></Image>" +
                "</xml> ";
            SendMessage(xmlMsg);
        }

        /// <summary>
        /// 回复语音消息
        /// </summary>
        /// <param name="toUserName"></param>
        /// <param name="media_id"></param>
        public static void SendVoiceMessage(string toUserName, string media_id)
        {
            string FromUserName = "gh_ec5776f68745";
            string xmlMsg = "<xml>" +
                "<ToUserName><![CDATA[" + toUserName + "]]></ToUserName>" +
                "<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>" +
                "<CreateTime>" + GetCreateTime() + "</CreateTime>" +
                "<MsgType><![CDATA[voice]]></ MsgType>" +
                "<Voice><MediaId><![CDATA[" + media_id + "]]></MediaId></Voice>" +
                "</xml> ";
            SendMessage(xmlMsg);
        }

        /// <summary>
        /// 回复视频消息
        /// </summary>
        /// <param name="toUserName"></param>
        /// <param name="media_id"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        public static void SendVideoMessage(string toUserName, string media_id
            , string title, string description)
        {
            string FromUserName = "gh_ec5776f68745";
            string xmlMsg = "<xml>" +
                "<ToUserName><![CDATA[" + toUserName + "]]></ToUserName>" +
                "<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>" +
                "<CreateTime>" + GetCreateTime() + "</CreateTime>" +
                "<MsgType><![CDATA[video]]></ MsgType>" +
                "<Video>" +
                    "<MediaId><![CDATA[" + media_id + "]]></MediaId>" +
                    "<Title><![CDATA[" + title + "]]></Title>" +
                    "<Description><![CDATA[" + description + "]]></Description>" +
                "</Video>" +
                "</xml> ";
            SendMessage(xmlMsg);
        }

        /// <summary>
        /// 回复音乐消息
        /// </summary>
        /// <param name="toUserName"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="musicUrl"></param>
        /// <param name="hQMusicUrl"></param>
        /// <param name="thumbMedia"></param>
        public static void SendMusicMessage(string toUserName, string title
            , string description, string musicUrl, string hQMusicUrl, string thumbMedia)
        {
            string FromUserName = "gh_ec5776f68745";
            string xmlMsg = "<xml>" +
                "<ToUserName><![CDATA[" + toUserName + "]]></ToUserName>" +
                "<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>" +
                "<CreateTime>" + GetCreateTime() + "</CreateTime>" +
                "<MsgType><![CDATA[music]]></ MsgType>" +
                "<Music>" +
                    "<Title><![CDATA[" + title + "]]></Title>" +
                    "<Description><![CDATA[" + description + "]]></Description>" +
                    "<MusicUrl><![CDATA[" + musicUrl + "]]></MusicUrl>" +
                    "<HQMusicUrl><![CDATA[" + hQMusicUrl + "]]></HQMusicUrl>" +
                    "<ThumbMedia><![CDATA[" + thumbMedia + "]]></ThumbMedia>" +
                "</Music>" +
                "</xml> ";
            SendMessage(xmlMsg);
        }

        /// <summary>
        /// 创建事件事件
        /// </summary>
        /// <returns></returns>
        private static int GetCreateTime()
        {
            DateTime startDate = new DateTime(1970, 1, 1, 8, 0, 0);
            return (int)(DateTime.Now - startDate).TotalSeconds;
        }

        /// <summary>
        /// 执行消息发送
        /// </summary>
        /// <param name="xmlMsg"></param>
        private static void SendMessage(string xmlMsg)
        {
            LogService.RecordLog("回复消息:" + xmlMsg);
            HttpContext httpContext = HttpContext.Current;
            httpContext.Response.Write(xmlMsg);
        }
    }
}
