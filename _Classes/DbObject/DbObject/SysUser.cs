using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace infomanager.DbObject
{    
    public partial class SysUser : Db
    {
        public int userId { get; set; }
	public string firstName { get; set; }
	public string lastName { get; set; }
	public string emailAddress { get; set; }
	public string password { get; set; }
	public DateTime? statLastLogon { get; set; }
	public DateTime? statGoodMorning { get; set; }
	public DateTime? statGoodNight { get; set; }
	public int? statCigQty { get; set; }
	public int isAwaken { get; set; }
	public int? statSleepMs { get; set; }
	public int? statAwakenMs { get; set; }
	public DateTime? modifyDateTime { get; set; }
	public DateTime createDateTime { get; set; }

        public SysUser Create(int userId, string firstName, string lastName, string emailAddress, string password, DateTime? statLastLogon, DateTime? statGoodMorning, DateTime? statGoodNight, int? statCigQty, int isAwaken, int? statSleepMs, int? statAwakenMs, DateTime? modifyDateTime, DateTime createDateTime)
        {
                string url = $"SysUser/Create/userId={userId}&firstName={firstName}&lastName={lastName}&emailAddress={emailAddress}&password={password}&statLastLogon={statLastLogon}&statGoodMorning={statGoodMorning}&statGoodNight={statGoodNight}&statCigQty={statCigQty}&isAwaken={isAwaken}&statSleepMs={statSleepMs}&statAwakenMs={statAwakenMs}&modifyDateTime={modifyDateTime}&createDateTime={createDateTime}";

                return JsonConvert.DeserializeObject<SysUser>((string)GetData(url).Result);
        }

        public SysUser Retrieve(int userId)
        {
                string url = $"SysUser/userId={userId}";

                return JsonConvert.DeserializeObject<SysUser>((string)GetData(url).Result);
        }
        
        public SysUser Update(int userId, string firstName, string lastName, string emailAddress, string password, DateTime? statLastLogon, DateTime? statGoodMorning, DateTime? statGoodNight, int? statCigQty, int isAwaken, int? statSleepMs, int? statAwakenMs, DateTime? modifyDateTime)
        {
                string url = $"SysUser/Update/?userId={userId}&firstName={firstName}&lastName={lastName}&emailAddress={emailAddress}&password={password}&statLastLogon={statLastLogon}&statGoodMorning={statGoodMorning}&statGoodNight={statGoodNight}&statCigQty={statCigQty}&isAwaken={isAwaken}&statSleepMs={statSleepMs}&statAwakenMs={statAwakenMs}&modifyDateTime={modifyDateTime}";

                return JsonConvert.DeserializeObject<SysUser>((string)GetData(url).Result);
        }

        public SysUser Delete(int userId)
        {
                string url = $"SysUser/Delete/userId={userId}";

                return JsonConvert.DeserializeObject<SysUser>((string)GetData(url).Result);
        }

        public SysUser Overview(int maxResult = 500)
        {
                string url = $"SysUser/Overview/?maxResult={maxResult}";

                return JsonConvert.DeserializeObject<SysUser>((string)GetData(url).Result);
        }
    }
}