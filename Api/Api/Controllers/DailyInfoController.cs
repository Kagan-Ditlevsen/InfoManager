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
		public string Create(int extraId, string entry, DateTime createDateTime, int createUserId)
		{
                     
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

		[HttpGet("Retrieve", Name = "DailyInfoRetrieve")]
		public string Retrieve(Guid dailyId, int extraId)
		{
			using (var context = ApiHelper.Db())
            {
                DailyInfo obj = context.DailyInfo.Find(dailyId, extraId);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "DailyInfoUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "DailyInfoDelete")]
		public string Delete(Guid dailyId, int extraId)
		{
			using (var context = ApiHelper.Db())
            {
                DailyInfo obj = context.DailyInfo.Find(dailyId, extraId);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "DailyInfoOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.DailyInfo.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}