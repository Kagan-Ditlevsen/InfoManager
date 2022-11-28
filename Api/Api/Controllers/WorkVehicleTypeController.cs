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
	public partial class WorkVehicleTypeController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "WorkVehicleTypeCreate")]
		public string Create(int typeId, string icon, string internalTitle)
		{
                     
            
            using (var context = ApiHelper.Db())
            {
                WorkVehicleType obj = new WorkVehicleType()
                {
                    typeId = typeId,
icon = icon,
internalTitle = internalTitle
                };
                context.Entry(obj).State = System.Data.Entity.EntityState.Added;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Retrieve", Name = "WorkVehicleTypeRetrieve")]
		public string Retrieve(int typeId)
		{
			using (var context = ApiHelper.Db())
            {
                WorkVehicleType obj = context.WorkVehicleType.Find(typeId);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "WorkVehicleTypeUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "WorkVehicleTypeDelete")]
		public string Delete(int typeId)
		{
			using (var context = ApiHelper.Db())
            {
                WorkVehicleType obj = context.WorkVehicleType.Find(typeId);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "WorkVehicleTypeOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.WorkVehicleType.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}