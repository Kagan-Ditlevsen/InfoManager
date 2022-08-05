using dk.infomanager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Skd.Web;

namespace dk.infomanager
{
    public static class ExtensionMethods
    {
        public static string Numberplate7(this string obj)
        {
            if (string.IsNullOrEmpty(obj) || obj.Length != 7)
            {
                return "";
            }
            return obj.Substring(0, 2) + " " + obj.Substring(2, 2) + " " + obj.Substring(4, 3);
        }
        public static string Numberplate6(this string obj)
        {
            if (string.IsNullOrEmpty(obj) || obj.Length != 6)
            {
                return "";
            }
            return obj.Substring(0, 2) + " " + obj.Substring(2, 4);
        }
    }
}

namespace dk.infomanager.Controllers
{
    public class WorkController : CommonController
    {
        public ActionResult DriversAid() => View();
        public ActionResult Drive2(Guid id)
        {
            using (var db = new Db())
            {
                var model = db.Work
                    .Include("WorkTask")
                    .Include("WorkTask.WorkTaskType")
                    .Single(x => x.createUserId == Common.User.userId && x.workId == id);
                if (model == null)
                {
                    new Message(Message.MessageTypeEnum.Error, "Workday do not exist");
                    Response.Redirect("/Work", true);
                }

                return View(model);
            }
        }
        public ActionResult Design() => View();
        public ActionResult DriveOTF(Guid? id)
        {
            if (id.HasValue)
            {
                using (var db = new Db())
                {
                    var model = db.Work
                        .Include("WorkTask")
                        .Include("WorkTask.WorkTaskType")
                        .Single(x => x.createUserId == Common.User.userId && x.workId == id);
                    if (model == null)
                    {
                        new Message(Message.MessageTypeEnum.Error, "Workday do not exist");
                        Response.Redirect("/Work", true);
                    }

                    return View(model);
                }
            }

            return View(new Work()
            {
                createDateTime = DateTime.Now,
                createUserId = Common.User.userId
            });
        }
        public ActionResult Index() => View();
        public ActionResult Edit(Guid id)
        {
            using (var db = new Db())
            {
                return View(db.Work.Include("WorkTask").Include("WorkTask.WorkTaskType").Single(x => x.workId == id));
            }
        }
        public ActionResult Hours() => View();
        public ActionResult ReportWork(Guid id)
        {
            using (var db = new Db())
            {
                return View(db.Work.Include("WorkTask").Include("WorkTask.WorkTaskType").Single(x => x.workId == id));
            }
        }
        public ActionResult Report(string id, DateTime? dateTime)
        {
            string viewName = "Report";
            if (!string.IsNullOrEmpty(id))
            {
                viewName += "_" + id;
            }
            return View(viewName);
        }
        public void Create(DateTime? startDateTime, DateTime? endDateTime, string remark, string templateId, string rUrl = "Drive/")
        {
            if (!rUrl.EndsWith("/"))
            {
                rUrl += "/";
            }
            using (var db = new Db())
            {
                Work obj = new Work()
                {
                    workId = Guid.NewGuid(),
                    startDateTime = startDateTime,
                    endDateTime = endDateTime,
                    remark = remark,
                    createDateTime = DateTime.Now,
                    createUserId = Common.User.userId
                };
                db.Entry(obj).State = System.Data.Entity.EntityState.Added;

                if (string.IsNullOrEmpty(templateId))
                {
                    new Message(Message.MessageTypeEnum.Success, "Workday created");
                }
                else
                {
                    obj.remark = "[" + templateId + "]";
                    if (!string.IsNullOrEmpty(remark))
                    {
                        obj.remark += " " + remark;
                    }
                    foreach (var tempTask in db.WorkTaskTemplate.Where(x => x.templateId == templateId))
                    {
                        obj.WorkTask.Add(new WorkTask()
                        {
                            taskId = Guid.NewGuid(),
                            workId = obj.workId,
                            sortOrder = tempTask.sortOrder,
                            typeId = tempTask.typeId,
                            startDateTime = tempTask.startDateTime,
                            endDateTime = tempTask.endDateTime,
                            addressText = tempTask.addressText,
                            vehicleNumberplate = tempTask.vehicleNumberplate,
                            linkNumberplate = tempTask.linkNumberplate,
                            dollyNumberplate = tempTask.dollyNumberplate,
                            trailerNumberplate = tempTask.trailerNumberplate,
                            remark = tempTask.remark,
                            systemRemark = tempTask.systemRemark,
                            reportColumnNumber = (byte)tempTask.reportColumnNumber,
                            createDateTime = DateTime.Now,
                            createUserId = Common.User.userId
                        });
                    }

                    new Message(Message.MessageTypeEnum.Success, "Workday created with [" + templateId + "] template.");
                }
                db.SaveChangesWithTrace();

                if (rUrl == "/")
                {
                    Response.Redirect("/Work");
                }
                else
                {
                    Response.Redirect("/Work/" + rUrl + obj.workId);
                }
            }
        }
        public void Delete(Guid workId)
        {
            using (var db = new Db())
            {
                Work obj = db.Work.Single(x => x.workId == workId);
                db.Entry(obj).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChangesWithTrace();

                new Message(Message.MessageTypeEnum.Success, "Workday deleted");
                Response.Redirect("/Work");
            }
        }
        public void Duplicate(Guid workId)
        {

        }

        public void Edit_Do(Guid workId, DateTime? startDateTime, DateTime? endDateTime, string remark)
        {
            using (var db = new Db())
            {
                Work obj = db.Work.Single(x => x.workId == workId);
                obj.startDateTime = startDateTime ?? null;
                obj.endDateTime = endDateTime ?? null;
                obj.remark = remark;
                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;

                new Message(Message.MessageTypeEnum.Success, "Workday updated");

                db.SaveChangesWithTrace();

                Response.Redirect("/Work");
            }
        }
        public void Finish(Guid workId)
        {
            using (var db = new Db())
            {
                Work obj = db.Work.Include("WorkTask").Single(x => x.workId == workId);

                obj.status = 250;
                obj.startDateTime = obj.WorkTask.Min(x => x.startDateTime);
                obj.endDateTime = obj.WorkTask.Max(x => x.endDateTime);

                db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
                db.SaveChangesWithTrace();

                new Message(Message.MessageTypeEnum.Success, "Workday finished");
                Response.Redirect("/Work");
            }
        }
        public void ActionDelete(Guid workId, Guid taskId)
        {
            using (var db = new Db())
            {
                WorkTask obj = db.WorkTask.Single(x => x.workId == workId && x.taskId == taskId);
                db.Entry(obj).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChangesWithTrace();
                UpdateSortOrder(workId);

                new Message(Message.MessageTypeEnum.Success, "Task deleted");
                Response.Redirect("/Work/Drive/" + workId);
            }
        }
        public void ActionCreateUpdate(Guid workId, Guid? taskId, int sortOrder, DateTime? startDateTime, DateTime? endDateTime, int typeId, string addressText, string vehicleNumberplate, string linkNumberplate, string dollyNumberplate, string trailerNumberplate, string remark, byte reportColumnNumber)
        {
            bool isNew = !taskId.HasValue || taskId == Guid.Empty;
            using (var db = new Db())
            {
                WorkTask obj;
                if (isNew)
                {
                    obj = new WorkTask()
                    {
                        taskId = Guid.NewGuid(),
                        workId = workId,
                        typeId = typeId,
                        sortOrder = sortOrder,
                        createDateTime = DateTime.Now,
                        createUserId = Common.User.userId
                    };
                }
                else
                {
                    obj = db.WorkTask.Single(x => x.taskId == taskId.Value);
                }
                obj.startDateTime = startDateTime.HasValue ? startDateTime : null;
                obj.endDateTime = endDateTime.HasValue ? endDateTime : null;
                obj.addressText = String.IsNullOrEmpty(addressText) ? "" : addressText;
                if (addressText == "-1")
                {
                    obj.addressText = Request["addressText_text"];
                }
                obj.vehicleNumberplate = String.IsNullOrEmpty(vehicleNumberplate) ? "" : vehicleNumberplate;
                if (vehicleNumberplate == "-1")
                {
                    obj.vehicleNumberplate = Request["vehicleNumberplate_text"];
                    obj.vehicleText = obj.vehicleNumberplate;
                }
                obj.linkNumberplate = String.IsNullOrEmpty(linkNumberplate) ? "" : linkNumberplate;
                if (linkNumberplate == "-1")
                {
                    obj.linkNumberplate = Request["linkNumberplate_text"];
                    obj.vehicleText = obj.linkNumberplate;
                }
                obj.dollyNumberplate = String.IsNullOrEmpty(dollyNumberplate) ? "" : dollyNumberplate;
                if (dollyNumberplate == "-1")
                {
                    obj.dollyNumberplate = Request["dollyNumberplate_text"];
                    obj.vehicleText = obj.dollyNumberplate;
                }
                obj.trailerNumberplate = String.IsNullOrEmpty(trailerNumberplate) ? "" : trailerNumberplate;
                if (trailerNumberplate == "-1")
                {
                    obj.trailerNumberplate = Request["trailerNumberplate_text"];
                    obj.vehicleText = obj.trailerNumberplate;
                }
                obj.remark = String.IsNullOrEmpty(remark) ? "" : remark;
                obj.reportColumnNumber = reportColumnNumber;

                db.Entry(obj).State = isNew ? System.Data.Entity.EntityState.Added : System.Data.Entity.EntityState.Modified;

                if (isNew)
                {
                    switch (Enum.Parse(typeof(WorkTaskTypeEnum), typeId.ToString()))
                    {
                        case WorkTaskTypeEnum.Arrivial:
                            var dep = new WorkTask()
                            {
                                taskId = Guid.NewGuid(),
                                workId = workId,
                                typeId = (int)WorkTaskTypeEnum.Departure,
                                sortOrder = sortOrder + 1,
                                createDateTime = DateTime.Now,
                                createUserId = Common.User.userId
                            };
                            db.Entry(dep).State = System.Data.Entity.EntityState.Added;
                            break;
                    }
                }

                db.SaveChangesWithTrace();

                UpdateSortOrder(workId);
            };

            new Message(Message.MessageTypeEnum.Success, "Task " + (isNew ? "created" : "updated"));
            Response.Redirect("/Work/Drive/" + workId);
        }
        public ActionResult Drive(Guid id)
        {
            using (var db = new Db())
            {
                var model = db.Work
                    .Include("WorkTask")
                    .Include("WorkTask.WorkTaskType")
                    .Single(x => x.createUserId == Common.User.userId && x.workId == id);
                if (model == null)
                {
                    new Message(Message.MessageTypeEnum.Error, "Workday do not exist");
                    Response.Redirect("/Work", true);
                }
                //string currentTaskId = model.WorkTask.Where(x => !(bool)x.isFinished).OrderBy(x => x.sortOrder).FirstOrDefault().taskId.ToString();

                //return View("Drive#" + model.workId + "#" + currentTaskId);
                return View(model);
            }
        }
        public void DriveCmd(Guid workId, Guid taskId, string cmd, string entry)
        {
            using (var db = new Db())
            {
                WorkTask model = new WorkTask();
                switch (cmd.ToUpper())
                {
                    case "START":
                        model = db.WorkTask.Single(x => x.workId == workId && x.taskId == taskId);
                        if (string.IsNullOrEmpty(entry))
                        {
                            model.startDateTime = DateTime.Now;
                        }
                        else
                        {
                            model.startDateTime = DateTime.Parse(entry);
                        }
                        break;
                    case "END":
                        model = db.WorkTask.Single(x => x.workId == workId && x.taskId == taskId);
                        if (string.IsNullOrEmpty(entry))
                        {
                            model.endDateTime = DateTime.Now;
                        }
                        else
                        {
                            model.endDateTime = DateTime.Parse(entry);
                        }
                        break;
                }
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChangesWithTrace();

                Response.Redirect("/Work/Drive/" + workId);
            }
        }
        public ActionResult TaskEdit(Guid? id, int? typeId, int? sortOrder, Guid? workId)
        {
            using (var db = new Db())
            {
                WorkTask model = null;
                if (id.HasValue)
                {
                    model = db.WorkTask.Include("WorkTaskType").Single(x => x.createUserId == Common.User.userId && x.taskId == id.Value);
                }
                else
                {
                    model = new WorkTask() { workId = workId.Value, typeId = typeId.Value, sortOrder = sortOrder.Value, startDateTime = DateTime.Now };
                    model.WorkTaskType = db.WorkTaskType.Single(x => x.typeId == typeId);
                }
                return PartialView("_TaskEdit", model);
            }
        }
        public static void UpdateSortOrder(Guid workId)
        {
            using (var db = new Db())
            {
                try
                {
                    db.Database.ExecuteSqlCommand("EXEC stp_WorkTask_UpdateFields @workId",
                        new System.Data.SqlClient.SqlParameter("@workId", workId)
                    );
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEve)
                {
                    string text = "";
                    foreach (System.Data.Entity.Validation.DbEntityValidationResult entityValidationError in dbEve.EntityValidationErrors)
                    {
                        foreach (System.Data.Entity.Validation.DbValidationError validationError in entityValidationError.ValidationErrors)
                        {
                            text = text + validationError.PropertyName + "  " + validationError.ErrorMessage + Environment.NewLine;
                        }
                    }

                    throw new Exception(text);
                }
            }
        }
    }
}