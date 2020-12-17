using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp.Authenticators;

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
            //base.HandleUnauthorizedRequest(filterContext);
            //filterContext.Result = new HttpUnauthorizedResult();
            //filterContext.Result = new RedirectResult("Account/Login");
            filterContext.Result = new RedirectResult("Login");
        }
    }
}