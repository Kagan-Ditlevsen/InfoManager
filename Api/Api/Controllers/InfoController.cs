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
	public partial class InfoController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "InfoCreate")]
		public string Create(string auth, int infoId, int? categoryId, string title, string remark, DateTime createDateTime, int createUserId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);
			    
                using (var context = ApiHelper.Db())
                {
                    Info obj = new Info()
                    {
                        infoId = infoId,
categoryId = categoryId,
title = title,
remark = remark,
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

		[HttpGet("Retrieve", Name = "InfoRetrieve")]
		public string Retrieve(string auth, int infoId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    Info obj = context.Info.Find(infoId);

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Update", Name = "InfoUpdate")]
		public string Update(string auth, int infoId, int? categoryId, string? title, string? remark, DateTime? createDateTime, int? createUserId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

                using (var context = ApiHelper.Db())
                {
                    var obj = context.Info.Single(x => infoId == infoId);
                    obj.infoId = infoId == null ? (int)infoId : obj.infoId; // isKey: True, isIdentity: True, isComputed: False;
obj.categoryId = categoryId.HasValue ? (int)categoryId : obj.categoryId; // isKey: False, isIdentity: False, isComputed: False;
obj.title = title.Length > 0 ? title : obj.title; // isKey: False, isIdentity: False, isComputed: False;
obj.remark = remark.Length > 0 ? remark : obj.remark; // isKey: False, isIdentity: False, isComputed: False;
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

		[HttpGet("Delete", Name = "InfoDelete")]
		public string Delete(string auth, int infoId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    Info obj = context.Info.Find(infoId);
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

		[HttpGet("Overview", Name = "InfoOverview")]
		public string Overview(string auth, int qtyToReturn = 10)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
				    var obj = context.Info.Take(qtyToReturn).ToList();

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