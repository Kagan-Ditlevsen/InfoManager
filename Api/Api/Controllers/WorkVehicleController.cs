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
		public string Update(string auth, int vehicleId, string? numberplate, int? typeId, bool? isArchived, bool? isTemporary, string? lastLocation, string? remark, DateTime? createDateTime, int? createUserId, string? internalId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

                using (var context = ApiHelper.Db())
                {
                    var obj = context.WorkVehicle.Single(x => vehicleId == vehicleId);
                    obj.vehicleId = vehicleId == null ? (int)vehicleId : obj.vehicleId; // isKey: True, isIdentity: True, isComputed: False;
obj.numberplate = numberplate.Length > 0 ? numberplate : obj.numberplate; // isKey: False, isIdentity: False, isComputed: False;
obj.typeId = typeId.HasValue ? (int)typeId : obj.typeId; // isKey: False, isIdentity: False, isComputed: False;
obj.isArchived = isArchived.HasValue ? (bool)isArchived : obj.isArchived; // isKey: False, isIdentity: False, isComputed: False;
obj.isTemporary = isTemporary.HasValue ? (bool)isTemporary : obj.isTemporary; // isKey: False, isIdentity: False, isComputed: False;
obj.lastLocation = lastLocation.Length > 0 ? lastLocation : obj.lastLocation; // isKey: False, isIdentity: False, isComputed: False;
obj.remark = remark.Length > 0 ? remark : obj.remark; // isKey: False, isIdentity: False, isComputed: False;
obj.createDateTime = createDateTime.HasValue ? (DateTime)createDateTime : obj.createDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.createUserId = createUserId.HasValue ? (int)createUserId : obj.createUserId; // isKey: False, isIdentity: False, isComputed: False;
obj.internalId = internalId.Length > 0 ? internalId : obj.internalId; // isKey: False, isIdentity: False, isComputed: False

                    context.Entry(obj).State = System.Data.Entity.EntityState.Modified;

                    int qtyChanges = context.SaveChanges();

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
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