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
		public string Create(string auth, int addressId, string title, string gpsAddress, string gpsPlusCode, string systemRemark, string locationCode, bool isArchived)
		{
            try
            {
                AuthenticatedUser.Validate(auth);
			    
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
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Retrieve", Name = "WorkAddressRetrieve")]
		public string Retrieve(string auth, int addressId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    WorkAddress obj = context.WorkAddress.Find(addressId);

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Update", Name = "WorkAddressUpdate")]
		public string Update(string auth, int addressId, string? title, string? gpsAddress, string? gpsPlusCode, string? systemRemark, string? locationCode, bool? isArchived)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

                using (var context = ApiHelper.Db())
                {
                    var obj = context.WorkAddress.Single(x => addressId == addressId);
                    obj.addressId = addressId == null ? (int)addressId : obj.addressId; // isKey: True, isIdentity: True, isComputed: False;
obj.title = title.Length > 0 ? title : obj.title; // isKey: False, isIdentity: False, isComputed: False;
obj.gpsAddress = gpsAddress.Length > 0 ? gpsAddress : obj.gpsAddress; // isKey: False, isIdentity: False, isComputed: False;
obj.gpsPlusCode = gpsPlusCode.Length > 0 ? gpsPlusCode : obj.gpsPlusCode; // isKey: False, isIdentity: False, isComputed: False;
obj.systemRemark = systemRemark.Length > 0 ? systemRemark : obj.systemRemark; // isKey: False, isIdentity: False, isComputed: False;
obj.locationCode = locationCode.Length > 0 ? locationCode : obj.locationCode; // isKey: False, isIdentity: False, isComputed: False;
obj.isArchived = isArchived.HasValue ? (bool)isArchived : obj.isArchived; // isKey: False, isIdentity: False, isComputed: False

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

		[HttpGet("Delete", Name = "WorkAddressDelete")]
		public string Delete(string auth, int addressId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    WorkAddress obj = context.WorkAddress.Find(addressId);
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

		[HttpGet("Overview", Name = "WorkAddressOverview")]
		public string Overview(string auth, int qtyToReturn = 10)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
				    var obj = context.WorkAddress.Take(qtyToReturn).ToList();

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