using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Skd;

namespace dk.infomanager.Models
{
    public partial class DailyType
    {
        #region crudO
        public static List<DailyType> Overview()
        {
            using (var db = new Db())
            {
                //db.Configuration.LazyLoadingEnabled = false;
                return db.DailyType.ToList();
            }
        }
        #endregion
        public DailyType Parent() => Common.DailyTypes.FirstOrDefault(x => x.typeId == parentTypeId);
        public DailyType Next() => Common.DailyTypes.SingleOrDefault(x => x.typeId == nextTypeId);
        public DailyType Previous() => Common.DailyTypes.SingleOrDefault(x => x.nextTypeId == typeId);
        public DailyTypeOption DefaultOption()
        {
            if (!defaultOptionId.HasValue) return null;
            return Common.DailyTypeOptions.SingleOrDefault(x => x.optionId == defaultOptionId);
        }
        public HtmlString Breadcrum(string divider = " &raquo; ")
        {
            string rtn = "";
            int counter = 0;
            DailyType dt = Common.DailyTypes.Single(x => x.typeId == typeId);
            while (dt != null || counter > 10)
            {
                rtn = dt.internalTitle + divider + rtn;
                dt = dt.Parent();
                counter++;
            }
            return new HtmlString(rtn);
        }
        public HtmlString Icon(bool equalSizeIcons = false)
        {
            if (equalSizeIcons)
            {
                iconCss += " fa-fw";
            }
            return new HtmlString("<span class=\"" + iconCss + "\" title=\"" + internalTitle + "\"></span>");

            // #FIX stacked icons isn't working
            var cssList = iconCss.Split('#');
            if (cssList.Length < 2)
            {
                if(equalSizeIcons) {
                    iconCss += " fa-fw";
                }
                return new HtmlString("<span class=\"" + iconCss + "\" title=\"" + internalTitle + "\"></span>");
            }
            else
            {
                return new HtmlString("<span class='fa-stack' title='" + internalTitle + "'><i class='fa-stack-1x " + cssList[0] + "'></i><i class='fa-stack-1x " + cssList[1] + "'></i></span>");
            }
        }

        /*
        public static DailyTypeStat Stat(int typeId, int optionId = 0)
        {
            return Common.DailyTypes.Single(x => x.typeId == typeId).Stat(optionId);
        }
        public DailyTypeStat Stat(int optionId = 0, bool includeTypeChildren = false)
        {
            DailyTypeStat rtn = new DailyTypeStat();
            List<Daily> list = new List<Daily>();
            using (var db = new Db())
            {
                if (optionId > 0)
                {
                    list = db.Daily
                        .Where(x => x.createUserId == Common.User.userId && x.typeId == typeId && x.optionId == optionId)
                        .OrderByDescending(x => x.registerDateTime)
                        .ToList();
                    rtn.Title = Common.DailyTypes.First(x => x.typeId == typeId).internalTitle + ", " +
                        Common.DailyTypeOptions.First(x => x.optionId == optionId).internalTitle;
                }
                else
                {
                    list = db.Daily
                        .Where(x => x.createUserId == Common.User.userId && x.typeId == typeId)
                        .OrderByDescending(x => x.registerDateTime)
                        .ToList();
                    rtn.Title = Common.DailyTypes.First(x => x.typeId == typeId).internalTitle;
                }
            }

            rtn.SinceDayBreak = list.Where(x => x.registerDateTime >= Common.User.statGoodMorning).Count();
            rtn.Last24Hour = list.Where(x => x.registerDateTime >= Common.Last24hour).Count();
            rtn.SinceMidnight = list.Where(x => x.registerDateTime >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)).Count();
            rtn.LastDateTime = list.Max(x => x.registerDateTime);

            string statusTemplate = string.Format(
                "The last one were {0} ago. You had {1} today, {2} since midnight and {3} the last 24 hours.",
                rtn.LastDateTime.ToDayHM(DateTime.Now, false),
                rtn.SinceDayBreak,
                rtn.SinceMidnight,
                rtn.Last24Hour
            );

            if (typeId == 9503) // cigarette -> calculate package qty
            {
                var lastPackage = list.Where(x => x.typeId == 9503 && x.optionId == 25).Max(x => x.registerDateTime);
                var qty = list.Count(x => x.typeId == 9503 && x.registerDateTime >= lastPackage);
                statusTemplate += string.Format(" There are <b><u>{0}</u></b> left in the package.", (20 - qty));
            }

            statusTemplate = statusTemplate.Replace("now ago", "now");

            rtn.StatusText = new HtmlString(statusTemplate);

            return rtn;
        }
        public class DailyTypeStat
        {
            //public string Title = "unknown";
            //public int SinceMidnight = 0;
            //public int SinceDayBreak = 1;
            //public int Last24Hour = 2;
            //public DateTime LastDateTime = new DateTime(2021, 9, 5, 13, 45, 0);
            //public HtmlString StatusText = new HtmlString("Status text");

            public string Title;
            public int SinceMidnight;
            public int SinceDayBreak;
            public int Last24Hour;
            public DateTime LastDateTime;
            public HtmlString StatusText;
        }
        */
    }
}