using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dk.infomanager.Models;

namespace dk.infomanager.Controllers
{
    public class TaskController : CommonController
    {
        public ActionResult Task_Sidebar() => View();

        public ActionResult ToDo_Add(string title)
        {
            using (var db = new Db())
            {
                Info i = new Info()
                {
                    remark="dummy",
                    title = title,
                    createDateTime = DateTime.Now,
                    createUserId = Common.User.userId
                };
               
                db.Info.Add(i);
                db.Entry(i).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
            }

            return RedirectToAction("Index", "Info", null);
        }
    }
}