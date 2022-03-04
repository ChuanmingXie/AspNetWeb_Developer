using System.Web.Mvc;

namespace AspNetMvc_WeChat.Areas.Chapter02
{
    public class Chapter02AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Chapter02";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Chapter02_default",
                "Chapter02/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}