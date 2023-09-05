using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PT.Service
{
    public class BaseAuthAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool hasSession = true;

            if (httpContext.Request.IsAuthenticated == false || httpContext.Session["UserId"] == null)
            {
                hasSession = false;
            }

            return hasSession && base.AuthorizeCore(httpContext);
        }
    }
}
