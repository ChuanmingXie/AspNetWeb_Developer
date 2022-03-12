using AspNetMvc_WeChat_Base.FuncHelper;
using AspNetMvc_WeChat_Base.Logging;
using AspNetMvc_WeChat_Base.Model;
using AspNetMvc_WeChat_Base.WeChat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace AspNetMvc_WeChat.Areas.Chapter05.Controllers
{
    public class HomeController : Controller
    {
        // GET: Chapter05/Home
        public ActionResult Index()
        {
            LogService.RecordLog("微信平台数据参数:\n" + Request.Url);
            WeChatBeginAPI beginAPI = new WeChatBeginAPI
            {
                SignatureOrigin = !string.IsNullOrEmpty(Request.QueryString["signature"]) ? Request.QueryString["signature"] : "",
                Timestamp = !string.IsNullOrEmpty(Request.QueryString["timestamp"]) ? Request.QueryString["timestamp"] : "",
                Nonce = !string.IsNullOrEmpty(Request.QueryString["nonce"]) ? Request.QueryString["nonce"] : "",
                Encrypt_Type = (!string.IsNullOrEmpty(Request.QueryString["encrypt_type"]) ? Request.QueryString["encrypt_type"] : ""),
                Msg_SignatureOrigin = (!string.IsNullOrEmpty(Request.QueryString["msg_signature"]) ? Request.QueryString["msg_signature"] : ""),
                EchoStr = (!string.IsNullOrEmpty(Request.QueryString["echoStr"]) ? Request.QueryString["echoStr"] : ""),
            };
            //对token，timestamp，nonce加密生成singnature
            beginAPI.SignatureConfirm = WeChatTookenService.MakeSignature(beginAPI.Timestamp, beginAPI.Nonce);
            LogService.RecordLog(ReflectionHelper.GetModelByGeneric(beginAPI));

            if (Request.RequestType.ToUpper() == "POST")
            {
                string postMessage = PostInput();
                WeChatMessage weChatData = new WeChatMessage();
                weChatData.XMLToMessage(postMessage);
                if (beginAPI.Encrypt_Type != "")
                {
                    LogService.RecordLog("密文信息:\n" + weChatData.Encrypt);

                    WeChatCryptService weChatCrypt = new WeChatCryptService(
                        WeChatTookenService.Token,
                        WeChatTookenService.EncodingAESKey,
                        WeChatTookenService.AppID);

                    string messageXmlData = string.Empty;
                    int result = weChatCrypt.DecryptMsg(
                        beginAPI.Msg_SignatureOrigin, beginAPI.Timestamp, beginAPI.Nonce,
                        postMessage, ref messageXmlData);

                    if (result != 0)
                        LogService.RecordLog("解密错误代码:\n" + result);

                    LogService.RecordLog("密文解密:\n" + messageXmlData);
                    weChatData.XMLToMessage(messageXmlData);
                }
                WeChatMessageService.ShowMessage(weChatData);
                WeChatMessageService.ReplyMessage(weChatData);
                Response.Write("");
                Response.End();
            }
            else
            {
                if (beginAPI.SignatureConfirm == beginAPI.SignatureOrigin)
                {
                    //注意这里必须使用Response.Write()和Response.End(),否则配置出现错误
                    Response.Write(beginAPI.EchoStr);
                    Response.End();
                    ViewBag.CheckFromWeChat = beginAPI.EchoStr;
                }
                else
                {
                    Response.Write("");
                    //Response.Write("Invalid request!");
                    ViewBag.CheckFromWeChat = "Invalid request!";
                }
                return View(beginAPI);
            }
            return View();
        }

        /// <summary>
        /// 流数据接收来自微信平台POST过俩的数据
        /// </summary>
        /// <returns></returns>
        private string PostInput()
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                Stream stream = Request.InputStream;
                int count = 0;
                byte[] buffer = new byte[1024];
                while ((count = stream.Read(buffer, 0, 1024)) > 0)
                {
                    stringBuilder.Append(Encoding.UTF8.GetString(buffer, 0, count));
                }
                LogService.RecordLog("接收POST数据:\n" + stringBuilder.ToString());
                stream.Flush();
                stream.Close();
                stream.Dispose();
            }
            catch (Exception ex)
            {
                LogService.RecordLog("接收POST数据错误:\n" + ex.Message);
                return string.Empty;
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// gat_id群发消息
        /// </summary>
        /// <returns></returns>
        public ActionResult SendByGroupID()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendByGroupID(SendMessageParam messageParam)
        {
            string paramJson = string.Empty;
            switch (messageParam.msgtype)
            {
                case MessageType.text:
                    SendMsgGroup sendTextMsg = new SendTextByGroupID()
                    {
                        filter = new GroupFilter()
                        {
                            tag_id = messageParam.group_id,
                            is_to_all = messageParam.is_to_all
                        },
                        text = new TextParam()
                        {
                            content = messageParam.content
                        },
                        msgtype = messageParam.msgtype.ToString()
                    };
                    paramJson = JSONHelper.ObjectToJSON(sendTextMsg); break;
                case MessageType.mpnews:
                case MessageType.voice:
                case MessageType.image:
                case MessageType.mpvideo:
                case MessageType.video:
                    SendMsgGroup sendMediaMsg = new SendMediaByGroupID()
                    {
                        filter = new GroupFilter()
                        {
                            is_to_all = messageParam.is_to_all,
                            tag_id = messageParam.group_id
                        },
                        mediaType = new mediaIDParam()
                        {
                            media_id = messageParam.media_id
                        },
                        msgtype = messageParam.msgtype.ToString()
                    };
                    paramJson = JSONHelper.ObjectToJSON(sendMediaMsg).Replace("mediaType", messageParam.msgtype.ToString()); break;
            }
            string resultJson = WeChatMessageService.SendMsgByGroupID(paramJson);
            WeChatErrorResult errorResult = JSONHelper.JSONToObject<WeChatErrorResult>(resultJson);
            ViewBag.msgResult = !string.IsNullOrEmpty(errorResult.errmsg) ? "群发失败 " + errorResult.errcode + " " + errorResult.errmsg : "群发成功";
            return View();
        }

        /// <summary>
        /// open id 群发消息
        /// </summary>
        /// <returns></returns>
        public ActionResult SendByOpenID()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendByOpenID(SendMessageParam messageParam)
        {
            string paramJson = string.Empty;
            var OpenList = new List<string>();
            OpenList.AddRange(messageParam.touser.Split(new[] { ';', ' ', ',', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            switch (messageParam.msgtype)
            {
                case MessageType.text:
                    SendMsgGroup sendText = new SendTextByOpenID()
                    {
                        touser = OpenList,
                        text = new TextParam()
                        {
                            content = messageParam.content
                        },
                        msgtype = messageParam.msgtype.ToString()
                    };
                    paramJson = JSONHelper.ObjectToJSON(sendText); break;
                case MessageType.image:
                case MessageType.video:
                case MessageType.voice:
                case MessageType.mpnews:
                case MessageType.mpvideo:
                    SendMsgGroup msgMedia = new SendMediaByOpenID()
                    {
                        touser = messageParam.TouserList,
                        mediaType = new mediaIDParam()
                        {
                            media_id = messageParam.media_id
                        },
                        msgtype = messageParam.msgtype.ToString()
                    };
                    paramJson = JSONHelper.ObjectToJSON(msgMedia)
                        .Replace("mediaType", MessageType.mpvideo.ToString());
                    break;
            }
            string resultJson = WeChatMessageService.SendMsgByOpenID(paramJson);
            WeChatErrorResult errorResult = JSONHelper.JSONToObject<WeChatErrorResult>(resultJson);
            ViewBag.msgResult = !string.IsNullOrEmpty(errorResult.errmsg) ? "群发失败" + errorResult.errmsg : "群发成功";
            return View();
        }

        /// <summary>
        /// 设置所属行业
        /// </summary>
        /// <returns></returns>
        public ActionResult SetIndustry()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IndustrySet(FormCollection form)
        {
            var param = new { industry_id1 = form["industry_id1"], industry_id2 = form["industry_id2"] };
            string paramJson = JSONHelper.ObjectToJSON(param);
            string resultJson = WeChatMessageService.SetIndustry(paramJson);
            var strResult = JSONHelper.JSONToObject<WeChatErrorResult>(resultJson);
            ViewBag.setResult = "返回代码: " + strResult.errcode + " 返回消息: " + strResult.errmsg;
            return View();
        }

        /// <summary>
        /// 获取所属行业
        /// </summary>
        /// <returns></returns>
        public ActionResult IndustryGet()
        {
            string getIndustryJson = WeChatMessageService.GetIndustry();
            WeChatMsgIndustry msgIndustry = JSONHelper.JSONToObject<WeChatMsgIndustry>(getIndustryJson);
            ViewBag.infoIndustry = "主营行业：" + msgIndustry.primary_industry.first_class +
                "/" + msgIndustry.primary_industry.second_class +
                "<br />副营行业：" + msgIndustry.secondary_industry.first_class +
                "/" + msgIndustry.secondary_industry.second_class;
            return View();
        }

        public ActionResult TemplateIDGet()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TemplateIDGet(string templateIdShort)
        {
            var param = new { template_id_short = templateIdShort };
            string resultJson = WeChatMessageService.GetTemplateID(JSONHelper.ObjectToJSON(param));
            WeChatMsgTemplateResult templateIdGetResult = JSONHelper.JSONToObject<WeChatMsgTemplateResult>(resultJson);
            string resultInfo = ReflectionHelper.GetModelByGeneric(templateIdGetResult);
            LogService.RecordLog(resultInfo);
            ViewBag.templateID = resultInfo;
            return View();
        }

        public ActionResult TemplateListGet()
        {
            var resultJson = WeChatMessageService.GetTemplateList();
            LogService.RecordLog(resultJson);
            WeChatTemplateList templateList = JSONHelper.JSONToObject<WeChatTemplateList>(resultJson);
            string templateId=string.Empty;
            foreach (var item in templateList.template_list)
            {
                templateId += item.template_id+"<br />";
            }
            ViewBag.templateId = templateId;
            ViewBag.templateList = resultJson.Replace("\n","<br/>").Replace(",",",<br />");
            return View();
        }

        public ActionResult TemplateMsgSend()
        {
            var chatTemplateMsg = new
            {
                touser = "obotH60QgZm7LBI6wGBpLaOWCnHk",
                template_id = "hROcaMyz7e6WA9vxBYbxTU3JFp7VkYzWG-P-4fxGmgQ",
                url = "http://101.132.152.252/Chapter05/Home",
                data = new
                {
                    first = new
                    {
                        value = "恭喜你购买成功",
                        color = "#173177"
                    },
                    orderName=new
                    {
                        value = "香格里拉酒店",
                        color = "#ff0000"
                    },
                    orderPrice = new
                    {
                        value = "239 ￥",
                        color = "#173177"
                    },
                    orderAddress = new
                    {
                        value = "塔里木庄园草原牧场",
                        color = "#173177"
                    },
                    orderNumber = new
                    {
                        value = "3019392851343",
                        color = "#173177"
                    },
                    orderTime = new
                    {
                        value = DateTime.Now.ToString("M"),
                        color = "#173177"
                    },
                    remark = new
                    {
                        value = "点击查看更多酒店详情，部分酒店已开通网上预约及退款功能",
                        color = "#173177"
                    }
                }
            };
            string paramJson = JSONHelper.ObjectToJSON(chatTemplateMsg);
            string resultJson = WeChatMessageService.SendTemplateMsg(paramJson);
            ViewBag.msgResult = resultJson;
            return View();
        }
    }
}