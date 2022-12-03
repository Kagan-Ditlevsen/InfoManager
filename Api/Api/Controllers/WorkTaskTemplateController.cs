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
	public partial class WorkTaskTemplateController : CommonController
	{
		#region Done
		[HttpGet("Create", Name = "WorkTaskTemplateCreate")]
		public string Create(string auth, string templateId, int sortOrder, int typeId, DateTime? startDateTime, DateTime? endDateTime, string addressText, string vehicleNumberplate, string linkNumberplate, string dollyNumberplate, string trailerNumberplate, string remark, string systemRemark, byte? reportColumnNumber, DateTime createDateTime, int createUserId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);
			    
                using (var context = ApiHelper.Db())
                {
                    WorkTaskTemplate obj = new WorkTaskTemplate()
                    {
                        templateId = templateId,
sortOrder = sortOrder,
typeId = typeId,
startDateTime = startDateTime,
endDateTime = endDateTime,
addressText = addressText,
vehicleNumberplate = vehicleNumberplate,
linkNumberplate = linkNumberplate,
dollyNumberplate = dollyNumberplate,
trailerNumberplate = trailerNumberplate,
remark = remark,
systemRemark = systemRemark,
reportColumnNumber = reportColumnNumber,
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

		[HttpGet("Retrieve", Name = "WorkTaskTemplateRetrieve")]
		public string Retrieve(string auth, string templateId, int sortOrder)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    WorkTaskTemplate obj = context.WorkTaskTemplate.Find(templateId, sortOrder);

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Update", Name = "WorkTaskTemplateUpdate")]
		public string Update(string auth, string templateId, int sortOrder, int? typeId, DateTime? startDateTime, DateTime? endDateTime, string? addressText, string? vehicleNumberplate, string? linkNumberplate, string? dollyNumberplate, string? trailerNumberplate, string? remark, string? systemRemark, byte? reportColumnNumber, DateTime? createDateTime, int? createUserId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

                using (var context = ApiHelper.Db())
                {
                    var obj = context.WorkTaskTemplate.Single(x => templateId == templateId && sortOrder == sortOrder);
                    obj.templateId = templateId.Length > 0 ? templateId : obj.templateId; // isKey: True, isIdentity: False, isComputed: False;
obj.sortOrder = sortOrder == null ? (int)sortOrder : obj.sortOrder; // isKey: True, isIdentity: False, isComputed: False;
obj.typeId = typeId.HasValue ? (int)typeId : obj.typeId; // isKey: False, isIdentity: False, isComputed: False;
obj.startDateTime = startDateTime.HasValue ? (DateTime)startDateTime : obj.startDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.endDateTime = endDateTime.HasValue ? (DateTime)endDateTime : obj.endDateTime; // isKey: False, isIdentity: False, isComputed: False;
obj.addressText = addressText.Length > 0 ? addressText : obj.addressText; // isKey: False, isIdentity: False, isComputed: False;
obj.vehicleNumberplate = vehicleNumberplate.Length > 0 ? vehicleNumberplate : obj.vehicleNumberplate; // isKey: False, isIdentity: False, isComputed: False;
obj.linkNumberplate = linkNumberplate.Length > 0 ? linkNumberplate : obj.linkNumberplate; // isKey: False, isIdentity: False, isComputed: False;
obj.dollyNumberplate = dollyNumberplate.Length > 0 ? dollyNumberplate : obj.dollyNumberplate; // isKey: False, isIdentity: False, isComputed: False;
obj.trailerNumberplate = trailerNumberplate.Length > 0 ? trailerNumberplate : obj.trailerNumberplate; // isKey: False, isIdentity: False, isComputed: False;
obj.remark = remark.Length > 0 ? remark : obj.remark; // isKey: False, isIdentity: False, isComputed: False;
obj.systemRemark = systemRemark.Length > 0 ? systemRemark : obj.systemRemark; // isKey: False, isIdentity: False, isComputed: False;
obj.reportColumnNumber = reportColumnNumber.HasValue ? (byte)reportColumnNumber : obj.reportColumnNumber; // isKey: False, isIdentity: False, isComputed: False;
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

		[HttpGet("Delete", Name = "WorkTaskTemplateDelete")]
		public string Delete(string auth, string templateId, int sortOrder)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    WorkTaskTemplate obj = context.WorkTaskTemplate.Find(templateId, sortOrder);
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

		[HttpGet("Overview", Name = "WorkTaskTemplateOverview")]
		public string Overview(string auth, int qtyToReturn = 10)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
				    var obj = context.WorkTaskTemplate.Take(qtyToReturn).ToList();

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