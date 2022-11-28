using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class SysUserTimesheet : Db
    {
        public Guid timesheetId { get; set; }
	public int userId { get; set; }
	public DateTime? startDateTime { get; set; }
	public DateTime? endDateTime { get; set; }
	public string remark { get; set; }
	public DateTime createDateTime { get; set; }
	public int createUserId { get; set; }

        public SysUserTimesheet Create(Guid timesheetId, int userId, DateTime? startDateTime, DateTime? endDateTime, string remark, DateTime createDateTime, int createUserId)
        {
                string url = $"SysUserTimesheet/Create/timesheetId={timesheetId}&userId={userId}&startDateTime={startDateTime}&endDateTime={endDateTime}&remark={remark}&createDateTime={createDateTime}&createUserId={createUserId}";

                return JsonConvert.DeserializeObject<SysUserTimesheet>((string)GetData(url).Result);
        }

        public SysUserTimesheet Retrieve(Guid timesheetId)
        {
                string url = $"SysUserTimesheet/timesheetId={timesheetId}";

                return JsonConvert.DeserializeObject<SysUserTimesheet>((string)GetData(url).Result);
        }
        
        public SysUserTimesheet Update(Guid timesheetId, int userId, DateTime? startDateTime, DateTime? endDateTime, string remark)
        {
                string url = $"SysUserTimesheet/Update/?timesheetId={timesheetId}&userId={userId}&startDateTime={startDateTime}&endDateTime={endDateTime}&remark={remark}";

                return JsonConvert.DeserializeObject<SysUserTimesheet>((string)GetData(url).Result);
        }

        public SysUserTimesheet Delete(Guid timesheetId)
        {
                string url = $"SysUserTimesheet/Delete/timesheetId={timesheetId}";

                return JsonConvert.DeserializeObject<SysUserTimesheet>((string)GetData(url).Result);
        }

        public SysUserTimesheet Overview(int maxResult = 500)
        {
                string url = $"SysUserTimesheet/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<SysUserTimesheet>((string)GetData(url).Result);
        }
    }
}