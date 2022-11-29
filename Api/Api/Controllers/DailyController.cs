using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using infomanager.DAL;

namespace infomanager.Api
{
    [ApiController]
    [Route("[controller]")]
    public partial class DailyController : CommonController
    {
        #region Done
        [HttpGet("Create", Name = "DailyCreate")]
        public string Create(string auth, DateTime registerDateTime, int typeId, int? optionId, string remark, DateTime createDateTime, int createUserId)
        {
            try
            {
                AuthenticatedUser.Validate(auth);
                Guid dailyId = Guid.NewGuid();
                using (var context = ApiHelper.Db())
                {
                    Daily obj = new Daily()
                    {
                        dailyId = dailyId,
                        registerDateTime = registerDateTime,
                        typeId = typeId,
                        optionId = optionId,
                        remark = remark,
                        createDateTime = createDateTime,
                        createUserId = createUserId
                    };
                    context.Entry(obj).State = System.Data.Entity.EntityState.Added;

                    int qtyChanges = context.SaveChanges();

                    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
        }

        [HttpGet("Retrieve", Name = "DailyRetrieve")]
        public string Retrieve(string auth, Guid dailyId)
        {
            try
            {
                AuthenticatedUser.Validate(auth);

                using (var context = ApiHelper.Db())
                {
                    Daily obj = context.Daily.Find(dailyId);

                    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
        }

        [HttpGet("Update", Name = "DailyUpdate")]
        public string Update()
        {
            return "";
        }

        [HttpGet("Delete", Name = "DailyDelete")]
        public string Delete(string auth, Guid dailyId)
        {
            try
            {
                AuthenticatedUser.Validate(auth);

                using (var context = ApiHelper.Db())
                {
                    Daily obj = context.Daily.Find(dailyId);
                    context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                    int qtyChanges = context.SaveChanges();

                    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
        }

        [HttpGet("Overview", Name = "DailyOverview")]
        public string Overview(string auth, int qtyToReturn = 10)
        {
            try
            {
                AuthenticatedUser.Validate(auth);

                using (var context = ApiHelper.Db())
                {
                    var obj = context.Daily.Take(qtyToReturn).ToList();

                    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
        }
        #endregion
    }
}