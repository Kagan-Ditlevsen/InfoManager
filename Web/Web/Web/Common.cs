using InfoMan.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace InfoMan
{
    public partial class Common
    {
        public class DayName
        {
            public string Name;
            public bool IsHoliday;
            public DateTime Date;

            public static DayName Retrieve(DateTime date)
            {
                return null;
            }
        }
    }
    public partial class Common
    {
        public static void AddToLog(string logEntry)
        {
            string[] logArray = new string[1];
            logArray[0] = string.Format("{0};{1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.FFFFFF"), logEntry);
            File.AppendAllLines(HttpContext.Current.Server.MapPath("~/Log.txt"), logArray);
        }

        public class FileItem
        {
            public string Id { get; private set; }
            public string Directory { get; private set; }
            public string Filename { get; private set; }
            public HtmlString Title { get; private set; }
            public string Url { get; private set; }

            public static List<FileItem> Get(string inputDirectory, string searchPattern = "*.cshtml")
            {
                List<FileItem> rtn = new List<FileItem>();
                if (!inputDirectory.EndsWith("\\"))
                {
                    inputDirectory += "\\";
                }
                string directoryReplace = HttpContext.Current.Server.MapPath(inputDirectory);

                foreach (var file in System.IO.Directory.GetFiles(HttpContext.Current.Server.MapPath(inputDirectory), searchPattern, SearchOption.AllDirectories))
                {
                    FileInfo fi = new FileInfo(file);
                    string[] elms = fi.FullName.Replace(directoryReplace, "").Split('\\');

                    if (elms.Length > 1)
                    {
                        var elm = new FileItem()
                        {
                            Id = file,
                            Directory = elms[0],
                            Filename = elms[1].Replace(".cshtml", ""),
                        };
                        elm.Url = (inputDirectory + elm.Directory + "/" + elm.Filename)
                            .Replace(@"/Views", "")
                            .Replace("\\", "");

                        var content = File.ReadAllLines(file);

                        var contentTitle = content.FirstOrDefault(x => x.Contains("ViewBag.Title"));
                        elm.Title = new HtmlString("*" + elm.Filename);
                        if (contentTitle != null)
                        {
                            elm.Title = new HtmlString(content.FirstOrDefault(x => x.Contains("ViewBag.Title"))
                                .Replace("ViewBag.Title += ", "")
                                .Replace("ViewBag.Title = ", "")
                                .Replace("\"", "").Replace(";", "")
                            );
                        }

                        rtn.Add(elm);
                    }
                }

                return rtn.OrderBy(x => x.Directory).ThenBy(x => x.Filename).ToList();
            }
        }

        #region Data Tables
        public static void ReloadData()
        {
            // Application
            dailyTypes = null;
            dailyTypeExtras = null;
            dailyTypeOptions = null;
            infoCategories = null;
            infoEntryTypes = null;
        }

        // DailyTypeExtra
        private static List<DailyTypeExtra> dailyTypeExtras;
        private static DateTime dailyTypeExtras_UpdateDateTime;
        private static void dailyTypeExtras_Update()
        {
            dailyTypeExtras_UpdateDateTime = DateTime.Now;
            using (var db = new Db())
            {
                dailyTypeExtras = db.DailyTypeExtra
                    .Include("DailyType")
                    .Include("DailyType.DailyTypeOption")
                    .Include("DailyInfo")
                    .Include("DailyInfo.Daily")
                    .OrderBy(x => x.internalTitle)
                    .ToList();
            }
        }
        public static List<DailyTypeExtra> DailyTypeExtra
        {
            get
            {
                if (dailyTypeExtras == null || ((DateTime.Now - dailyTypeExtras_UpdateDateTime).TotalHours > 1)) { dailyTypeExtras_Update(); }

                return dailyTypeExtras;
            }
        }

        // DailyType
        private static List<DailyType> dailyTypes;
        private static DateTime dailyTypes_UpdateDateTime;
        private static void dailyTypes_Update()
        {
            dailyTypes_UpdateDateTime = DateTime.Now;
            using (var db = new Db())
            {
                dailyTypes = db.DailyType
                    .Include("DailyTypeOption")
                    .Include("Daily")
                    .Include("Daily.DailyInfo")
                    .Include("DailyTypeExtra")
                    .OrderBy(x => x.typeId)
                    .ToList();
            }
        }
        public static List<DailyType> DailyTypes
        {
            get
            {
                if (dailyTypes == null || (DateTime.Now - dailyTypes_UpdateDateTime).TotalHours > 1) { dailyTypes_Update(); }

                return dailyTypes;
            }
        }

        // DailyTypeOption
        private static List<DailyTypeOption> dailyTypeOptions;
        private static DateTime dailyTypeOptions_UpdateDateTime;
        private static void dailyTypeOptions_Update()
        {
            dailyTypeOptions_UpdateDateTime = DateTime.Now;
            using (var db = new Db())
            {
                dailyTypeOptions = db.DailyTypeOption
                    .Include("DailyType")
                    .Include("Daily")
                    .Include("Daily.DailyInfo")
                    .Include("DailyType.DailyTypeExtra")
                    .OrderBy(x => x.internalTitle)
                    .ToList();
            }
        }
        public static List<DailyTypeOption> DailyTypeOptions
        {
            get
            {
                if (dailyTypeOptions == null || ((DateTime.Now - dailyTypeOptions_UpdateDateTime).TotalHours > 1)) { dailyTypeOptions_Update(); }

                return dailyTypeOptions;
            }
        }

        // InfoCategory
        private static List<InfoCategory> infoCategories;
        private static DateTime infoCategories_UpdateDateTime;
        private static void infoCategories_Update()
        {
            infoCategories_UpdateDateTime = DateTime.Now;
            using (var db = new Db())
            {
                infoCategories = db.InfoCategory
                    .Include("Info")
                    .OrderBy(x => x.internalTitle)
                    .ToList();
            }
        }
        public static List<InfoCategory> InfoCategories
        {
            get
            {
                if (infoCategories == null || ((DateTime.Now - infoCategories_UpdateDateTime).TotalHours > 1)) { infoCategories_Update(); }

                return infoCategories;
            }
        }

        // InfoEntryType
        private static List<InfoEntryType> infoEntryTypes;
        private static DateTime infoEntryTypes_UpdateDateTime;
        private static void infoEntryTypes_Update()
        {
            infoEntryTypes_UpdateDateTime = DateTime.Now;
            using (var db = new Db())
            {
                infoEntryTypes = db.InfoEntryType
                    .Include("InfoEntry")
                    .Include("InfoEntry.Info")
                    .OrderBy(x => x.typeId)
                    .ToList();
            }
        }
        public static List<InfoEntryType> InfoEntryTypes
        {
            get
            {
                if (infoEntryTypes == null || ((DateTime.Now - dailyTypes_UpdateDateTime).TotalHours > 1)) { infoEntryTypes_Update(); }

                return infoEntryTypes;
            }
        }
        #endregion

        #region Other
        public static DateTime Last24hour = DateTime.Now.AddDays(-1);
        public static SysUser User => (SysUser)HttpContext.Current.Session["User"];
        public static bool Debug
        {
            get
            {
                return Skd.Web.Developer.IsDebug();
            }
        }
        #endregion
    }
}