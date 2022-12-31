using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dk.infomanager.Models
{
    public partial class DailyTypeOption
    {
        #region crudO
        public static List<DailyTypeOption> Overview()
        {
            using (var db = new Db())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.DailyTypeOption.ToList();
            }
        }
        #endregion
    }
}
