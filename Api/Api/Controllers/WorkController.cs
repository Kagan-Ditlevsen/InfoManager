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
	public partial class WorkController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "WorkCreate")]
		public string Create(string auth, DateTime? startDateTime, DateTime? endDateTime, string remark, string systemRemark, int? status, DateTime createDateTime, int createUserId, int? deleteUserId, DateTime? deleteDateTime, DateTime? finishedDateTime, int? finishedUserId, DateTime? archiveDateTime, int? archiveUserId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);
			    Guid workId = Guid.NewGuid();
                using (var context = ApiHelper.Db())
                {
                    Work obj = new Work()
                    {
                        workId = workId,
startDateTime = startDateTime,
endDateTime = endDateTime,
remark = remark,
systemRemark = systemRemark,
status = status,
createDateTime = createDateTime,
createUserId = createUserId,
deleteUserId = deleteUserId,
deleteDateTime = deleteDateTime,
finishedDateTime = finishedDateTime,
finishedUserId = finishedUserId,
archiveDateTime = archiveDateTime,
archiveUserId = archiveUserId
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

		[HttpGet("Retrieve", Name = "WorkRetrieve")]
		public string Retrieve(string auth, Guid workId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    Work obj = context.Work.Find(workId);

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Update", Name = "WorkUpdate")]
		public string Update(string auth, Guid workId, DateTime? startDateTime, DateTime? endDateTime, string? remark, string? systemRemark, int? status, DateTime? createDateTime, int? createUserId, int? deleteUserId, DateTime? deleteDateTime, DateTime? finishedDateTime, int? finishedUserId, DateTime? archiveDateTime, int? archiveUserId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

                using (var context = ApiHelper.Db())
                {
                    var obj = context.Work.Single(x => workId == workId);
                    obj.workId = workId == null ? (Guid)workId : obj.workId; // isKey: True, isIdentity: False, isComputed: False;
obj.startDateTime = startDateTime.HasValue ? (DateTime)startDateTime : obj.startDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.endDateTime = endDateTime.HasValue ? (DateTime)endDateTime : obj.endDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.remark = remark.Length > 0 ? remark : obj.remark; // isKey: False, isIdentity: False, isComputed: False;
obj.systemRemark = systemRemark.Length > 0 ? systemRemark : obj.systemRemark; // isKey: False, isIdentity: False, isComputed: False;
obj.status = status.HasValue ? (int)status : obj.status; // isKey: False, isIdentity: False, isComputed: False;
obj.createDateTime = createDateTime.HasValue ? (DateTime)createDateTime : obj.createDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.createUserId = createUserId.HasValue ? (int)createUserId : obj.createUserId; // isKey: False, isIdentity: False, isComputed: False;
obj.deleteUserId = deleteUserId.HasValue ? (int)deleteUserId : obj.deleteUserId; // isKey: False, isIdentity: False, isComputed: False;
obj.deleteDateTime = deleteDateTime.HasValue ? (DateTime)deleteDateTime : obj.deleteDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.finishedDateTime = finishedDateTime.HasValue ? (DateTime)finishedDateTime : obj.finishedDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.finishedUserId = finishedUserId.HasValue ? (int)finishedUserId : obj.finishedUserId; // isKey: False, isIdentity: False, isComputed: False;
obj.archiveDateTime = archiveDateTime.HasValue ? (DateTime)archiveDateTime : obj.archiveDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.archiveUserId = archiveUserId.HasValue ? (int)archiveUserId : obj.archiveUserId; // isKey: False, isIdentity: False, isComputed: False

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

		[HttpGet("Delete", Name = "WorkDelete")]
		public string Delete(string auth, Guid workId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    Work obj = context.Work.Find(workId);
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

		[HttpGet("Overview", Name = "WorkOverview")]
		public string Overview(string auth, int qtyToReturn = 10)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
				    var obj = context.Work.Take(qtyToReturn).ToList();

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