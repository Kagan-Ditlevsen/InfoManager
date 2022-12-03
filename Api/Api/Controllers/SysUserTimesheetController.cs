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
		public string Create(string auth, int userId, DateTime? startDateTime, DateTime? endDateTime, string remark, DateTime createDateTime, int createUserId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);
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
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Retrieve", Name = "SysUserTimesheetRetrieve")]
		public string Retrieve(string auth, Guid timesheetId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    SysUserTimesheet obj = context.SysUserTimesheet.Find(timesheetId);

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Update", Name = "SysUserTimesheetUpdate")]
		public string Update(string auth, Guid timesheetId, int? userId, DateTime? startDateTime, DateTime? endDateTime, string? remark, DateTime? createDateTime, int? createUserId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

                using (var context = ApiHelper.Db())
                {
                    var obj = context.SysUserTimesheet.Single(x => timesheetId == timesheetId);
                    obj.timesheetId = timesheetId == null ? (Guid)timesheetId : obj.timesheetId; // isKey: True, isIdentity: False, isComputed: False;
obj.userId = userId.HasValue ? (int)userId : obj.userId; // isKey: False, isIdentity: False, isComputed: False;
obj.startDateTime = startDateTime.HasValue ? (DateTime)startDateTime : obj.startDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.endDateTime = endDateTime.HasValue ? (DateTime)endDateTime : obj.endDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.remark = remark.Length > 0 ? remark : obj.remark; // isKey: False, isIdentity: False, isComputed: False;
obj.createDateTime = createDateTime.HasValue ? (DateTime)createDateTime : obj.createDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.createUserId = createUserId.HasValue ? (int)createUserId : obj.createUserId; // isKey: False, isIdentity: False, isComputed: False

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

		[HttpGet("Delete", Name = "SysUserTimesheetDelete")]
		public string Delete(string auth, Guid timesheetId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    SysUserTimesheet obj = context.SysUserTimesheet.Find(timesheetId);
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

		[HttpGet("Overview", Name = "SysUserTimesheetOverview")]
		public string Overview(string auth, int qtyToReturn = 10)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
				    var obj = context.SysUserTimesheet.Take(qtyToReturn).ToList();

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