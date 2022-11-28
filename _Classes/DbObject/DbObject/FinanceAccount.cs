using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class FinanceAccount : Db
    {
        public int accountId { get; set; }
	public int? parentAccountId { get; set; }
	public string typeId { get; set; }
	public string title { get; set; }
	public decimal sumInitial { get; set; }
	public decimal? sumCurrent { get; set; }
	public string _sortOrder { get; set; }
	public byte? _lvl { get; set; }
	public string _lvlTitle { get; set; }
	public string _breadcrum { get; set; }

        public FinanceAccount Create(int accountId, int? parentAccountId, string typeId, string title, decimal sumInitial, decimal? sumCurrent, string _sortOrder, byte? _lvl, string _lvlTitle, string _breadcrum)
        {
                string url = $"FinanceAccount/Create/accountId={accountId}&parentAccountId={parentAccountId}&typeId={typeId}&title={title}&sumInitial={sumInitial}&sumCurrent={sumCurrent}&_sortOrder={_sortOrder}&_lvl={_lvl}&_lvlTitle={_lvlTitle}&_breadcrum={_breadcrum}";

                return JsonConvert.DeserializeObject<FinanceAccount>((string)GetData(url).Result);
        }

        public FinanceAccount Retrieve(int accountId)
        {
                string url = $"FinanceAccount/accountId={accountId}";

                return JsonConvert.DeserializeObject<FinanceAccount>((string)GetData(url).Result);
        }
        
        public FinanceAccount Update(int accountId, int? parentAccountId, string typeId, string title, decimal sumInitial, decimal? sumCurrent, string _sortOrder, byte? _lvl, string _lvlTitle, string _breadcrum)
        {
                string url = $"FinanceAccount/Update/?accountId={accountId}&parentAccountId={parentAccountId}&typeId={typeId}&title={title}&sumInitial={sumInitial}&sumCurrent={sumCurrent}&_sortOrder={_sortOrder}&_lvl={_lvl}&_lvlTitle={_lvlTitle}&_breadcrum={_breadcrum}";

                return JsonConvert.DeserializeObject<FinanceAccount>((string)GetData(url).Result);
        }

        public FinanceAccount Delete(int accountId)
        {
                string url = $"FinanceAccount/Delete/accountId={accountId}";

                return JsonConvert.DeserializeObject<FinanceAccount>((string)GetData(url).Result);
        }

        public FinanceAccount Overview(int maxResult = 500)
        {
                string url = $"FinanceAccount/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<FinanceAccount>((string)GetData(url).Result);
        }
    }
}