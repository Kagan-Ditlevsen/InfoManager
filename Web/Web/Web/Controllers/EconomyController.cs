using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dk.infomanager.Controllers
{
    public class EconomyController : CommonController
    {
        public ActionResult Index() => View();

        public ActionResult Finance(int id, string view)
        {
            throw new Exception(String.Format("Showing financeId {0} using view \"{1}\"", id.ToString(), view));
        }

        public void FinanceCreate() { }
    }
}