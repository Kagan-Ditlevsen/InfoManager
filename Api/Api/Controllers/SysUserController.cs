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
	public partial class SysUserController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "SysUserCreate")]
		public string Create(int userId, string firstName, string lastName, string emailAddress, string password, DateTime? statLastLogon, DateTime? statGoodMorning, DateTime? statGoodNight, int? statCigQty, int isAwaken, int? statSleepMs, int? statAwakenMs, DateTime? modifyDateTime, DateTime createDateTime)
		{
                     
            
            using (var context = ApiHelper.Db())
            {
                SysUser obj = new SysUser()
                {
                    userId = userId,
firstName = firstName,
lastName = lastName,
emailAddress = emailAddress,
password = password,
statLastLogon = statLastLogon,
statGoodMorning = statGoodMorning,
statGoodNight = statGoodNight,
statCigQty = statCigQty,
isAwaken = isAwaken,
statSleepMs = statSleepMs,
statAwakenMs = statAwakenMs,
modifyDateTime = modifyDateTime,
createDateTime = createDateTime
                };
                context.Entry(obj).State = System.Data.Entity.EntityState.Added;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Retrieve", Name = "SysUserRetrieve")]
		public string Retrieve(int userId)
		{
			using (var context = ApiHelper.Db())
            {
                SysUser obj = context.SysUser.Find(userId);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "SysUserUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "SysUserDelete")]
		public string Delete(int userId)
		{
			using (var context = ApiHelper.Db())
            {
                SysUser obj = context.SysUser.Find(userId);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "SysUserOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.SysUser.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}