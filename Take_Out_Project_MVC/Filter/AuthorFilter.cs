using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Take_Out_Project_MVC.Filter
{
    public class AuthorFilter:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            
            if (filterContext.HttpContext.Session["UserId"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/MZGUser/MZGUser");
            }
        }
    }
}