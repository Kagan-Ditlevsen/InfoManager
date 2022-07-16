using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InfoMan.Controllers
{
    public class HomeController : CommonController
    {
        public ActionResult Index() => View();

        public object SessionSetValue(string name, string value)
        {
            Session[name] = value;
            return value;

        }
    }
}