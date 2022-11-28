using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class WorkAddress : Db
    {
        public int addressId { get; set; }
	public string title { get; set; }
	public string gpsAddress { get; set; }
	public string gpsPlusCode { get; set; }
	public string systemRemark { get; set; }
	public string locationCode { get; set; }
	public bool isArchived { get; set; }

        public WorkAddress Create(int addressId, string title, string gpsAddress, string gpsPlusCode, string systemRemark, string locationCode, bool isArchived)
        {
                string url = $"WorkAddress/Create/addressId={addressId}&title={title}&gpsAddress={gpsAddress}&gpsPlusCode={gpsPlusCode}&systemRemark={systemRemark}&locationCode={locationCode}&isArchived={isArchived}";

                return JsonConvert.DeserializeObject<WorkAddress>((string)GetData(url).Result);
        }

        public WorkAddress Retrieve(int addressId)
        {
                string url = $"WorkAddress/addressId={addressId}";

                return JsonConvert.DeserializeObject<WorkAddress>((string)GetData(url).Result);
        }
        
        public WorkAddress Update(int addressId, string title, string gpsAddress, string gpsPlusCode, string systemRemark, string locationCode, bool isArchived)
        {
                string url = $"WorkAddress/Update/?addressId={addressId}&title={title}&gpsAddress={gpsAddress}&gpsPlusCode={gpsPlusCode}&systemRemark={systemRemark}&locationCode={locationCode}&isArchived={isArchived}";

                return JsonConvert.DeserializeObject<WorkAddress>((string)GetData(url).Result);
        }

        public WorkAddress Delete(int addressId)
        {
                string url = $"WorkAddress/Delete/addressId={addressId}";

                return JsonConvert.DeserializeObject<WorkAddress>((string)GetData(url).Result);
        }

        public WorkAddress Overview(int maxResult = 500)
        {
                string url = $"WorkAddress/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<WorkAddress>((string)GetData(url).Result);
        }
    }
}