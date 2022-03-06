using System.Web.Mvc;

namespace AspNetMvc_WeChat.Areas.Chapter04
{
    public class Chapter04AreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Chapter04";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Chapter04_default",
                "Chapter04/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}