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
	public partial class WorkVehicleController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "WorkVehicleCreate")]
		public string Create(string auth, int vehicleId, string numberplate, int typeId, bool isArchived, bool isTemporary, string lastLocation, string remark, DateTime createDateTime, int createUserId, string internalId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);
			    
                using (var context = ApiHelper.Db())
                {
                    WorkVehicle obj = new WorkVehicle()
                    {
                        vehicleId = vehicleId,
numberplate = numberplate,
typeId = typeId,
isArchived = isArchived,
isTemporary = isTemporary,
lastLocation = lastLocation,
remark = remark,
createDateTime = createDateTime,
createUserId = createUserId,
internalId = internalId
                    };
                    context.Entry(obj).State = System.Data.Entity.EntityState.Added;

                    int qtyChanges = context.SaveChanges();

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Retrieve", Name = "WorkVehicleRetrieve")]
		public string Retrieve(string auth, int vehicleId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    WorkVehicle obj = context.WorkVehicle.Find(vehicleId);

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Update", Name = "WorkVehicleUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "WorkVehicleDelete")]
		public string Delete(string auth, int vehicleId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    WorkVehicle obj = context.WorkVehicle.Find(vehicleId);
				    context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                    int qtyChanges = context.SaveChanges();

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Overview", Name = "WorkVehicleOverview")]
		public string Overview(string auth, int qtyToReturn = 10)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
				    var obj = context.WorkVehicle.Take(qtyToReturn).ToList();

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}
		#endregion
	}
}