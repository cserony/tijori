using System.Web.Mvc;

namespace ESOL_EDU.Areas.MerchantUser
{
    public class MerchantUserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MerchantUser";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MerchantUser_default",
                "MerchantUser/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}