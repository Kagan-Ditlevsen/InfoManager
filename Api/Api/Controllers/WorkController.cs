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
		public string Update()
		{
return "";
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