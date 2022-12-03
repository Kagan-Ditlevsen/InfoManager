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
	public partial class FinanceController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "FinanceCreate")]
		public string Create(string auth, int financeId, DateTime receiptDateTime, string title, string shopId, string receiptId, string remark, DateTime createDateTime, int createUserId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);
			    
                using (var context = ApiHelper.Db())
                {
                    Finance obj = new Finance()
                    {
                        financeId = financeId,
receiptDateTime = receiptDateTime,
title = title,
shopId = shopId,
receiptId = receiptId,
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

		[HttpGet("Retrieve", Name = "FinanceRetrieve")]
		public string Retrieve(string auth, int financeId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    Finance obj = context.Finance.Find(financeId);

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Update", Name = "FinanceUpdate")]
		public string Update(string auth, int financeId, DateTime? receiptDateTime, string? title, string? shopId, string? receiptId, string? remark, DateTime? createDateTime, int? createUserId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

                using (var context = ApiHelper.Db())
                {
                    var obj = context.Finance.Single(x => financeId == financeId);
                    obj.financeId = financeId == null ? (int)financeId : obj.financeId; // isKey: True, isIdentity: True, isComputed: False;
obj.receiptDateTime = receiptDateTime.HasValue ? (DateTime)receiptDateTime : obj.receiptDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.title = title.Length > 0 ? title : obj.title; // isKey: False, isIdentity: False, isComputed: False;
obj.shopId = shopId.Length > 0 ? shopId : obj.shopId; // isKey: False, isIdentity: False, isComputed: False;
obj.receiptId = receiptId.Length > 0 ? receiptId : obj.receiptId; // isKey: False, isIdentity: False, isComputed: False;
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

		[HttpGet("Delete", Name = "FinanceDelete")]
		public string Delete(string auth, int financeId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    Finance obj = context.Finance.Find(financeId);
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

		[HttpGet("Overview", Name = "FinanceOverview")]
		public string Overview(string auth, int qtyToReturn = 10)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
				    var obj = context.Finance.Take(qtyToReturn).ToList();

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