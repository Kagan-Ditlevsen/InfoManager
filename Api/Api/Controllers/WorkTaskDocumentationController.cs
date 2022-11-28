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
		public string Create(Guid workId, Guid taskId, DateTime registerDateTime, string remark, DateTime createDateTime, int createUserId)
		{
                     
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

		[HttpGet("Retrieve", Name = "WorkTaskDocumentationRetrieve")]
		public string Retrieve(Guid documentationId)
		{
			using (var context = ApiHelper.Db())
            {
                WorkTaskDocumentation obj = context.WorkTaskDocumentation.Find(documentationId);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "WorkTaskDocumentationUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "WorkTaskDocumentationDelete")]
		public string Delete(Guid documentationId)
		{
			using (var context = ApiHelper.Db())
            {
                WorkTaskDocumentation obj = context.WorkTaskDocumentation.Find(documentationId);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "WorkTaskDocumentationOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.WorkTaskDocumentation.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}