using System;
using System.Collections.Generic;
using System.Linq;
using Skd.Web;
using System.Web;

namespace InfoMan.Models
{
    public partial class Daily
    {
        public HtmlString InfoesAsString()
        {
            string rtn = string.Join(", ", DailyInfo.Where(x => !string.IsNullOrEmpty(x.entry)).Select(x => x.DailyTypeExtra.internalTitle + ": " + x.entry).ToArray());
            return new HtmlString(rtn);
        }
        public HtmlString Title(bool includeIcon = true)
        {
            string rtn = "";
            if (includeIcon)
            {
                rtn += Common.DailyTypes.Single(x => x.typeId == typeId).Icon() + " ";
            }
            rtn += Common.DailyTypes.Single(x => x.typeId == typeId).internalTitle;
            if (optionId.HasValue)
            {
                rtn += ", " + Common.DailyTypeOptions.Single(x => x.optionId == optionId.Value).internalTitle;
            }
            return new HtmlString(rtn);
        }
        #region CRUDO
        public static DbReturnValue Create(int typeId, int? optionId, DateTime? registerDateTime, string remark, Dictionary<int, string> extras)
        {
            try
            {
                using (var db = new Db())
                {
                    var create = new Daily()
                    {
                        dailyId = Guid.NewGuid(),
                        registerDateTime = registerDateTime ?? DateTime.Now,
                        typeId = typeId,
                        optionId = optionId ?? null,
                        remark = remark,
                        createDateTime = DateTime.Now,
                        createUserId = Common.User.userId
                    };
                    foreach (var item in extras.Where(x => !string.IsNullOrEmpty(x.Value)))
                    {
                        var extra = new DailyInfo()
                        {
                            extraId = item.Key,
                            entry = item.Value,
                            createDateTime = DateTime.Now,
                            createUserId = Common.User.userId
                        };
                        create.DailyInfo.Add(extra);
                    }
                    db.Daily.Add(create);
                    db.Entry(create).State = System.Data.Entity.EntityState.Added;
                    db.SaveChanges();

                    Common.User.Stat.Reload();

                    return new DbReturnValue()
                    {
                        Success = true,
                        RowIdsAffected = create.dailyId.ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                return new DbReturnValue()
                {
                    Success = false,
                    Error = ex
                };
            }
        }
        public static Daily Retrieve(Guid dailyId)
        {
            using (var db = new Db())
            {
                return db.Daily
                    .Include("DailyType")
                    .Include("DailyType.DailyTypeOption")
                    .Include("DailyInfo")
                    .Include("DailyType.DailyTypeExtra")
                    .Single(x => x.dailyId == dailyId);
            }
        }
        public static DbReturnValue Update(Guid dailyId, int typeId, int? optionId, DateTime? registerDateTime, string remark, Dictionary<int, string> extras)
        {
            bool success = false;
            using (var db = new Db())
            {
                // can not use Retrieve(dailyId) as it sets relationships
                var update = db.Daily.Single(x => x.dailyId == dailyId);
                update.registerDateTime = registerDateTime ?? DateTime.Now;
                update.typeId = typeId;
                update.optionId = optionId ?? null;
                update.remark = remark;

                foreach (var item in extras)
                {
                    var extra = db.DailyInfo.SingleOrDefault(x => x.extraId == item.Key && x.dailyId == dailyId);
                    if (extra == null)
                    {
                        if (!string.IsNullOrEmpty(item.Value))
                        {
                            extra = new DailyInfo()
                            {
                                extraId = item.Key,
                                entry = item.Value,
                                createDateTime = DateTime.Now,
                                createUserId = Common.User.userId
                            };
                            db.Entry(extra).State = System.Data.Entity.EntityState.Added;
                            update.DailyInfo.Add(extra);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item.Value))
                        {
                            extra.entry = item.Value;
                            db.Entry(extra).State = System.Data.Entity.EntityState.Modified;
                        }
                        else
                        {
                            db.Entry(extra).State = System.Data.Entity.EntityState.Deleted;
                        }
                    }
                }

                db.Entry(update).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                Common.User.Stat.Reload();
            }

            return new DbReturnValue()
            {
                Success = success,
                RowIdsAffected = dailyId.ToString()
            };
        }
        public static DbReturnValue Delete(Guid dailyId)
        {
            try
            {
                using (var db = new Db())
                {
                    var delete = db.Daily.Single(x => x.dailyId == dailyId);
                    db.Entry(delete).State = System.Data.Entity.EntityState.Deleted;
                    db.SaveChanges();

                    if (Common.User != null)
                    {
                        Common.User.Stat.Reload();
                    }
                    return new DbReturnValue
                    {
                        Success = true,
                        RowIdsAffected = dailyId.ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                return new DbReturnValue
                {
                    Success = false,
                    RowIdsAffected = dailyId.ToString(),
                    Error = ex
                };
            }
        }
        public static List<Daily> Overview(int userId)
        {
            using (var db = new Db())
            {
                return db.Daily
                    .Include("DailyType")
                    .Include("DailyType.DailyTypeOption")
                    .Include("DailyInfo")
                    .Include("DailyType.DailyTypeExtra")
                    .Where(x => x.createUserId == userId)
                    .ToList();
            }
        }
        #endregion
    }
}
