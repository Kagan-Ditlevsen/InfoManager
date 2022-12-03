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
		public string Create(string auth, int typeId, string icon, string internalTitle)
		{
            try
            {
                AuthenticatedUser.Validate(auth);
			    
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
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Retrieve", Name = "WorkVehicleTypeRetrieve")]
		public string Retrieve(string auth, int typeId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    WorkVehicleType obj = context.WorkVehicleType.Find(typeId);

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Update", Name = "WorkVehicleTypeUpdate")]
		public string Update(string auth, int typeId, string? icon, string? internalTitle)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

                using (var context = ApiHelper.Db())
                {
                    var obj = context.WorkVehicleType.Single(x => typeId == typeId);
                    obj.typeId = typeId == null ? (int)typeId : obj.typeId; // isKey: True, isIdentity: False, isComputed: False;
obj.icon = icon.Length > 0 ? icon : obj.icon; // isKey: False, isIdentity: False, isComputed: False;
obj.internalTitle = internalTitle.Length > 0 ? internalTitle : obj.internalTitle; // isKey: False, isIdentity: False, isComputed: False

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

		[HttpGet("Delete", Name = "WorkVehicleTypeDelete")]
		public string Delete(string auth, int typeId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    WorkVehicleType obj = context.WorkVehicleType.Find(typeId);
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

		[HttpGet("Overview", Name = "WorkVehicleTypeOverview")]
		public string Overview(string auth, int qtyToReturn = 10)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
				    var obj = context.WorkVehicleType.Take(qtyToReturn).ToList();

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