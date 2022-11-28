using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class DailyInfo : Db
    {
        public Guid dailyId { get; set; }
	public int extraId { get; set; }
	public string entry { get; set; }
	public DateTime createDateTime { get; set; }
	public int createUserId { get; set; }

        public DailyInfo Create(Guid dailyId, int extraId, string entry, DateTime createDateTime, int createUserId)
        {
                string url = $"DailyInfo/Create/dailyId={dailyId}&extraId={extraId}&entry={entry}&createDateTime={createDateTime}&createUserId={createUserId}";

                return JsonConvert.DeserializeObject<DailyInfo>((string)GetData(url).Result);
        }

        public DailyInfo Retrieve(Guid dailyId, int extraId)
        {
                string url = $"DailyInfo/dailyId={dailyId}&extraId={extraId}";

                return JsonConvert.DeserializeObject<DailyInfo>((string)GetData(url).Result);
        }
        
        public DailyInfo Update(Guid dailyId, int extraId, string entry)
        {
                string url = $"DailyInfo/Update/?dailyId={dailyId}&extraId={extraId}&entry={entry}";

                return JsonConvert.DeserializeObject<DailyInfo>((string)GetData(url).Result);
        }

        public DailyInfo Delete(Guid dailyId, int extraId)
        {
                string url = $"DailyInfo/Delete/dailyId={dailyId}&extraId={extraId}";

                return JsonConvert.DeserializeObject<DailyInfo>((string)GetData(url).Result);
        }

        public DailyInfo Overview(int maxResult = 500)
        {
                string url = $"DailyInfo/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<DailyInfo>((string)GetData(url).Result);
        }
    }
}