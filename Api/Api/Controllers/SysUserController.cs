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
		public string Create(string auth, int userId, string firstName, string lastName, string emailAddress, string password, DateTime? statLastLogon, DateTime? statGoodMorning, DateTime? statGoodNight, int? statCigQty, int isAwaken, int? statSleepMs, int? statAwakenMs, DateTime? modifyDateTime, DateTime createDateTime)
		{
            try
            {
                AuthenticatedUser.Validate(auth);
			    
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
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Retrieve", Name = "SysUserRetrieve")]
		public string Retrieve(string auth, int userId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    SysUser obj = context.SysUser.Find(userId);

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Update", Name = "SysUserUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "SysUserDelete")]
		public string Delete(string auth, int userId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    SysUser obj = context.SysUser.Find(userId);
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

		[HttpGet("Overview", Name = "SysUserOverview")]
		public string Overview(string auth, int qtyToReturn = 10)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
				    var obj = context.SysUser.Take(qtyToReturn).ToList();

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