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
	public partial class WorkStatusEnumController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "WorkStatusEnumCreate")]
		public string Create(int statusId, string internalTitle)
		{
                     
            
            using (var context = ApiHelper.Db())
            {
                WorkStatusEnum obj = new WorkStatusEnum()
                {
                    statusId = statusId,
internalTitle = internalTitle
                };
                context.Entry(obj).State = System.Data.Entity.EntityState.Added;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Retrieve", Name = "WorkStatusEnumRetrieve")]
		public string Retrieve(int statusId)
		{
			using (var context = ApiHelper.Db())
            {
                WorkStatusEnum obj = context.WorkStatusEnum.Find(statusId);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "WorkStatusEnumUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "WorkStatusEnumDelete")]
		public string Delete(int statusId)
		{
			using (var context = ApiHelper.Db())
            {
                WorkStatusEnum obj = context.WorkStatusEnum.Find(statusId);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "WorkStatusEnumOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.WorkStatusEnum.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}