using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskApp.Models
{
    public class EmployeeAuthorization : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            int userType = 0;
            if (HttpContext.Current.Session["UserType"] != null)
                userType = int.Parse(HttpContext.Current.Session["UserType"].ToString());
            return (userType == 2) ? true : false;
        }
    }
}