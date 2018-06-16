using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace API_ScoreCard.Authorization
{
    public class AuthorizeAdminAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized) return false;

            var user = httpContext.User;
            if (user.IsInRole("Admin"))
                return true;
            return false;
        }
    }
}