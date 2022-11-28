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
	public partial class InfoTagController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "InfoTagCreate")]
		public string Create(int tagId, string title)
		{
                     
            
            using (var context = ApiHelper.Db())
            {
                InfoTag obj = new InfoTag()
                {
                    tagId = tagId,
title = title
                };
                context.Entry(obj).State = System.Data.Entity.EntityState.Added;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Retrieve", Name = "InfoTagRetrieve")]
		public string Retrieve(int tagId)
		{
			using (var context = ApiHelper.Db())
            {
                InfoTag obj = context.InfoTag.Find(tagId);

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Update", Name = "InfoTagUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "InfoTagDelete")]
		public string Delete(int tagId)
		{
			using (var context = ApiHelper.Db())
            {
                InfoTag obj = context.InfoTag.Find(tagId);
				context.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                int qtyChanges = context.SaveChanges();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}

		[HttpGet("Overview", Name = "InfoTagOverview")]
		public string Overview(int qtyToReturn = 10)
		{
			using (var context = ApiHelper.Db())
            {
				var obj = context.InfoTag.Take(qtyToReturn).ToList();

				return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
            }
		}
		#endregion
	}
}