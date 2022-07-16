using InfoMan.Models;
using System;
using System.Web.Mvc;
using Skd.Web;
using System.Linq;
using System.Collections.Generic;

namespace InfoMan.Controllers
{
    public class DailyController : CommonController
    {
        public ActionResult Index() => View();
        public ActionResult Stat() => View();
        public ActionResult Stat_Dietitian() => View();
        public ActionResult Stat_OccTherapist() => View();
        public ActionResult Sidebar_Daily() => View();
        public ActionResult Sidebar_Stat() => View();
        public ActionResult Sidebar_DailyAdd() => View();
        public void Delete(Guid dailyId, string rUrl = "/Daily")
        {
            var rtn = Daily.Delete(dailyId);
            if (rtn.Success)
            {
                new Message(Message.MessageTypeEnum.Success, "Entry were deleted");
            }
            else
            {
                new Message()
                {
                    MessageType = Message.MessageTypeEnum.Error,
                    Title = "Entry were NOT deleted",
                    SubTitle = rtn.Error.Message
                };
            }

            Response.Redirect(rUrl);
        }
        public void Daily_AddEdit(Guid? dailyId, int typeId, int? optionId, DateTime? registerDateTime, string remark = "", int qty = 1, string extraValues = null, string rUrl = "/Daily")
        {
            DbReturnValue rtn;
            string typeTitle = Common.DailyTypes.Single(x => x.typeId == typeId).internalTitle;
            Dictionary<int, string> extras = new Dictionary<int, string>();
            foreach (string s in Request.Form.Keys)
            {
                if (s.StartsWith("extraId_") && !s.EndsWith("_text"))
                {
                    extras.Add(int.Parse(s.Substring(8)), Request.Form[s].ToString());
                }
            }
            if (extraValues != null)
            {
                string[] tmp = extraValues.Split(',');
                //throw new Exception(extraValues[0]);
                string err = "";
                for (int index = 0; index < tmp.Length - 1; index += 2)
                {
                    extras.Add(int.Parse(tmp[index]), tmp[index + 1]);
                    err += int.Parse(tmp[index]).ToString() + " = " + tmp[index + 1] + "<br/>";
                }
                //throw new Exception(err);
            }

            if (!dailyId.HasValue)
            {
                string rowIdsAffected = "";
                for (int i = 1; i <= qty; i++)
                {
                    rtn = Daily.Create(typeId, optionId, registerDateTime, remark, extras);
                    if (!string.IsNullOrEmpty(rowIdsAffected))
                    {
                        rowIdsAffected += ";";
                    }
                    rowIdsAffected += rtn.RowIdsAffected;
                }

                if (Request.IsAjaxRequest())
                {
                    Response.Clear();
                    if (qty > 1)
                    {
                        Response.Write("success;" + qty.ToString() + " entries were added");
                    }
                    else
                    {
                        Response.Write("success;Entry were added");
                    }
                    Response.Flush();
                }
                else
                {
                    if (qty > 1)
                    {
                        new Message()
                        {
                            MessageType = Message.MessageTypeEnum.Success,
                            Title = qty.ToString() + " entries were added",
                            SubTitle = Daily.Retrieve(Guid.Parse(rowIdsAffected.Split(';')[0].Replace("'", ""))).Title().ToString()
                        };
                    }
                    else
                    {
                        new Message()
                        {
                            MessageType = Message.MessageTypeEnum.Success,
                            Title = "Entry were added",
                            SubTitle = typeTitle
                        };
                    }
                }

                TempData["highlightRowIds"] = rowIdsAffected;
            }
            else
            {
                rtn = Daily.Update(dailyId.Value, typeId, optionId, registerDateTime, remark, extras);
                new Message()
                {
                    MessageType = Message.MessageTypeEnum.Success,
                    Title = "Entry were updated",
                    SubTitle = typeTitle
                };

                TempData["highlightRowIds"] = rtn.RowIdsAffected;
            }

            if (Request.IsAjaxRequest() || rUrl == "void")
            {
                return;
            }
            Response.Redirect(rUrl);
        }
    }
}

namespace InfoMan.ViewModels
{
    public class Daily_DailyList
    {
        public int ShowQty = 25;
        public int MaxQty = 999;
        public bool ShowFooter = true;
        public bool ShowFilter = true;
        public Daily_DailyList(int showQty = 25, int maxQty = 999, bool showFooter = true, bool showFilter = false)
        {
            ShowQty = showQty;
            MaxQty = maxQty;
            ShowFooter = showFooter;
            ShowFilter = showFilter;
        }
    }
}