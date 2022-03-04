using System.Web.Mvc;

namespace AspNetMvc_WeChat.Areas.Chapter01
{
    public class Chapter01AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Chapter01";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Chapter01_default",
                "Chapter01/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}