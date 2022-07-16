using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InfoMan.Controllers
{
    public class InfoController : CommonController
    {
        public ActionResult Index() => View();
    }
}