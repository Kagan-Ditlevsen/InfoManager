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
	public partial class WorkTaskDocumentationController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "WorkTaskDocumentationCreate")]
		public string Create(string auth, Guid workId, Guid taskId, DateTime registerDateTime, string remark, DateTime createDateTime, int createUserId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);
			    Guid documentationId = Guid.NewGuid();
                using (var context = ApiHelper.Db())
                {
                    WorkTaskDocumentation obj = new WorkTaskDocumentation()
                    {
                        documentationId = documentationId,
workId = workId,
taskId = taskId,
registerDateTime = registerDateTime,
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

		[HttpGet("Retrieve", Name = "WorkTaskDocumentationRetrieve")]
		public string Retrieve(string auth, Guid documentationId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    WorkTaskDocumentation obj = context.WorkTaskDocumentation.Find(documentationId);

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Update", Name = "WorkTaskDocumentationUpdate")]
		public string Update(string auth, Guid documentationId, Guid? workId, Guid? taskId, DateTime? registerDateTime, string? remark, DateTime? createDateTime, int? createUserId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

                using (var context = ApiHelper.Db())
                {
                    var obj = context.WorkTaskDocumentation.Single(x => documentationId == documentationId && workId == workId && taskId == taskId);
                    obj.documentationId = documentationId == null ? (Guid)documentationId : obj.documentationId; // isKey: True, isIdentity: False, isComputed: False;
obj.workId = workId.HasValue ? (Guid)workId : obj.workId; // isKey: False, isIdentity: False, isComputed: False;
obj.taskId = taskId.HasValue ? (Guid)taskId : obj.taskId; // isKey: False, isIdentity: False, isComputed: False;
obj.registerDateTime = registerDateTime.HasValue ? (DateTime)registerDateTime : obj.registerDateTime; // isKey: False, isIdentity: False, isComputed: False;
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

		[HttpGet("Delete", Name = "WorkTaskDocumentationDelete")]
		public string Delete(string auth, Guid documentationId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    WorkTaskDocumentation obj = context.WorkTaskDocumentation.Find(documentationId);
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

		[HttpGet("Overview", Name = "WorkTaskDocumentationOverview")]
		public string Overview(string auth, int qtyToReturn = 10)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
				    var obj = context.WorkTaskDocumentation.Take(qtyToReturn).ToList();

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