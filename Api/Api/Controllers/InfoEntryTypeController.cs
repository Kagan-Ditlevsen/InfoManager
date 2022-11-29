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
	public partial class InfoEntryTypeController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "InfoEntryTypeCreate")]
		public string Create(string auth, int typeId, string internalTitle, DateTime createDateTime, int? createUserId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);
			    
                using (var context = ApiHelper.Db())
                {
                    InfoEntryType obj = new InfoEntryType()
                    {
                        typeId = typeId,
internalTitle = internalTitle,
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

		[HttpGet("Retrieve", Name = "InfoEntryTypeRetrieve")]
		public string Retrieve(string auth, int typeId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    InfoEntryType obj = context.InfoEntryType.Find(typeId);

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Update", Name = "InfoEntryTypeUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "InfoEntryTypeDelete")]
		public string Delete(string auth, int typeId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    InfoEntryType obj = context.InfoEntryType.Find(typeId);
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

		[HttpGet("Overview", Name = "InfoEntryTypeOverview")]
		public string Overview(string auth, int qtyToReturn = 10)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
				    var obj = context.InfoEntryType.Take(qtyToReturn).ToList();

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