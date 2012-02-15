using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class AuthenticationController : Controller
    {
        [NonAction]
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;

            base.OnActionExecuting(filterContext);
        }
    }
}
