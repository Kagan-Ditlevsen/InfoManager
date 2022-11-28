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
	public partial class DailyTypeExtraController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "DailyTypeExtraCreate")]
		public string Create(int extraId, int typeId, string internalTitle, int sortOrder, bool isActive, bool isRequired, string inputType, string inputData)
		{
                     
            
            using (var context = ApiHelper.Db())
            {
                DailyTypeExtra obj = new DailyTypeExtra()
                {
                    extraId = extraId,
typeId = typeId,
internalTitle = internalTitle,
sortOrder = sortOrder,
isActive = isActive,
isRequired = isRequired,
inputType = inputType,
inputData = inputData
                };
                context.Entry(obj).State = System.Data.Entity.EntityState.Added;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Retrieve", Name = "DailyTypeExtraRetrieve")]
		public string Retrieve(int extraId)
		{
			using (var context = ApiHelper.Db())
            {
                DailyTypeExtra obj = context.DailyTypeExtra.Find(extraId);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "DailyTypeExtraUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "DailyTypeExtraDelete")]
		public string Delete(int extraId)
		{
			using (var context = ApiHelper.Db())
            {
                DailyTypeExtra obj = context.DailyTypeExtra.Find(extraId);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "DailyTypeExtraOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.DailyTypeExtra.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}