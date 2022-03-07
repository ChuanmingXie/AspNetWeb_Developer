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

namespace AspNetMvc_WeChat_Base.WeChat
{
    public static class WeChatMessageService
    {
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
    }
}
