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
	public partial class WorkTaskTemplateController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "WorkTaskTemplateCreate")]
		public string Create(string templateId, int sortOrder, int typeId, DateTime? startDateTime, DateTime? endDateTime, string addressText, string vehicleNumberplate, string linkNumberplate, string dollyNumberplate, string trailerNumberplate, string remark, string systemRemark, byte? reportColumnNumber, DateTime createDateTime, int createUserId)
		{
                     
            
            using (var context = ApiHelper.Db())
            {
                WorkTaskTemplate obj = new WorkTaskTemplate()
                {
                    templateId = templateId,
sortOrder = sortOrder,
typeId = typeId,
startDateTime = startDateTime,
endDateTime = endDateTime,
addressText = addressText,
vehicleNumberplate = vehicleNumberplate,
linkNumberplate = linkNumberplate,
dollyNumberplate = dollyNumberplate,
trailerNumberplate = trailerNumberplate,
remark = remark,
systemRemark = systemRemark,
reportColumnNumber = reportColumnNumber,
createDateTime = createDateTime,
createUserId = createUserId
                };
                context.Entry(obj).State = System.Data.Entity.EntityState.Added;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Retrieve", Name = "WorkTaskTemplateRetrieve")]
		public string Retrieve(string templateId, int sortOrder)
		{
			using (var context = ApiHelper.Db())
            {
                WorkTaskTemplate obj = context.WorkTaskTemplate.Find(templateId, sortOrder);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "WorkTaskTemplateUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "WorkTaskTemplateDelete")]
		public string Delete(string templateId, int sortOrder)
		{
			using (var context = ApiHelper.Db())
            {
                WorkTaskTemplate obj = context.WorkTaskTemplate.Find(templateId, sortOrder);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "WorkTaskTemplateOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.WorkTaskTemplate.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}