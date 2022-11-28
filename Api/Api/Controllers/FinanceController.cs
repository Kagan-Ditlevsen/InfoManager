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
		public string Create(int financeId, DateTime receiptDateTime, string title, string shopId, string receiptId, string remark, DateTime createDateTime, int createUserId)
		{
                     
            
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

		[HttpGet("Retrieve", Name = "FinanceRetrieve")]
		public string Retrieve(int financeId)
		{
			using (var context = ApiHelper.Db())
            {
                Finance obj = context.Finance.Find(financeId);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "FinanceUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "FinanceDelete")]
		public string Delete(int financeId)
		{
			using (var context = ApiHelper.Db())
            {
                Finance obj = context.Finance.Find(financeId);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "FinanceOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.Finance.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}