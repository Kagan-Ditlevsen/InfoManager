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
		public string Create(Guid workId, int typeId, int sortOrder, DateTime? startDateTime, DateTime? endDateTime, string addressText, string vehicleText, string vehicleNumberplate, string linkNumberplate, string dollyNumberplate, string trailerNumberplate, string remark, string systemRemark, DateTime createDateTime, int createUserId, DateTime? deleteDateTime, int? deleteUserId, bool? isFinished, byte reportColumnNumber, string transportId, string freighterSetup, string addressSetup)
		{
                     
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

		[HttpGet("Retrieve", Name = "WorkTaskRetrieve")]
		public string Retrieve(Guid taskId)
		{
			using (var context = ApiHelper.Db())
            {
                WorkTask obj = context.WorkTask.Find(taskId);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "WorkTaskUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "WorkTaskDelete")]
		public string Delete(Guid taskId)
		{
			using (var context = ApiHelper.Db())
            {
                WorkTask obj = context.WorkTask.Find(taskId);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "WorkTaskOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.WorkTask.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}