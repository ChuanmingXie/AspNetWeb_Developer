using System.Web.Mvc;

namespace AspNetMvc_WeChat.Areas.areaDemo
{
    public class areaDemoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "areaDemo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "areaDemo_default",
                "areaDemo/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}