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
	public partial class InfoCategoryController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "InfoCategoryCreate")]
		public string Create(string auth, int categoryId, int? parentCategoryId, string internalTitle, int sortOrder, DateTime createDateTime, int createUserId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);
			    
                using (var context = ApiHelper.Db())
                {
                    InfoCategory obj = new InfoCategory()
                    {
                        categoryId = categoryId,
parentCategoryId = parentCategoryId,
internalTitle = internalTitle,
sortOrder = sortOrder,
createDateTime = createDateTime,
createUserId = createUserId
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

		[HttpGet("Retrieve", Name = "InfoCategoryRetrieve")]
		public string Retrieve(string auth, int categoryId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    InfoCategory obj = context.InfoCategory.Find(categoryId);

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Update", Name = "InfoCategoryUpdate")]
		public string Update(string auth, int categoryId, int? parentCategoryId, string? internalTitle, int? sortOrder, DateTime? createDateTime, int? createUserId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

                using (var context = ApiHelper.Db())
                {
                    var obj = context.InfoCategory.Single(x => categoryId == categoryId);
                    obj.categoryId = categoryId == null ? (int)categoryId : obj.categoryId; // isKey: True, isIdentity: False, isComputed: False;
obj.parentCategoryId = parentCategoryId.HasValue ? (int)parentCategoryId : obj.parentCategoryId; // isKey: False, isIdentity: False, isComputed: False;
obj.internalTitle = internalTitle.Length > 0 ? internalTitle : obj.internalTitle; // isKey: False, isIdentity: False, isComputed: False;
obj.sortOrder = sortOrder.HasValue ? (int)sortOrder : obj.sortOrder; // isKey: False, isIdentity: False, isComputed: False;
obj.createDateTime = createDateTime.HasValue ? (DateTime)createDateTime : obj.createDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.createUserId = createUserId.HasValue ? (int)createUserId : obj.createUserId; // isKey: False, isIdentity: False, isComputed: False

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

		[HttpGet("Delete", Name = "InfoCategoryDelete")]
		public string Delete(string auth, int categoryId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    InfoCategory obj = context.InfoCategory.Find(categoryId);
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

		[HttpGet("Overview", Name = "InfoCategoryOverview")]
		public string Overview(string auth, int qtyToReturn = 10)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
				    var obj = context.InfoCategory.Take(qtyToReturn).ToList();

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