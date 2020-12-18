using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestaurantWebApp.Model
{
    internal class LoginRequired : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //return base.AuthorizeCore(httpContext);
            return HttpContext.Current.Session["Token"] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var context = HttpContext.Current;
            HttpContext.Current.Session["returnUrl"] = context.Request.Url.AbsoluteUri;

            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    {"action", "Login"},
                    {"controller", "Account"}
                });
        }
    }
}