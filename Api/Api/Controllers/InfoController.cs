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
	public partial class InfoController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "InfoCreate")]
		public string Create(int infoId, int? categoryId, string title, string remark, DateTime createDateTime, int createUserId)
		{
                     
            
            using (var context = ApiHelper.Db())
            {
                Info obj = new Info()
                {
                    infoId = infoId,
categoryId = categoryId,
title = title,
remark = remark,
createDateTime = createDateTime,
createUserId = createUserId
                };
                context.Entry(obj).State = System.Data.Entity.EntityState.Added;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Retrieve", Name = "InfoRetrieve")]
		public string Retrieve(int infoId)
		{
			using (var context = ApiHelper.Db())
            {
                Info obj = context.Info.Find(infoId);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "InfoUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "InfoDelete")]
		public string Delete(int infoId)
		{
			using (var context = ApiHelper.Db())
            {
                Info obj = context.Info.Find(infoId);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "InfoOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.Info.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}