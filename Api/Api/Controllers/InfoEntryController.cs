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
	public partial class InfoEntryController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "InfoEntryCreate")]
		public string Create(int entryId, int infoId, int typeId, string title, string entry, int sortOrder, string remark, DateTime createDateTime, int createUserId)
		{
                     
            
            using (var context = ApiHelper.Db())
            {
                InfoEntry obj = new InfoEntry()
                {
                    entryId = entryId,
infoId = infoId,
typeId = typeId,
title = title,
entry = entry,
sortOrder = sortOrder,
remark = remark,
createDateTime = createDateTime,
createUserId = createUserId
                };
                context.Entry(obj).State = System.Data.Entity.EntityState.Added;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Retrieve", Name = "InfoEntryRetrieve")]
		public string Retrieve(int entryId)
		{
			using (var context = ApiHelper.Db())
            {
                InfoEntry obj = context.InfoEntry.Find(entryId);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "InfoEntryUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "InfoEntryDelete")]
		public string Delete(int entryId)
		{
			using (var context = ApiHelper.Db())
            {
                InfoEntry obj = context.InfoEntry.Find(entryId);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "InfoEntryOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.InfoEntry.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}