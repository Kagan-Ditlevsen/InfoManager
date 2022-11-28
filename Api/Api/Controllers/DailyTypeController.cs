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
	public partial class DailyTypeController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "DailyTypeCreate")]
		public string Create(int typeId, int? parentTypeId, int? nextTypeId, string internalTitle, bool isActive, bool isFavorite, int sortOrder, int? defaultOptionId, string iconCss, string description)
		{
                     
            
            using (var context = ApiHelper.Db())
            {
                DailyType obj = new DailyType()
                {
                    typeId = typeId,
parentTypeId = parentTypeId,
nextTypeId = nextTypeId,
internalTitle = internalTitle,
isActive = isActive,
isFavorite = isFavorite,
sortOrder = sortOrder,
defaultOptionId = defaultOptionId,
iconCss = iconCss,
description = description
                };
                context.Entry(obj).State = System.Data.Entity.EntityState.Added;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Retrieve", Name = "DailyTypeRetrieve")]
		public string Retrieve(int typeId)
		{
			using (var context = ApiHelper.Db())
            {
                DailyType obj = context.DailyType.Find(typeId);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "DailyTypeUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "DailyTypeDelete")]
		public string Delete(int typeId)
		{
			using (var context = ApiHelper.Db())
            {
                DailyType obj = context.DailyType.Find(typeId);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "DailyTypeOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.DailyType.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}