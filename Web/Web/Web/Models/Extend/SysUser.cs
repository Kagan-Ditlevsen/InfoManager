using Skd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dk.infomanager.Models
{
    public class SysUserStat
    {
        public int typeId { get; set; }
        public string typeTitle { get; set; }
        public Nullable<int> optionId { get; set; }
        public string optionTitle { get; set; }
        public int qtyMorning { get; set; }
        public int qty24h { get; set; }
        public int qtyMidnight { get; set; }
        public Nullable<System.DateTime> lastTimeTypeId { get; set; }
        public Nullable<System.DateTime> lastTimeOptionId { get; set; }
        public Nullable<System.DateTime> lastRegisterDateTime { get; set; }
        public int createUserId { get; set; }
        public HtmlString StatusText()
        {
            string rtn = "";
            if (lastTimeTypeId.HasValue)
            {
                rtn += "The last one were " + lastTimeTypeId.Value.ToDayHM(DateTime.Now, false) + " ago. ";
            }
            if (qty24h > 0)
            {
                rtn += qty24h + " within the last 24 hours. ";
            }
            if (qtyMidnight > 0)
            {
                rtn += qtyMidnight + " since midnight. ";
            }
            if (qtyMorning > 0)
            {
                rtn += qtyMorning + " since morning. ";
            }
            rtn = rtn.Replace("  ", " ");

            return new HtmlString(rtn);
        }
        public HtmlString TypeIcon()
        {
            return Common.DailyTypes.Single(x => x.typeId == typeId).Icon();
        }
    }
    public class Stat
    {
        public SysUserStat RetrieveAsOptionGroup(int typeId)
        {
            List<SysUserStat> list = Overview().Where(x => x.typeId == typeId).ToList();
            return new SysUserStat()
            {
                typeId = list.First().typeId,
                typeTitle = list.First().typeTitle,
                optionId = -1,
                optionTitle = "Option Group",
                qtyMorning = list.Sum(x => x.qtyMorning),
                qty24h = list.Sum(x => x.qty24h),
                qtyMidnight = list.Sum(x => x.qtyMidnight),
                lastTimeTypeId = list.Max(x => x.lastTimeTypeId),
                lastTimeOptionId = list.Max(x => x.lastTimeOptionId),
                createUserId = list.First().createUserId
            };
        }
        public SysUserStat Retrieve(int typeId)
        {
            return Overview().SingleOrDefault(x => x.typeId == typeId && !x.optionId.HasValue);
        }
        public SysUserStat Retrieve(int typeId, int optionId)
        {
            return Overview().SingleOrDefault(x => x.typeId == typeId && x.optionId == optionId);
        }
        public List<SysUserStat> Overview()
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["UserStat"] == null)
            {
                //using (var db = new Db())
                {
                    Common.User.Stat.Reload();
                }
            }
            return (List<SysUserStat>)HttpContext.Current.Session["UserStat"];
        }
        //public static void Reload(int userId)
        //{
        //    using (var db = new Db())
        //    {
        //        HttpContext.Current.Session["UserStat"] = db.Database.SqlQuery<SysUserStat>("SELECT * FROM dbo.vw_Daily_Statistics WHERE createUserId = " + userId).ToList();
        //        HttpContext.Current.Session["UserStatReload"] = DateTime.Now;
        //    }
        //}
        public void Reload()
        {
            using (var db = new Db())
            {
                HttpContext.Current.Session["UserStat"] = db.Database.SqlQuery<SysUserStat>("SELECT * FROM dbo.vw_Daily_Statistics WHERE createUserId = " + Common.User.userId).ToList();
                HttpContext.Current.Session["UserStatReload"] = DateTime.Now;
            }
        }
    }
    public partial class SysUser
    {
        public virtual Stat Stat { get; set; }

        public SysUser() { Stat = new Stat(); }
        public string CompleteName()
        {
            return firstName + " " + lastName;
        }
        #region cRudo
        public static SysUser Retrieve(int userId)
        {
            using (var db = new Db())
            {
                var user = db.SysUser.SingleOrDefault(c => c.userId == userId);
                if (user != null)
                {
                    HttpContext.Current.Session["User"] = user;
                    user.Stat.Reload();
                }
                return user;
            }
        }
        public static SysUser Retrieve(string logonId, string password)
        {
            using (var db = new Db())
            {
                var user = db.SysUser.SingleOrDefault(c => c.emailAddress == logonId && c.password == password);
                if (user != null)
                {
                    HttpContext.Current.Session["User"] = user;
                    user.Stat.Reload();
                }
                return user;
            }
        }
        #endregion
    }
}
