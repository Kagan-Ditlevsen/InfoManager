using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skd;

namespace dk.infomanager.Models
{
    public partial class DailyInfo
    {
        #region crudO
        public static List<DailyInfo> Overview()
        {
            using (var db = new Db())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.DailyInfo.ToList();
            }
        }
        #endregion
    }
}