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
		public string Create(int categoryId, int? parentCategoryId, string internalTitle, int sortOrder, DateTime createDateTime, int createUserId)
		{
                     
            
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

		[HttpGet("Retrieve", Name = "InfoCategoryRetrieve")]
		public string Retrieve(int categoryId)
		{
			using (var context = ApiHelper.Db())
            {
                InfoCategory obj = context.InfoCategory.Find(categoryId);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "InfoCategoryUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "InfoCategoryDelete")]
		public string Delete(int categoryId)
		{
			using (var context = ApiHelper.Db())
            {
                InfoCategory obj = context.InfoCategory.Find(categoryId);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "InfoCategoryOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.InfoCategory.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}