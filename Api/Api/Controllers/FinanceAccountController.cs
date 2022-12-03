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
		public string Create(string auth, int accountId, int? parentAccountId, string typeId, string title, decimal sumInitial, decimal? sumCurrent, string sortOrder, byte? lvl, string lvlTitle, string breadcrum)
		{
            try
            {
                AuthenticatedUser.Validate(auth);
			    
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
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Retrieve", Name = "FinanceAccountRetrieve")]
		public string Retrieve(string auth, int accountId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    FinanceAccount obj = context.FinanceAccount.Find(accountId);

				    return JsonConvert.SerializeObject(obj, Formatting.None, ApiHelper.serializerSettings);
                }
            }
            catch (Exception ex)
            {
                return ApiHelper.ApiException(ex.ToString(), ex.Message);
            }
		}

		[HttpGet("Update", Name = "FinanceAccountUpdate")]
		public string Update(string auth, int accountId, int? parentAccountId, string? typeId, string? title, decimal? sumInitial, decimal? sumCurrent, string? sortOrder, byte? lvl, string? lvlTitle, string? breadcrum)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

                using (var context = ApiHelper.Db())
                {
                    var obj = context.FinanceAccount.Single(x => accountId == accountId);
                    obj.accountId = accountId == null ? (int)accountId : obj.accountId; // isKey: True, isIdentity: False, isComputed: False;
obj.parentAccountId = parentAccountId.HasValue ? (int)parentAccountId : obj.parentAccountId; // isKey: False, isIdentity: False, isComputed: False;
obj.typeId = typeId.Length > 0 ? typeId : obj.typeId; // isKey: False, isIdentity: False, isComputed: False;
obj.title = title.Length > 0 ? title : obj.title; // isKey: False, isIdentity: False, isComputed: False;
obj.sumInitial = sumInitial.HasValue ? (decimal)sumInitial : obj.sumInitial; // isKey: False, isIdentity: False, isComputed: False;
obj.sumCurrent = sumCurrent.HasValue ? (decimal)sumCurrent : obj.sumCurrent; // isKey: False, isIdentity: False, isComputed: True;
obj.sortOrder = sortOrder.Length > 0 ? sortOrder : obj.sortOrder; // isKey: False, isIdentity: False, isComputed: False;
obj.lvl = lvl.HasValue ? (byte)lvl : obj.lvl; // isKey: False, isIdentity: False, isComputed: False;
obj.lvlTitle = lvlTitle.Length > 0 ? lvlTitle : obj.lvlTitle; // isKey: False, isIdentity: False, isComputed: True;
obj.breadcrum = breadcrum.Length > 0 ? breadcrum : obj.breadcrum; // isKey: False, isIdentity: False, isComputed: True

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

		[HttpGet("Delete", Name = "FinanceAccountDelete")]
		public string Delete(string auth, int accountId)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
                    FinanceAccount obj = context.FinanceAccount.Find(accountId);
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

		[HttpGet("Overview", Name = "FinanceAccountOverview")]
		public string Overview(string auth, int qtyToReturn = 10)
		{
            try
            {
                AuthenticatedUser.Validate(auth);

			    using (var context = ApiHelper.Db())
                {
				    var obj = context.FinanceAccount.Take(qtyToReturn).ToList();

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