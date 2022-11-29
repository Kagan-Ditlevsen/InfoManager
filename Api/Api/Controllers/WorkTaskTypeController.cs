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
	public partial class WorkTaskTypeController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "WorkTaskTypeCreate")]
		public string Create(string auth, int typeId, string actionType, string internalTitle, string btnStartText, string btnEndText, string icon, int? requiredFields)
		{
            try
            {
                AuthenticatedUser.Validate(auth);
			    
                using (var context = ApiHelper.Db())
                {
                    WorkTaskType obj = new WorkTaskType()
                    {
                        typeId = typeId,
actionType = actionType,
internalTitle = internalTitle,
btnStartText = btnStartText,
btnEndText = btnEndText,
icon = icon,
requiredFields = requiredFields
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

		[HttpGet("Retrieve", Name = "WorkTaskTypeRetrieve")]
		public string Retrieve(string auth, int typeId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    WorkTaskType obj = context.WorkTaskType.Find(typeId);

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Update", Name = "WorkTaskTypeUpdate")]
		public string Update()
		{
return "";
		}

		[HttpGet("Delete", Name = "WorkTaskTypeDelete")]
		public string Delete(string auth, int typeId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    WorkTaskType obj = context.WorkTaskType.Find(typeId);
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

		[HttpGet("Overview", Name = "WorkTaskTypeOverview")]
		public string Overview(string auth, int qtyToReturn = 10)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
				    var obj = context.WorkTaskType.Take(qtyToReturn).ToList();

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