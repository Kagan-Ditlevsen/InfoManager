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
	public partial class SysUserTimesheetController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "SysUserTimesheetCreate")]
		public string Create(int userId, DateTime? startDateTime, DateTime? endDateTime, string remark, DateTime createDateTime, int createUserId)
		{
                     
            Guid timesheetId = Guid.NewGuid();
            using (var context = ApiHelper.Db())
            {
                SysUserTimesheet obj = new SysUserTimesheet()
                {
                    timesheetId = timesheetId,
userId = userId,
startDateTime = startDateTime,
endDateTime = endDateTime,
remark = remark,
createDateTime = createDateTime,
createUserId = createUserId
                };
                context.Entry(obj).State = System.Data.Entity.EntityState.Added;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Retrieve", Name = "SysUserTimesheetRetrieve")]
		public string Retrieve(Guid timesheetId)
		{
			using (var context = ApiHelper.Db())
            {
                SysUserTimesheet obj = context.SysUserTimesheet.Find(timesheetId);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "SysUserTimesheetUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "SysUserTimesheetDelete")]
		public string Delete(Guid timesheetId)
		{
			using (var context = ApiHelper.Db())
            {
                SysUserTimesheet obj = context.SysUserTimesheet.Find(timesheetId);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "SysUserTimesheetOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.SysUserTimesheet.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}