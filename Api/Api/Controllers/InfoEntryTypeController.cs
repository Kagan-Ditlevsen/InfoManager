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
	public partial class InfoEntryTypeController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "InfoEntryTypeCreate")]
		public string Create(int typeId, string internalTitle, DateTime createDateTime, int? createUserId)
		{
                     
            
            using (var context = ApiHelper.Db())
            {
                InfoEntryType obj = new InfoEntryType()
                {
                    typeId = typeId,
internalTitle = internalTitle,
createDateTime = createDateTime,
createUserId = createUserId
                };
                context.Entry(obj).State = System.Data.Entity.EntityState.Added;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Retrieve", Name = "InfoEntryTypeRetrieve")]
		public string Retrieve(int typeId)
		{
			using (var context = ApiHelper.Db())
            {
                InfoEntryType obj = context.InfoEntryType.Find(typeId);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "InfoEntryTypeUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "InfoEntryTypeDelete")]
		public string Delete(int typeId)
		{
			using (var context = ApiHelper.Db())
            {
                InfoEntryType obj = context.InfoEntryType.Find(typeId);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "InfoEntryTypeOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.InfoEntryType.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}