﻿/*****************************************************************************
*项目名称:AspNetMvc_WeChat_Base.Model
*项目描述:
*类 名 称:WeChatMessage
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/3/6 22:35:44
*修 改 人:
*修改时间:
*作用描述:创建与消息传递有关的Model
*Copyright @ Chuanming 2022. All rights reserved
******************************************************************************/
using AspNetMvc_WeChat_Base.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AspNetMvc_WeChat_Base.Model
{
    public class WeChatMessage
    {
        /// <summary>
        /// 消息接收方微信号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 消息发送方微信号
        /// </summary>
        public string FromUserName { get; set; }

        public string Encrypt { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 信息类型,取值:地理位置：location；文本消息：text；图片消息：image
        /// </summary>
        public string MsgType { get; set; }
        /// <summary>
        /// 信息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 消息ID
        /// </summary>
        public string MsgId { get; set; }


        /// <summary>
        /// 开发链接,开发者可用HTTP,GET获取
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 图片消息媒体id|语音消息id|,可以调用第8章的多媒体文件下载获取数据
        /// </summary>
        public string Media_id { get; set; }


        /// <summary>
        /// 描述语音的格式
        /// </summary>
        public string Format { get; set; }


        /// <summary>
        /// 视频的封面媒体id，可以调用第8章的多媒体文件下载获取数据
        /// </summary>
        public string ThumdMediaId { get; set; }

        /// <summary>
        /// 地理位置维度
        /// </summary>
        public string Location_X { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Location_Y { get; set; }
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public string Scale { get; set; }
        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }


        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; }


        /// <summary>
        /// 事件类型 取值：
        /// subscribe(扫描带参数二维码订阅)
        /// unsubscribe(取消订阅)
        /// CLICK(自定义菜单点击事件)
        /// SCAN(已关注的状态下扫描带参数二维码)
        /// </summary>
        public string Event { get; set; }
        /// <summary>
        /// 扫描二维码的参数
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 二维码图片
        /// </summary>
        public string Ticket { get; set; }
        /// <summary>
        /// 自动更新地理位置，传递参数维度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// 自动更新地理位置，传递参数经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 地理位置精度
        /// </summary>
        public string Precision { get; set; }

        /// <summary>
        /// 用于解析接收到的XML文件为信息字符串
        /// </summary>
        /// <param name="requestXML"></param>
        public void XMLToMessage(string requestXML)
        {
            if (!string.IsNullOrEmpty(requestXML))
            {
                try
                {
                    requestXML = requestXML.Replace("< ", "<").Replace(" >", ">").Replace("/ ", "/");
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(requestXML);
                    XmlElement rootElement = xmlDocument.DocumentElement;
                    CollectNodeElement(rootElement);
                }
                catch (Exception ex)
                {
                    LogService.RecordLog("接收XML文件并转换失败:\n" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 收集XML文件的数据节点
        /// </summary>
        /// <param name="rootElement"></param>
        private void CollectNodeElement(XmlElement rootElement)
        {
            try
            {
                ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
                Encrypt = WhenExict(rootElement, "Encrypt") ? rootElement.SelectSingleNode("Encrypt").InnerText : "";
                FromUserName = WhenExict(rootElement, "FromUserName") ? rootElement.SelectSingleNode("FromUserName").InnerText : "";
                CreateTime = TimeCorrect(WhenExict(rootElement, "CreateTime") ? rootElement.SelectSingleNode("CreateTime").InnerText : "0");
                MsgType = WhenExict(rootElement, "MsgType") ? rootElement.SelectSingleNode("MsgType").InnerText : "";
                MsgId = WhenExict(rootElement, "MsgId") ? rootElement.SelectSingleNode("MsgId").InnerText : "";
                switch (MsgType)
                {
                    case "text": Content = rootElement.SelectSingleNode("Content").InnerText; break;
                    case "image": PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText; break;
                    //Media_id = rootElement.SelectSingleNode("MediaId").InnerText;  图片的接口调试参数中没有媒体id
                    case "voice":
                        Format = rootElement.SelectSingleNode("Format").InnerText;
                        Media_id = rootElement.SelectSingleNode("MediaId").InnerText; break;
                    case "video":
                    case "shortvideo":
                        Media_id = rootElement.SelectSingleNode("MediaId").InnerText;
                        ThumdMediaId = rootElement.SelectSingleNode("ThumbMediaId").InnerText; break;
                    case "location":
                        Location_X = rootElement.SelectSingleNode("Location_X").InnerText;
                        Location_Y = rootElement.SelectSingleNode("Location_Y").InnerText;
                        Scale = rootElement.SelectSingleNode("Scale").InnerText;
                        Label = rootElement.SelectSingleNode("Label").InnerText; break;
                    case "link":
                        Title = rootElement.SelectSingleNode("Title").InnerText;
                        Description = rootElement.SelectSingleNode("Description").InnerText;
                        Url = rootElement.SelectSingleNode("Url").InnerText; break;
                    case "event":
                        Event = rootElement.SelectSingleNode("Event").InnerText;
                        EventKey = rootElement.SelectSingleNode("EventKey").InnerText;
                        Ticket = WhenExict(rootElement, "Ticket") ? rootElement.SelectSingleNode("Ticket").InnerText : "";
                        Latitude = WhenExict(rootElement, "Latitude") ? rootElement.SelectSingleNode("Latitude").InnerText : "";
                        Longitude = WhenExict(rootElement, "Longitude") ? rootElement.SelectSingleNode("Longitude").InnerText : "";
                        Precision = WhenExict(rootElement, "Precision") ? rootElement.SelectSingleNode("Precision").InnerText : "";
                        break;
                }
            }
            catch (Exception ex)
            {
                LogService.RecordLog("消息数据字段收集失败:\n" + ex.Message);
            }
        }

        /// <summary>
        /// 判断元素是否存在
        /// </summary>
        /// <param name="rootElement"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        private bool WhenExict(XmlElement rootElement, string v)
            => rootElement.SelectSingleNode(v) != null;

        /// <summary>
        /// 创建时间数据的时间校准
        /// </summary>
        /// <param name="innerText"></param>
        /// <returns></returns>
        private string TimeCorrect(string innerText)
        {
            try
            {
                long bigtime = Convert.ToInt64(innerText) * 10000000;
                DateTime dt_1970 = new DateTime(1970, 1, 1, 8, 0, 0);
                // 1970-01-01 08:00:00 是基准时间
                long tricks_1970 = dt_1970.Ticks;           //1970年1月1日刻度
                long time_tricks = tricks_1970 + bigtime;   //日志时间刻度
                DateTime dt = new DateTime(time_tricks);    //转化为DateTime
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                LogService.RecordLog("消息创建时间校正失败:\n" + ex.Message);
                return string.Empty;
            }
        }

    }
}
