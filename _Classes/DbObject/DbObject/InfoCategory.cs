using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class InfoCategory : Db
    {
        public int categoryId { get; set; }
	public int? parentCategoryId { get; set; }
	public string internalTitle { get; set; }
	public int sortOrder { get; set; }
	public DateTime createDateTime { get; set; }
	public int createUserId { get; set; }

        public InfoCategory Create(int categoryId, int? parentCategoryId, string internalTitle, int sortOrder, DateTime createDateTime, int createUserId)
        {
                string url = $"InfoCategory/Create/categoryId={categoryId}&parentCategoryId={parentCategoryId}&internalTitle={internalTitle}&sortOrder={sortOrder}&createDateTime={createDateTime}&createUserId={createUserId}";

                return JsonConvert.DeserializeObject<InfoCategory>((string)GetData(url).Result);
        }

        public InfoCategory Retrieve(int categoryId)
        {
                string url = $"InfoCategory/categoryId={categoryId}";

                return JsonConvert.DeserializeObject<InfoCategory>((string)GetData(url).Result);
        }
        
        public InfoCategory Update(int categoryId, int? parentCategoryId, string internalTitle, int sortOrder)
        {
                string url = $"InfoCategory/Update/?categoryId={categoryId}&parentCategoryId={parentCategoryId}&internalTitle={internalTitle}&sortOrder={sortOrder}";

                return JsonConvert.DeserializeObject<InfoCategory>((string)GetData(url).Result);
        }

        public InfoCategory Delete(int categoryId)
        {
                string url = $"InfoCategory/Delete/categoryId={categoryId}";

                return JsonConvert.DeserializeObject<InfoCategory>((string)GetData(url).Result);
        }

        public InfoCategory Overview(int maxResult = 500)
        {
                string url = $"InfoCategory/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<InfoCategory>((string)GetData(url).Result);
        }
    }
}