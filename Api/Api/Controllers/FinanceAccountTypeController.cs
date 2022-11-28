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
	public partial class FinanceAccountTypeController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "FinanceAccountTypeCreate")]
		public string Create(string typeId, string internalTitle)
		{
                     
            
            using (var context = ApiHelper.Db())
            {
                FinanceAccountType obj = new FinanceAccountType()
                {
                    typeId = typeId,
internalTitle = internalTitle
                };
                context.Entry(obj).State = System.Data.Entity.EntityState.Added;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Retrieve", Name = "FinanceAccountTypeRetrieve")]
		public string Retrieve(string typeId)
		{
			using (var context = ApiHelper.Db())
            {
                FinanceAccountType obj = context.FinanceAccountType.Find(typeId);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "FinanceAccountTypeUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "FinanceAccountTypeDelete")]
		public string Delete(string typeId)
		{
			using (var context = ApiHelper.Db())
            {
                FinanceAccountType obj = context.FinanceAccountType.Find(typeId);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "FinanceAccountTypeOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.FinanceAccountType.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}