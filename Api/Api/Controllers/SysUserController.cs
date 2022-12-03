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
		public string Update(string auth, int userId, string? firstName, string? lastName, string? emailAddress, string? password, DateTime? statLastLogon, DateTime? statGoodMorning, DateTime? statGoodNight, int? statCigQty, int? isAwaken, int? statSleepMs, int? statAwakenMs, DateTime? modifyDateTime, DateTime? createDateTime)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

                using (var context = ApiHelper.Db())
                {
                    var obj = context.SysUser.Single(x => userId == userId);
                    obj.userId = userId == null ? (int)userId : obj.userId; // isKey: True, isIdentity: True, isComputed: False;
obj.firstName = firstName.Length > 0 ? firstName : obj.firstName; // isKey: False, isIdentity: False, isComputed: False;
obj.lastName = lastName.Length > 0 ? lastName : obj.lastName; // isKey: False, isIdentity: False, isComputed: False;
obj.emailAddress = emailAddress.Length > 0 ? emailAddress : obj.emailAddress; // isKey: False, isIdentity: False, isComputed: False;
obj.password = password.Length > 0 ? password : obj.password; // isKey: False, isIdentity: False, isComputed: False;
obj.statLastLogon = statLastLogon.HasValue ? (DateTime)statLastLogon : obj.statLastLogon; // isKey: False, isIdentity: False, isComputed: False;
obj.statGoodMorning = statGoodMorning.HasValue ? (DateTime)statGoodMorning : obj.statGoodMorning; // isKey: False, isIdentity: False, isComputed: False;
obj.statGoodNight = statGoodNight.HasValue ? (DateTime)statGoodNight : obj.statGoodNight; // isKey: False, isIdentity: False, isComputed: False;
obj.statCigQty = statCigQty.HasValue ? (int)statCigQty : obj.statCigQty; // isKey: False, isIdentity: False, isComputed: False;
obj.isAwaken = isAwaken.HasValue ? (int)isAwaken : obj.isAwaken; // isKey: False, isIdentity: False, isComputed: True;
obj.statSleepMs = statSleepMs.HasValue ? (int)statSleepMs : obj.statSleepMs; // isKey: False, isIdentity: False, isComputed: True;
obj.statAwakenMs = statAwakenMs.HasValue ? (int)statAwakenMs : obj.statAwakenMs; // isKey: False, isIdentity: False, isComputed: True;
obj.modifyDateTime = modifyDateTime.HasValue ? (DateTime)modifyDateTime : obj.modifyDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.createDateTime = createDateTime.HasValue ? (DateTime)createDateTime : obj.createDateTime; // isKey: False, isIdentity: False, isComputed: False

                    context.Entry(obj).State = System.Data.Entity.EntityState.Modified;

                    int qtyChanges = context.SaveChanges();

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
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