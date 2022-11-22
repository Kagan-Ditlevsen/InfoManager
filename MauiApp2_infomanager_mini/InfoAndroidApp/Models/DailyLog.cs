using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoAndroidApp.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Daily
    {
        public string dailyId { get; set; }
        public DateTime registerDateTime { get; set; }
        public int typeId { get; set; }
        public object optionId { get; set; }
        public object remark { get; set; }
        public DateTime createDateTime { get; set; }
        public int createUserId { get; set; }
        public object DailyType { get; set; }
        public object DailyTypeOption { get; set; }
        public List<object> DailyInfo { get; set; }
    }

    public class DailyInfo
    {
        public string dailyId { get; set; }
        public int extraId { get; set; }
        public object entry { get; set; }
        public DateTime createDateTime { get; set; }
        public int createUserId { get; set; }
        public object Daily { get; set; }
        public object DailyTypeExtra { get; set; }
    }

    public class DailyType
    {
        public List<object> Daily { get; set; }
        public List<object> DailyTypeExtra { get; set; }
        public List<object> DailyTypeOption { get; set; }
        public int typeId { get; set; }
        public int? parentTypeId { get; set; }
        public object nextTypeId { get; set; }
        public string internalTitle { get; set; }
        public bool isActive { get; set; }
        public bool isFavorite { get; set; }
        public int sortOrder { get; set; }
        public int? defaultOptionId { get; set; }
        public string iconCss { get; set; }
        public object description { get; set; }
    }

    public class DailyTypeExtra
    {
        public List<object> DailyInfo { get; set; }
        public object DailyType { get; set; }
        public int extraId { get; set; }
        public int typeId { get; set; }
        public string internalTitle { get; set; }
        public int sortOrder { get; set; }
        public bool isActive { get; set; }
        public bool isRequired { get; set; }
        public string inputType { get; set; }
        public object inputData { get; set; }
    }

    public class DailyTypeOption
    {
        public List<object> Daily { get; set; }
        public object DailyType { get; set; }
        public int optionId { get; set; }
        public int typeId { get; set; }
        public string internalTitle { get; set; }
        public int sortOrder { get; set; }
        public bool isActive { get; set; }
        public object description { get; set; }
    }

    public class Root
    {
        public SysUser SysUser { get; set; }
        public Daily Daily { get; set; }
        public DailyInfo DailyInfo { get; set; }
        public List<DailyType> DailyType { get; set; }
        public List<DailyTypeExtra> DailyTypeExtra { get; set; }
        public List<DailyTypeOption> DailyTypeOption { get; set; }
    }

    public class SysUser
    {
        public int userId { get; set; }
        public object firstName { get; set; }
        public object lastName { get; set; }
        public object emailAddress { get; set; }
        public object password { get; set; }
        public object statLastLogon { get; set; }
        public object statGoodMorning { get; set; }
        public object statGoodNight { get; set; }
        public object statCigQty { get; set; }
        public int isAwaken { get; set; }
        public object statSleepMs { get; set; }
        public object statAwakenMs { get; set; }
        public object modifyDateTime { get; set; }
        public DateTime createDateTime { get; set; }
    }
}
