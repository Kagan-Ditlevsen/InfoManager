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
	public partial class FinanceLineController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "FinanceLineCreate")]
		public string Create(string auth, int lineId, int financeId, int? accountId, decimal amount, string title, string remark, DateTime createDateTime, int createUserId, DateTime? validateDateTime)
		{
            try
            {
                AuthenticatedUser.Validate(auth);
			    
                using (var context = ApiHelper.Db())
                {
                    FinanceLine obj = new FinanceLine()
                    {
                        lineId = lineId,
financeId = financeId,
accountId = accountId,
amount = amount,
title = title,
remark = remark,
createDateTime = createDateTime,
createUserId = createUserId,
validateDateTime = validateDateTime
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

		[HttpGet("Retrieve", Name = "FinanceLineRetrieve")]
		public string Retrieve(string auth, int lineId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    FinanceLine obj = context.FinanceLine.Find(lineId);

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Update", Name = "FinanceLineUpdate")]
		public string Update(string auth, int lineId, int? financeId, int? accountId, decimal? amount, string? title, string? remark, DateTime? createDateTime, int? createUserId, DateTime? validateDateTime)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

                using (var context = ApiHelper.Db())
                {
                    var obj = context.FinanceLine.Single(x => lineId == lineId);
                    obj.lineId = lineId == null ? (int)lineId : obj.lineId; // isKey: True, isIdentity: True, isComputed: False;
obj.financeId = financeId.HasValue ? (int)financeId : obj.financeId; // isKey: False, isIdentity: False, isComputed: False;
obj.accountId = accountId.HasValue ? (int)accountId : obj.accountId; // isKey: False, isIdentity: False, isComputed: False;
obj.amount = amount.HasValue ? (decimal)amount : obj.amount; // isKey: False, isIdentity: False, isComputed: False;
obj.title = title.Length > 0 ? title : obj.title; // isKey: False, isIdentity: False, isComputed: False;
obj.remark = remark.Length > 0 ? remark : obj.remark; // isKey: False, isIdentity: False, isComputed: False;
obj.createDateTime = createDateTime.HasValue ? (DateTime)createDateTime : obj.createDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.createUserId = createUserId.HasValue ? (int)createUserId : obj.createUserId; // isKey: False, isIdentity: False, isComputed: False;
obj.validateDateTime = validateDateTime.HasValue ? (DateTime)validateDateTime : obj.validateDateTime; // isKey: False, isIdentity: False, isComputed: False

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

		[HttpGet("Delete", Name = "FinanceLineDelete")]
		public string Delete(string auth, int lineId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    FinanceLine obj = context.FinanceLine.Find(lineId);
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

		[HttpGet("Overview", Name = "FinanceLineOverview")]
		public string Overview(string auth, int qtyToReturn = 10)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
				    var obj = context.FinanceLine.Take(qtyToReturn).ToList();

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