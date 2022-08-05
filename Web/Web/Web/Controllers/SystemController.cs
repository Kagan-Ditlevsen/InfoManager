using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dk.infomanager.Controllers
{
    public class SystemController : CommonController
    {
        public ActionResult Index() => View();
        public ActionResult DailyType(int? id)
        {
            return View(id);
        }
        public ActionResult DailyTypeExtra(int? id)
        {
            return View(id);
        }
        public ActionResult InfoCategory(int? id)
        {
            return View(id);
        }
        /* Sidebar */
        public ActionResult Me_Sidebar() => View();
        public ActionResult Sidebar_Device() => View();
    }
}