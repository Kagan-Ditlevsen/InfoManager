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
	public partial class DailyInfoController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "DailyInfoCreate")]
		public string Create(string auth, int extraId, string entry, DateTime createDateTime, int createUserId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);
			    Guid dailyId = Guid.NewGuid();
                using (var context = ApiHelper.Db())
                {
                    DailyInfo obj = new DailyInfo()
                    {
                        dailyId = dailyId,
extraId = extraId,
entry = entry,
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

		[HttpGet("Retrieve", Name = "DailyInfoRetrieve")]
		public string Retrieve(string auth, Guid dailyId, int extraId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    DailyInfo obj = context.DailyInfo.Find(dailyId, extraId);

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Update", Name = "DailyInfoUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "DailyInfoDelete")]
		public string Delete(string auth, Guid dailyId, int extraId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    DailyInfo obj = context.DailyInfo.Find(dailyId, extraId);
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

		[HttpGet("Overview", Name = "DailyInfoOverview")]
		public string Overview(string auth, int qtyToReturn = 10)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
				    var obj = context.DailyInfo.Take(qtyToReturn).ToList();

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