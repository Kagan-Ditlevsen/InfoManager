using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dk.infomanager.Controllers
{
    public class DeveloperController : CommonController
    {
        public ActionResult Index() => View();

        public ActionResult WiP(string view, int? id)
        {
            if (string.IsNullOrEmpty(view))
            {
                view = "Index";
            }
            ViewBag.Title = "WiP: ";

            string viewUrl = "WiP/" + view;

            return View(viewUrl);
        }
        public ActionResult POC(string area, string view, int? id)
        {
            ViewBag.Title = "Proof of " + area + " Concept: ";

            string viewUrl = "PoC/";
            if (!string.IsNullOrEmpty(area))
            {
                viewUrl += area + "/";
                if (!string.IsNullOrEmpty(view))
                {
                    viewUrl += view;
                }
            }
            if (viewUrl == "PoC/")
            {
                viewUrl = "PoC/Index";
            }

            return View(viewUrl);
        }
        public ActionResult ToDo(string area, string view, int? id)
        {
            ViewBag.Title = "ToDo: ";

            string viewUrl = "ToDo/";
            if (!string.IsNullOrEmpty(area))
            {
                viewUrl += area + "/";
                if (!string.IsNullOrEmpty(view))
                {
                    viewUrl += view;
                }
            }
            if (viewUrl == "ToDo/")
            {
                viewUrl = "ToDo/Index";
            }

            return View(viewUrl);
        }

        public bool ToggleDebug()
        {
            return Skd.Web.Developer.ToggleDebug();
        }

        public void PostData()
        {
            Skd.Web.Developer.DisplayPostForm(Request.Form.AllKeys);
        }

        public void RefreshCommonData()
        {
            Common.ReloadData();
            HttpContext.Session.Remove("table.workaddress");
            HttpContext.Session.Remove("table.workvehicles");
            HttpContext.Session.Remove("table.worktasktype");
        }
    }
}