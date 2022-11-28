using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class WorkTaskTemplate : Db
    {
        public string templateId { get; set; }
	public int sortOrder { get; set; }
	public int typeId { get; set; }
	public DateTime? startDateTime { get; set; }
	public DateTime? endDateTime { get; set; }
	public string addressText { get; set; }
	public string vehicleNumberplate { get; set; }
	public string linkNumberplate { get; set; }
	public string dollyNumberplate { get; set; }
	public string trailerNumberplate { get; set; }
	public string remark { get; set; }
	public string systemRemark { get; set; }
	public byte? reportColumnNumber { get; set; }
	public DateTime createDateTime { get; set; }
	public int createUserId { get; set; }

        public WorkTaskTemplate Create(string templateId, int sortOrder, int typeId, DateTime? startDateTime, DateTime? endDateTime, string addressText, string vehicleNumberplate, string linkNumberplate, string dollyNumberplate, string trailerNumberplate, string remark, string systemRemark, byte? reportColumnNumber, DateTime createDateTime, int createUserId)
        {
                string url = $"WorkTaskTemplate/Create/templateId={templateId}&sortOrder={sortOrder}&typeId={typeId}&startDateTime={startDateTime}&endDateTime={endDateTime}&addressText={addressText}&vehicleNumberplate={vehicleNumberplate}&linkNumberplate={linkNumberplate}&dollyNumberplate={dollyNumberplate}&trailerNumberplate={trailerNumberplate}&remark={remark}&systemRemark={systemRemark}&reportColumnNumber={reportColumnNumber}&createDateTime={createDateTime}&createUserId={createUserId}";

                return JsonConvert.DeserializeObject<WorkTaskTemplate>((string)GetData(url).Result);
        }

        public WorkTaskTemplate Retrieve(string templateId, int sortOrder)
        {
                string url = $"WorkTaskTemplate/templateId={templateId}&sortOrder={sortOrder}";

                return JsonConvert.DeserializeObject<WorkTaskTemplate>((string)GetData(url).Result);
        }
        
        public WorkTaskTemplate Update(string templateId, int sortOrder, int typeId, DateTime? startDateTime, DateTime? endDateTime, string addressText, string vehicleNumberplate, string linkNumberplate, string dollyNumberplate, string trailerNumberplate, string remark, string systemRemark, byte? reportColumnNumber)
        {
                string url = $"WorkTaskTemplate/Update/?templateId={templateId}&sortOrder={sortOrder}&typeId={typeId}&startDateTime={startDateTime}&endDateTime={endDateTime}&addressText={addressText}&vehicleNumberplate={vehicleNumberplate}&linkNumberplate={linkNumberplate}&dollyNumberplate={dollyNumberplate}&trailerNumberplate={trailerNumberplate}&remark={remark}&systemRemark={systemRemark}&reportColumnNumber={reportColumnNumber}";

                return JsonConvert.DeserializeObject<WorkTaskTemplate>((string)GetData(url).Result);
        }

        public WorkTaskTemplate Delete(string templateId, int sortOrder)
        {
                string url = $"WorkTaskTemplate/Delete/templateId={templateId}&sortOrder={sortOrder}";

                return JsonConvert.DeserializeObject<WorkTaskTemplate>((string)GetData(url).Result);
        }

        public WorkTaskTemplate Overview(int maxResult = 500)
        {
                string url = $"WorkTaskTemplate/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<WorkTaskTemplate>((string)GetData(url).Result);
        }
    }
}