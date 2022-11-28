using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class Work : Db
    {
        public Guid workId { get; set; }
	public DateTime? startDateTime { get; set; }
	public DateTime? endDateTime { get; set; }
	public string remark { get; set; }
	public string systemRemark { get; set; }
	public int? status { get; set; }
	public DateTime createDateTime { get; set; }
	public int createUserId { get; set; }
	public int? deleteUserId { get; set; }
	public DateTime? deleteDateTime { get; set; }
	public DateTime? finishedDateTime { get; set; }
	public int? finishedUserId { get; set; }
	public DateTime? archiveDateTime { get; set; }
	public int? archiveUserId { get; set; }

        public Work Create(Guid workId, DateTime? startDateTime, DateTime? endDateTime, string remark, string systemRemark, int? status, DateTime createDateTime, int createUserId, int? deleteUserId, DateTime? deleteDateTime, DateTime? finishedDateTime, int? finishedUserId, DateTime? archiveDateTime, int? archiveUserId)
        {
                string url = $"Work/Create/workId={workId}&startDateTime={startDateTime}&endDateTime={endDateTime}&remark={remark}&systemRemark={systemRemark}&status={status}&createDateTime={createDateTime}&createUserId={createUserId}&deleteUserId={deleteUserId}&deleteDateTime={deleteDateTime}&finishedDateTime={finishedDateTime}&finishedUserId={finishedUserId}&archiveDateTime={archiveDateTime}&archiveUserId={archiveUserId}";

                return JsonConvert.DeserializeObject<Work>((string)GetData(url).Result);
        }

        public Work Retrieve(Guid workId)
        {
                string url = $"Work/workId={workId}";

                return JsonConvert.DeserializeObject<Work>((string)GetData(url).Result);
        }
        
        public Work Update(Guid workId, DateTime? startDateTime, DateTime? endDateTime, string remark, string systemRemark, int? status, int? deleteUserId, DateTime? deleteDateTime, DateTime? finishedDateTime, int? finishedUserId, DateTime? archiveDateTime, int? archiveUserId)
        {
                string url = $"Work/Update/?workId={workId}&startDateTime={startDateTime}&endDateTime={endDateTime}&remark={remark}&systemRemark={systemRemark}&status={status}&deleteUserId={deleteUserId}&deleteDateTime={deleteDateTime}&finishedDateTime={finishedDateTime}&finishedUserId={finishedUserId}&archiveDateTime={archiveDateTime}&archiveUserId={archiveUserId}";

                return JsonConvert.DeserializeObject<Work>((string)GetData(url).Result);
        }

        public Work Delete(Guid workId)
        {
                string url = $"Work/Delete/workId={workId}";

                return JsonConvert.DeserializeObject<Work>((string)GetData(url).Result);
        }

        public Work Overview(int maxResult = 500)
        {
                string url = $"Work/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<Work>((string)GetData(url).Result);
        }
    }
}