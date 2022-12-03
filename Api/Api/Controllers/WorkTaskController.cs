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
	public partial class WorkTaskController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "WorkTaskCreate")]
		public string Create(string auth, Guid workId, int typeId, int sortOrder, DateTime? startDateTime, DateTime? endDateTime, string addressText, string vehicleText, string vehicleNumberplate, string linkNumberplate, string dollyNumberplate, string trailerNumberplate, string remark, string systemRemark, DateTime createDateTime, int createUserId, DateTime? deleteDateTime, int? deleteUserId, bool? isFinished, byte reportColumnNumber, string transportId, string freighterSetup, string addressSetup)
		{
            try
            {
                AuthenticatedUser.Validate(auth);
			    Guid taskId = Guid.NewGuid();
                using (var context = ApiHelper.Db())
                {
                    WorkTask obj = new WorkTask()
                    {
                        taskId = taskId,
workId = workId,
typeId = typeId,
sortOrder = sortOrder,
startDateTime = startDateTime,
endDateTime = endDateTime,
addressText = addressText,
vehicleText = vehicleText,
vehicleNumberplate = vehicleNumberplate,
linkNumberplate = linkNumberplate,
dollyNumberplate = dollyNumberplate,
trailerNumberplate = trailerNumberplate,
remark = remark,
systemRemark = systemRemark,
createDateTime = createDateTime,
createUserId = createUserId,
deleteDateTime = deleteDateTime,
deleteUserId = deleteUserId,
isFinished = isFinished,
reportColumnNumber = reportColumnNumber,
transportId = transportId,
freighterSetup = freighterSetup,
addressSetup = addressSetup
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

		[HttpGet("Retrieve", Name = "WorkTaskRetrieve")]
		public string Retrieve(string auth, Guid taskId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    WorkTask obj = context.WorkTask.Find(taskId);

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Update", Name = "WorkTaskUpdate")]
		public string Update(string auth, Guid taskId, Guid? workId, int? typeId, int? sortOrder, DateTime? startDateTime, DateTime? endDateTime, string? addressText, string? vehicleText, string? vehicleNumberplate, string? linkNumberplate, string? dollyNumberplate, string? trailerNumberplate, string? remark, string? systemRemark, DateTime? createDateTime, int? createUserId, DateTime? deleteDateTime, int? deleteUserId, bool? isFinished, byte? reportColumnNumber, string? transportId, string? freighterSetup, string? addressSetup)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

                using (var context = ApiHelper.Db())
                {
                    var obj = context.WorkTask.Single(x => taskId == taskId && workId == workId);
                    obj.taskId = taskId == null ? (Guid)taskId : obj.taskId; // isKey: True, isIdentity: False, isComputed: False;
obj.workId = workId.HasValue ? (Guid)workId : obj.workId; // isKey: False, isIdentity: False, isComputed: False;
obj.typeId = typeId.HasValue ? (int)typeId : obj.typeId; // isKey: False, isIdentity: False, isComputed: False;
obj.sortOrder = sortOrder.HasValue ? (int)sortOrder : obj.sortOrder; // isKey: False, isIdentity: False, isComputed: False;
obj.startDateTime = startDateTime.HasValue ? (DateTime)startDateTime : obj.startDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.endDateTime = endDateTime.HasValue ? (DateTime)endDateTime : obj.endDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.addressText = addressText.Length > 0 ? addressText : obj.addressText; // isKey: False, isIdentity: False, isComputed: False;
obj.vehicleText = vehicleText.Length > 0 ? vehicleText : obj.vehicleText; // isKey: False, isIdentity: False, isComputed: False;
obj.vehicleNumberplate = vehicleNumberplate.Length > 0 ? vehicleNumberplate : obj.vehicleNumberplate; // isKey: False, isIdentity: False, isComputed: False;
obj.linkNumberplate = linkNumberplate.Length > 0 ? linkNumberplate : obj.linkNumberplate; // isKey: False, isIdentity: False, isComputed: False;
obj.dollyNumberplate = dollyNumberplate.Length > 0 ? dollyNumberplate : obj.dollyNumberplate; // isKey: False, isIdentity: False, isComputed: False;
obj.trailerNumberplate = trailerNumberplate.Length > 0 ? trailerNumberplate : obj.trailerNumberplate; // isKey: False, isIdentity: False, isComputed: False;
obj.remark = remark.Length > 0 ? remark : obj.remark; // isKey: False, isIdentity: False, isComputed: False;
obj.systemRemark = systemRemark.Length > 0 ? systemRemark : obj.systemRemark; // isKey: False, isIdentity: False, isComputed: False;
obj.createDateTime = createDateTime.HasValue ? (DateTime)createDateTime : obj.createDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.createUserId = createUserId.HasValue ? (int)createUserId : obj.createUserId; // isKey: False, isIdentity: False, isComputed: False;
obj.deleteDateTime = deleteDateTime.HasValue ? (DateTime)deleteDateTime : obj.deleteDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.deleteUserId = deleteUserId.HasValue ? (int)deleteUserId : obj.deleteUserId; // isKey: False, isIdentity: False, isComputed: False;
obj.isFinished = isFinished.HasValue ? (bool)isFinished : obj.isFinished; // isKey: False, isIdentity: False, isComputed: True;
obj.reportColumnNumber = reportColumnNumber.HasValue ? (byte)reportColumnNumber : obj.reportColumnNumber; // isKey: False, isIdentity: False, isComputed: False;
obj.transportId = transportId.Length > 0 ? transportId : obj.transportId; // isKey: False, isIdentity: False, isComputed: False;
obj.freighterSetup = freighterSetup.Length > 0 ? freighterSetup : obj.freighterSetup; // isKey: False, isIdentity: False, isComputed: False;
obj.addressSetup = addressSetup.Length > 0 ? addressSetup : obj.addressSetup; // isKey: False, isIdentity: False, isComputed: False

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

		[HttpGet("Delete", Name = "WorkTaskDelete")]
		public string Delete(string auth, Guid taskId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    WorkTask obj = context.WorkTask.Find(taskId);
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

		[HttpGet("Overview", Name = "WorkTaskOverview")]
		public string Overview(string auth, int qtyToReturn = 10)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
				    var obj = context.WorkTask.Take(qtyToReturn).ToList();

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