/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.WeChat
*项目描述:
*类 名 称:WeChatMessageService
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/7 17:19:51
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/
using AspNetMvc_WeChat_Base.Logging;
using AspNetMvc_WeChat_Base.Model;

namespace AspNetMvc_WeChat_Base.WeChat
{
    public static class WeChatMessageService
    {
        public static void ShowMessage(WeChatMessage weChatData)
        {
            switch (weChatData.MsgType.ToLower())
            {
                case "text":
                    LogService.RecordLog("\r\n收到来自【" + weChatData.FromUserName + "】的消息。" +
                        "消息类型：" + weChatData.MsgType + "," +
                        "消息id：" + weChatData.MsgId + "," +
                        "消息内容：" + weChatData.Content +
                        "消息时间：" + weChatData.CreateTime); break;
                case "image":
                    LogService.RecordLog("\r\n收到来自【" + weChatData.FromUserName + "】的消息。" +
                        "消息类型：" + weChatData.MsgType + "," +
                        "消息id：" + weChatData.MsgId + "," +
                        "图片地址：" + weChatData.PicUrl + "," +
                        "媒体id：" + weChatData.Media_id + "," +
                        "消息时间：" + weChatData.CreateTime); break;
                case "voice":
                    LogService.RecordLog("\r\n收到来自【" + weChatData.FromUserName + "】的消息。" +
                        "消息类型：" + weChatData.MsgType + "," +
                        "消息id：" + weChatData.MsgId + "," +
                        "音频格式：" + weChatData.Format + "," +
                        "媒体id：" + weChatData.Media_id + "," +
                        "消息时间：" + weChatData.CreateTime); break;
                case "video":
                case "shortvideo":
                    LogService.RecordLog("\r\n收到来自【" + weChatData.FromUserName + "】的消息。" +
                        "消息类型：" + weChatData.MsgType + "," +
                        "消息id：" + weChatData.MsgId + "," +
                        "ThumdMediaId：" + weChatData.ThumdMediaId + "," +
                        "媒体id：" + weChatData.Media_id + "," +
                        "消息时间：" + weChatData.CreateTime); break;
                case "location":
                    LogService.RecordLog("\r\n收到来自【" + weChatData.FromUserName + "】的消息。" +
                        "消息类型：" + weChatData.MsgType + "," +
                        "消息id：" + weChatData.MsgId + "," +
                        "地点信息：" + weChatData.Label + "(" + weChatData.Location_X + "," + weChatData.Location_Y + ")" +
                        "地图缩放：" + weChatData.Scale + "," +
                        "消息时间：" + weChatData.CreateTime); break;
                case "link":
                    LogService.RecordLog("\r\n收到来自【" + weChatData.FromUserName + "】的消息。" +
                        "消息类型：" + weChatData.MsgType + "," +
                        "消息id：" + weChatData.MsgId + "," +
                        "标题：" + weChatData.Title + "," +
                        "消息描述：" + weChatData.Scale + "," +
                        "消息地址：" + weChatData.Url + "," +
                        "消息时间：" + weChatData.CreateTime); break;
                case "event":
                    LogService.RecordLog("\r\n收到来自【" + weChatData.FromUserName + "】的消息。" +
                        "消息类型：" + weChatData.MsgType + "," +
                        "消息id：" + weChatData.MsgId + "," +
                        "事件类型：" + weChatData.Event + "," +
                        "消息时间：" + weChatData.CreateTime); break;
            }
        }
    }
}
