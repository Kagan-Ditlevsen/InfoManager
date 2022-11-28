using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class Finance : Db
    {
        public int financeId { get; set; }
	public DateTime receiptDateTime { get; set; }
	public string title { get; set; }
	public string shopId { get; set; }
	public string receiptId { get; set; }
	public string remark { get; set; }
	public DateTime createDateTime { get; set; }
	public int createUserId { get; set; }

        public Finance Create(int financeId, DateTime receiptDateTime, string title, string shopId, string receiptId, string remark, DateTime createDateTime, int createUserId)
        {
                string url = $"Finance/Create/financeId={financeId}&receiptDateTime={receiptDateTime}&title={title}&shopId={shopId}&receiptId={receiptId}&remark={remark}&createDateTime={createDateTime}&createUserId={createUserId}";

                return JsonConvert.DeserializeObject<Finance>((string)GetData(url).Result);
        }

        public Finance Retrieve(int financeId)
        {
                string url = $"Finance/financeId={financeId}";

                return JsonConvert.DeserializeObject<Finance>((string)GetData(url).Result);
        }
        
        public Finance Update(int financeId, DateTime receiptDateTime, string title, string shopId, string receiptId, string remark)
        {
                string url = $"Finance/Update/?financeId={financeId}&receiptDateTime={receiptDateTime}&title={title}&shopId={shopId}&receiptId={receiptId}&remark={remark}";

                return JsonConvert.DeserializeObject<Finance>((string)GetData(url).Result);
        }

        public Finance Delete(int financeId)
        {
                string url = $"Finance/Delete/financeId={financeId}";

                return JsonConvert.DeserializeObject<Finance>((string)GetData(url).Result);
        }

        public Finance Overview(int maxResult = 500)
        {
                string url = $"Finance/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<Finance>((string)GetData(url).Result);
        }
    }
}