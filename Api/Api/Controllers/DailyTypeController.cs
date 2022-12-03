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
		public string Create(string auth, int typeId, int? parentTypeId, int? nextTypeId, string internalTitle, bool isActive, bool isFavorite, int sortOrder, int? defaultOptionId, string iconCss, string description)
		{
            try
            {
                AuthenticatedUser.Validate(auth);
			    
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
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Retrieve", Name = "DailyTypeRetrieve")]
		public string Retrieve(string auth, int typeId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    DailyType obj = context.DailyType.Find(typeId);

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Update", Name = "DailyTypeUpdate")]
		public string Update(string auth, int typeId, int? parentTypeId, int? nextTypeId, string? internalTitle, bool? isActive, bool? isFavorite, int? sortOrder, int? defaultOptionId, string? iconCss, string? description)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

                using (var context = ApiHelper.Db())
                {
                    var obj = context.DailyType.Single(x => typeId == typeId);
                    obj.typeId = typeId == null ? (int)typeId : obj.typeId; // isKey: True, isIdentity: False, isComputed: False;
obj.parentTypeId = parentTypeId.HasValue ? (int)parentTypeId : obj.parentTypeId; // isKey: False, isIdentity: False, isComputed: False;
obj.nextTypeId = nextTypeId.HasValue ? (int)nextTypeId : obj.nextTypeId; // isKey: False, isIdentity: False, isComputed: False;
obj.internalTitle = internalTitle.Length > 0 ? internalTitle : obj.internalTitle; // isKey: False, isIdentity: False, isComputed: False;
obj.isActive = isActive.HasValue ? (bool)isActive : obj.isActive; // isKey: False, isIdentity: False, isComputed: False;
obj.isFavorite = isFavorite.HasValue ? (bool)isFavorite : obj.isFavorite; // isKey: False, isIdentity: False, isComputed: False;
obj.sortOrder = sortOrder.HasValue ? (int)sortOrder : obj.sortOrder; // isKey: False, isIdentity: False, isComputed: False;
obj.defaultOptionId = defaultOptionId.HasValue ? (int)defaultOptionId : obj.defaultOptionId; // isKey: False, isIdentity: False, isComputed: False;
obj.iconCss = iconCss.Length > 0 ? iconCss : obj.iconCss; // isKey: False, isIdentity: False, isComputed: False;
obj.description = description.Length > 0 ? description : obj.description; // isKey: False, isIdentity: False, isComputed: False

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

		[HttpGet("Delete", Name = "DailyTypeDelete")]
		public string Delete(string auth, int typeId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    DailyType obj = context.DailyType.Find(typeId);
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

		[HttpGet("Overview", Name = "DailyTypeOverview")]
		public string Overview(string auth, int qtyToReturn = 10)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
				    var obj = context.DailyType.Take(qtyToReturn).ToList();

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