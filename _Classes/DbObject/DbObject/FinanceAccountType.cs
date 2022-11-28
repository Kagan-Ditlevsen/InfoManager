using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class FinanceAccountType : Db
    {
        public string typeId { get; set; }
	public string internalTitle { get; set; }

        public FinanceAccountType Create(string typeId, string internalTitle)
        {
                string url = $"FinanceAccountType/Create/typeId={typeId}&internalTitle={internalTitle}";

                return JsonConvert.DeserializeObject<FinanceAccountType>((string)GetData(url).Result);
        }

        public FinanceAccountType Retrieve(string typeId)
        {
                string url = $"FinanceAccountType/typeId={typeId}";

                return JsonConvert.DeserializeObject<FinanceAccountType>((string)GetData(url).Result);
        }
        
        public FinanceAccountType Update(string typeId, string internalTitle)
        {
                string url = $"FinanceAccountType/Update/?typeId={typeId}&internalTitle={internalTitle}";

                return JsonConvert.DeserializeObject<FinanceAccountType>((string)GetData(url).Result);
        }

        public FinanceAccountType Delete(string typeId)
        {
                string url = $"FinanceAccountType/Delete/typeId={typeId}";

                return JsonConvert.DeserializeObject<FinanceAccountType>((string)GetData(url).Result);
        }

        public FinanceAccountType Overview(int maxResult = 500)
        {
                string url = $"FinanceAccountType/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<FinanceAccountType>((string)GetData(url).Result);
        }
    }
}