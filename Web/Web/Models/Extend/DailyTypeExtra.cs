using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skd;

namespace dk.infomanager.Models
{
    public partial class DailyTypeExtra
    {
        #region crudO
        public static List<DailyTypeExtra> Overview()
        {
            using (var db = new Db())
            {
                //db.Configuration.LazyLoadingEnabled = false;
                return db.DailyTypeExtra.ToList();
            }
        }
        #endregion
    }
}