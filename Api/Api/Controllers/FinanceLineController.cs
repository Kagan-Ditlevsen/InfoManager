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
		public string Update()
		{
return "";
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