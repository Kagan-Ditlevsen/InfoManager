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
	public partial class DailyTypeOptionController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "DailyTypeOptionCreate")]
		public string Create(int optionId, int typeId, string internalTitle, int sortOrder, bool isActive, string iconCss, string description)
		{
                     
            
            using (var context = ApiHelper.Db())
            {
                DailyTypeOption obj = new DailyTypeOption()
                {
                    optionId = optionId,
typeId = typeId,
internalTitle = internalTitle,
sortOrder = sortOrder,
isActive = isActive,
iconCss = iconCss,
description = description
                };
                context.Entry(obj).State = System.Data.Entity.EntityState.Added;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Retrieve", Name = "DailyTypeOptionRetrieve")]
		public string Retrieve(int optionId)
		{
			using (var context = ApiHelper.Db())
            {
                DailyTypeOption obj = context.DailyTypeOption.Find(optionId);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "DailyTypeOptionUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "DailyTypeOptionDelete")]
		public string Delete(int optionId)
		{
			using (var context = ApiHelper.Db())
            {
                DailyTypeOption obj = context.DailyTypeOption.Find(optionId);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "DailyTypeOptionOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.DailyTypeOption.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}