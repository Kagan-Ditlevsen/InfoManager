using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class Daily : Db
    {
        public Guid dailyId { get; set; }
	public DateTime registerDateTime { get; set; }
	public int typeId { get; set; }
	public int? optionId { get; set; }
	public string remark { get; set; }
	public DateTime createDateTime { get; set; }
	public int createUserId { get; set; }

        public Daily Create(Guid dailyId, DateTime registerDateTime, int typeId, int? optionId, string remark, DateTime createDateTime, int createUserId)
        {
                string url = $"Daily/Create/dailyId={dailyId}&registerDateTime={registerDateTime}&typeId={typeId}&optionId={optionId}&remark={remark}&createDateTime={createDateTime}&createUserId={createUserId}";

                return JsonConvert.DeserializeObject<Daily>((string)GetData(url).Result);
        }

        public Daily Retrieve(Guid dailyId)
        {
                string url = $"Daily/dailyId={dailyId}";

                return JsonConvert.DeserializeObject<Daily>((string)GetData(url).Result);
        }
        
        public Daily Update(Guid dailyId, DateTime registerDateTime, int typeId, int? optionId, string remark)
        {
                string url = $"Daily/Update/?dailyId={dailyId}&registerDateTime={registerDateTime}&typeId={typeId}&optionId={optionId}&remark={remark}";

                return JsonConvert.DeserializeObject<Daily>((string)GetData(url).Result);
        }

        public Daily Delete(Guid dailyId)
        {
                string url = $"Daily/Delete/dailyId={dailyId}";

                return JsonConvert.DeserializeObject<Daily>((string)GetData(url).Result);
        }

        public Daily Overview(int maxResult = 500)
        {
                string url = $"Daily/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<Daily>((string)GetData(url).Result);
        }
    }
}