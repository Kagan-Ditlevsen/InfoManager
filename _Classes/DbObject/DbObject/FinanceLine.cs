using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class FinanceLine : Db
    {
        public int lineId { get; set; }
	public int financeId { get; set; }
	public int? accountId { get; set; }
	public decimal amount { get; set; }
	public string title { get; set; }
	public string remark { get; set; }
	public DateTime createDateTime { get; set; }
	public int createUserId { get; set; }
	public DateTime? validateDateTime { get; set; }

        public FinanceLine Create(int lineId, int financeId, int? accountId, decimal amount, string title, string remark, DateTime createDateTime, int createUserId, DateTime? validateDateTime)
        {
                string url = $"FinanceLine/Create/lineId={lineId}&financeId={financeId}&accountId={accountId}&amount={amount}&title={title}&remark={remark}&createDateTime={createDateTime}&createUserId={createUserId}&validateDateTime={validateDateTime}";

                return JsonConvert.DeserializeObject<FinanceLine>((string)GetData(url).Result);
        }

        public FinanceLine Retrieve(int lineId)
        {
                string url = $"FinanceLine/lineId={lineId}";

                return JsonConvert.DeserializeObject<FinanceLine>((string)GetData(url).Result);
        }
        
        public FinanceLine Update(int lineId, int financeId, int? accountId, decimal amount, string title, string remark, DateTime? validateDateTime)
        {
                string url = $"FinanceLine/Update/?lineId={lineId}&financeId={financeId}&accountId={accountId}&amount={amount}&title={title}&remark={remark}&validateDateTime={validateDateTime}";

                return JsonConvert.DeserializeObject<FinanceLine>((string)GetData(url).Result);
        }

        public FinanceLine Delete(int lineId)
        {
                string url = $"FinanceLine/Delete/lineId={lineId}";

                return JsonConvert.DeserializeObject<FinanceLine>((string)GetData(url).Result);
        }

        public FinanceLine Overview(int maxResult = 500)
        {
                string url = $"FinanceLine/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<FinanceLine>((string)GetData(url).Result);
        }
    }
}