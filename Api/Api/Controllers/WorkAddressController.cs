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
	public partial class WorkAddressController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "WorkAddressCreate")]
		public string Create(int addressId, string title, string gpsAddress, string gpsPlusCode, string systemRemark, string locationCode, bool isArchived)
		{
                     
            
            using (var context = ApiHelper.Db())
            {
                WorkAddress obj = new WorkAddress()
                {
                    addressId = addressId,
title = title,
gpsAddress = gpsAddress,
gpsPlusCode = gpsPlusCode,
systemRemark = systemRemark,
locationCode = locationCode,
isArchived = isArchived
                };
                context.Entry(obj).State = System.Data.Entity.EntityState.Added;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Retrieve", Name = "WorkAddressRetrieve")]
		public string Retrieve(int addressId)
		{
			using (var context = ApiHelper.Db())
            {
                WorkAddress obj = context.WorkAddress.Find(addressId);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "WorkAddressUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "WorkAddressDelete")]
		public string Delete(int addressId)
		{
			using (var context = ApiHelper.Db())
            {
                WorkAddress obj = context.WorkAddress.Find(addressId);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "WorkAddressOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.WorkAddress.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}