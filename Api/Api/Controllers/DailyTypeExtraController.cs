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
		public string Create(string auth, int extraId, int typeId, string internalTitle, int sortOrder, bool isActive, bool isRequired, string inputType, string inputData)
		{
            try
            {
                AuthenticatedUser.Validate(auth);
			    
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
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Retrieve", Name = "DailyTypeExtraRetrieve")]
		public string Retrieve(string auth, int extraId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    DailyTypeExtra obj = context.DailyTypeExtra.Find(extraId);

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Update", Name = "DailyTypeExtraUpdate")]
		public string Update(string auth, int extraId, int? typeId, string? internalTitle, int? sortOrder, bool? isActive, bool? isRequired, string? inputType, string? inputData)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

                using (var context = ApiHelper.Db())
                {
                    var obj = context.DailyTypeExtra.Single(x => extraId == extraId);
                    obj.extraId = extraId == null ? (int)extraId : obj.extraId; // isKey: True, isIdentity: True, isComputed: False;
obj.typeId = typeId.HasValue ? (int)typeId : obj.typeId; // isKey: False, isIdentity: False, isComputed: False;
obj.internalTitle = internalTitle.Length > 0 ? internalTitle : obj.internalTitle; // isKey: False, isIdentity: False, isComputed: False;
obj.sortOrder = sortOrder.HasValue ? (int)sortOrder : obj.sortOrder; // isKey: False, isIdentity: False, isComputed: False;
obj.isActive = isActive.HasValue ? (bool)isActive : obj.isActive; // isKey: False, isIdentity: False, isComputed: False;
obj.isRequired = isRequired.HasValue ? (bool)isRequired : obj.isRequired; // isKey: False, isIdentity: False, isComputed: False;
obj.inputType = inputType.Length > 0 ? inputType : obj.inputType; // isKey: False, isIdentity: False, isComputed: False;
obj.inputData = inputData.Length > 0 ? inputData : obj.inputData; // isKey: False, isIdentity: False, isComputed: False

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

		[HttpGet("Delete", Name = "DailyTypeExtraDelete")]
		public string Delete(string auth, int extraId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    DailyTypeExtra obj = context.DailyTypeExtra.Find(extraId);
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

		[HttpGet("Overview", Name = "DailyTypeExtraOverview")]
		public string Overview(string auth, int qtyToReturn = 10)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
				    var obj = context.DailyTypeExtra.Take(qtyToReturn).ToList();

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