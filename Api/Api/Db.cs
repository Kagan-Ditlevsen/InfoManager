using api.infomanager.dk.DAL;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Data;

namespace api.infomanager.dk
{
    public partial class Db
    {
        public Db() {
            //Db.ToClass<List<DailyType>>();
        }

        private static string sqlConn = "data source=mssql.haulmanager.nu;initial catalog=haulmanager_nu_db_im;persist security info=True;user id=haulmanager_nu;password=als76Asl;multipleactiveresultsets=True;application name=infomanager.dk";
        public static T Overview<T>()
        {
            string query = "SELECT * FROM " + typeof(T).Name + " FOR JSON PATH";
            var response = default(T);

            SqlConnection conn = new SqlConnection(sqlConn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            reader.Read();
            string json = reader.GetString(0);

            //DataTable dt = new DataTable();
            //dt.Load(cmd.ExecuteReader());
            conn.Close();

            return JsonConvert.DeserializeObject<T>(json);
            //return dt.DataTableToList<DailyType>();
            //            return response;

            //return JsonConvert.SerializeObject(dt.DataTableToList<DailyType>());

            //if (!string.IsNullOrEmpty(data))
            //    response = jsonSettings == null
            //        ? JsonConvert.DeserializeObject<T>(data)
            //        : JsonConvert.DeserializeObject<T>(data, jsonSettings);

            //return response;
        }
        public List<Daily> Daily => new List<Daily>();
        public List<DailyInfo> DailyInfo => new List<DailyInfo>();
        public List<DailyType> DailyType => new List<DailyType>();
        public List<DailyTypeExtra> DailyTypeExtra => new List<DailyTypeExtra>();
        public List<DailyTypeOption> DailyTypeOption => new List<DailyTypeOption>();
        public List<SysUser> SysUser => new List<SysUser>();
        public List<SysUserTimesheet> SysUserTimesheet => new List<SysUserTimesheet>();
    }
}
namespace api.infomanager.dk.DAL
{

    public partial class Daily
    {
        [JsonProperty("dailyId")]
        public Guid DailyId { get; set; }
        [JsonProperty("registerDateTime")]
        public DateTime RegisterDateTime { get; set; }
        [JsonProperty("typeId")]
        public int TypeId { get; set; }
        [JsonProperty("optionId")]
        public int? OptionId { get; set; }
        [JsonProperty("remark")]
        public string? Remark { get; set; }
        [JsonProperty("createDateTime")]
        public DateTime CreateDateTime { get; set; }
        [JsonProperty("createUserId")]
        public int CreateUserId { get; set; }
        public DailyType? TypeId_Reference { get; set; }
        public DailyTypeOption? OptionId_Reference { get; set; }
        public List<DailyInfo>? DailyInfos { get; set; }

        public Daily()
        {
            this.DailyInfos = new List<DailyInfo>();
        }
    }


    public partial class DailyInfo
    {
        [JsonProperty("dailyId")]
        public Guid DailyId { get; set; }
        [JsonProperty("extraId")]
        public int ExtraId { get; set; }
        [JsonProperty("entry")]
        public string? Entry { get; set; }
        [JsonProperty("createDateTime")]
        public DateTime CreateDateTime { get; set; }
        [JsonProperty("createUserId")]
        public int CreateUserId { get; set; }
        public Daily? DailyId_Reference { get; set; }
        public DailyTypeExtra? ExtraId_Reference { get; set; }
    }


    public partial class DailyType
    {
        [JsonProperty("typeId")]
        public int TypeId { get; set; }
        [JsonProperty("parentTypeId")]
        public int? ParentTypeId { get; set; }
        [JsonProperty("nextTypeId")]
        public int? NextTypeId { get; set; }
        [JsonProperty("internalTitle")]
        public string? InternalTitle { get; set; }
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
        [JsonProperty("isFavorite")]
        public bool IsFavorite { get; set; }
        [JsonProperty("sortOrder")]
        public int SortOrder { get; set; }
        [JsonProperty("defaultOptionId")]
        public int? DefaultOptionId { get; set; }
        [JsonProperty("iconCss")]
        public string? IconCss { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
        public List<DailyTypeExtra>? DailyTypeExtras { get; set; }
        public List<Daily>? Dailys { get; set; }
        public List<DailyTypeOption>? DailyTypeOptions { get; set; }

        public DailyType()
        {
            this.DailyTypeExtras = new List<DailyTypeExtra>();
            this.Dailys = new List<Daily>();
            this.DailyTypeOptions = new List<DailyTypeOption>();
        }
    }


    public partial class DailyTypeExtra
    {
        [JsonProperty("extraId")]
        public int ExtraId { get; set; }
        [JsonProperty("typeId")]
        public int TypeId { get; set; }
        [JsonProperty("internalTitle")]
        public string? InternalTitle { get; set; }
        [JsonProperty("sortOrder")]
        public int SortOrder { get; set; }
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
        [JsonProperty("isRequired")]
        public bool IsRequired { get; set; }
        [JsonProperty("inputType")]
        public string? InputType { get; set; }
        [JsonProperty("inputData")]
        public string? InputData { get; set; }
        public DailyType? TypeId_Reference { get; set; }
        public List<DailyInfo>? DailyInfos { get; set; }

        public DailyTypeExtra()
        {
            this.DailyInfos = new List<DailyInfo>();
        }
    }


    public partial class DailyTypeOption
    {
        [JsonProperty("optionId")]
        public int OptionId { get; set; }
        [JsonProperty("typeId")]
        public int TypeId { get; set; }
        [JsonProperty("internalTitle")]
        public string? InternalTitle { get; set; }
        [JsonProperty("sortOrder")]
        public int SortOrder { get; set; }
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
        [JsonProperty("iconCss")]
        public string? IconCss { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
        public DailyType? TypeId_Reference { get; set; }
        public List<Daily>? Dailys { get; set; }

        public DailyTypeOption()
        {
            this.Dailys = new List<Daily>();
        }
    }


    public partial class SysUser
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("firstName")]
        public string? FirstName { get; set; }
        [JsonProperty("lastName")]
        public string? LastName { get; set; }
        [JsonProperty("emailAddress")]
        public string? EmailAddress { get; set; }
        [JsonProperty("password")]
        public string? Password { get; set; }
        [JsonProperty("statLastLogon")]
        public DateTime? StatLastLogon { get; set; }
        [JsonProperty("statGoodMorning")]
        public DateTime? StatGoodMorning { get; set; }
        [JsonProperty("statGoodNight")]
        public DateTime? StatGoodNight { get; set; }
        [JsonProperty("statCigQty")]
        public int? StatCigQty { get; set; }
        [JsonProperty("isAwaken")]
        public int IsAwaken { get; set; }
        [JsonProperty("statSleepMs")]
        public int? StatSleepMs { get; set; }
        [JsonProperty("statAwakenMs")]
        public int? StatAwakenMs { get; set; }
        [JsonProperty("modifyDateTime")]
        public DateTime? ModifyDateTime { get; set; }
        [JsonProperty("createDateTime")]
        public DateTime CreateDateTime { get; set; }
        public List<SysUserTimesheet>? SysUserTimesheets { get; set; }

        public SysUser()
        {
            this.SysUserTimesheets = new List<SysUserTimesheet>();
        }
    }


    public partial class SysUserTimesheet
    {
        [JsonProperty("timesheetId")]
        public Guid TimesheetId { get; set; }
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("startDateTime")]
        public DateTime? StartDateTime { get; set; }
        [JsonProperty("endDateTime")]
        public DateTime? EndDateTime { get; set; }
        [JsonProperty("remark")]
        public string? Remark { get; set; }
        [JsonProperty("createDateTime")]
        public DateTime CreateDateTime { get; set; }
        [JsonProperty("createUserId")]
        public int CreateUserId { get; set; }
        public SysUser? UserId_Reference { get; set; }
    }

}