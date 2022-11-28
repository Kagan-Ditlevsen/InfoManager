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
	public partial class FinanceAccountController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "FinanceAccountCreate")]
		public string Create(int accountId, int? parentAccountId, string typeId, string title, decimal sumInitial, decimal? sumCurrent, string sortOrder, byte? lvl, string lvlTitle, string breadcrum)
		{
                     
            
            using (var context = ApiHelper.Db())
            {
                FinanceAccount obj = new FinanceAccount()
                {
                    accountId = accountId,
parentAccountId = parentAccountId,
typeId = typeId,
title = title,
sumInitial = sumInitial,
sumCurrent = sumCurrent,
sortOrder = sortOrder,
lvl = lvl,
lvlTitle = lvlTitle,
breadcrum = breadcrum
                };
                context.Entry(obj).State = System.Data.Entity.EntityState.Added;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Retrieve", Name = "FinanceAccountRetrieve")]
		public string Retrieve(int accountId)
		{
			using (var context = ApiHelper.Db())
            {
                FinanceAccount obj = context.FinanceAccount.Find(accountId);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "FinanceAccountUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "FinanceAccountDelete")]
		public string Delete(int accountId)
		{
			using (var context = ApiHelper.Db())
            {
                FinanceAccount obj = context.FinanceAccount.Find(accountId);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "FinanceAccountOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.FinanceAccount.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}