using AspNetMvc_WeChat_Base.Logging;
using AspNetMvc_WeChat_Base.WeChat;
using System.Web.Mvc;

namespace AspNetMvc_WeChat.Controllers
{
    public class EchoController : Controller
    {
        // GET: Echo
        public ActionResult Index()
        {
            string echoString = Request.QueryString["echoStr"];
            string signature = Request.QueryString["signature"];
            string timestamp = Request.QueryString["timestamp"];
            string nonce = Request.QueryString["nonce"];
            LogService.RecordLog("echoString:" + echoString);
            LogService.RecordLog("signature:" + signature);
            LogService.RecordLog("timestamp:" + timestamp);
            LogService.RecordLog("nonce:" + nonce);

            //对token，timestamp，nonce加密生成singnature
            var signatureTemp = WeChatTookenService.MakeSignature(timestamp, nonce);
            LogService.RecordLog("signatureTemp:" + signatureTemp);

            if (!string.IsNullOrEmpty(echoString)&&signature==signatureTemp)
            {
                Response.Write(echoString);
                Response.End();
            }
            else
            {
                Response.Write("无效的信息传递");
            }
            return View();
        }
    }
}