/*****************************************************************************
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <summary>
        /// 创建事件
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
        public int MsgId { get; set; }


        /// <summary>
        /// 开发链接,开发者可用HTTP,GET获取
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 图片消息媒体id|语音消息id|,可以调用第8章的多媒体文件下载获取数据
        /// </summary>
        public string media_id { get; set; }


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
        /// 用于解析接收到的XML文件为信息字符串
        /// </summary>
        /// <param name="requestXML"></param>
        public void XMLToMessage(string requestXML)
        {

        }
    }

}
