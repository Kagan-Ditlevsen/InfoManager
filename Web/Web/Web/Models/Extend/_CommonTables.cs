using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using dk.infomanager.Models;

namespace dk.infomanager
{
    public partial class Common
    {
        public static List<WorkAddress> WorkAddresses
        {
            get
            {
                if (HttpContext.Current.Session["table.workaddress"] == null)
                {
                    using (var db = new Db())
                    {
                        HttpContext.Current.Session["table.workaddress"] = db.WorkAddress
                            .ToList();
                    }
                }
                return (List<WorkAddress>)HttpContext.Current.Session["table.workaddress"];
            }
        }
        public static List<WorkVehicle> WorkVehicles
        {
            get
            {
                if (HttpContext.Current.Session["table.workvehicles"] == null)
                {
                    using (var db = new Db())
                    {
                        HttpContext.Current.Session["table.workvehicles"] = db.WorkVehicle
                            .Include("WorkVehicleType")
                            .ToList();
                    }
                }
                return (List<WorkVehicle>)HttpContext.Current.Session["table.workvehicles"];
            }
        }
        public static List<WorkTaskType> WorkTaskTypes
        {
            get
            {
                if (HttpContext.Current.Session["table.worktasktype"] == null)
                {
                    using (var db = new Db())
                    {
                        HttpContext.Current.Session["table.worktasktype"] = db.WorkTaskType
                            .ToList();
                    }
                }
                return (List<WorkTaskType>)HttpContext.Current.Session["table.worktasktype"];
            }
        }
    }
}