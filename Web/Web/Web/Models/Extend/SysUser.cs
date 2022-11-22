using Skd;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using dk.infomanager.Controllers;

namespace dk.infomanager.Models
{
    public class SysUserStat
    {
        public int typeId { get; set; }
        public string typeTitle { get; set; }
        public Nullable<int> optionId { get; set; }
        public string optionTitle { get; set; }
        public int qtyMorning { get; set; }
        public int qty24h { get; set; }
        public int qtyMidnight { get; set; }
        public Nullable<System.DateTime> lastTimeTypeId { get; set; }
        public Nullable<System.DateTime> lastTimeOptionId { get; set; }
        public Nullable<System.DateTime> lastRegisterDateTime { get; set; }
        public int createUserId { get; set; }
        public HtmlString StatusText()
        {
            string rtn = "";
            if (lastTimeTypeId.HasValue)
            {
                rtn += "The last one were " + lastTimeTypeId.Value.ToDayHM(DateTime.Now, false) + " ago. ";
            }
            if (qty24h > 0)
            {
                rtn += qty24h + " within the last 24 hours. ";
            }
            if (qtyMidnight > 0)
            {
                rtn += qtyMidnight + " since midnight. ";
            }
            if (qtyMorning > 0)
            {
                rtn += qtyMorning + " since morning. ";
            }
            rtn = rtn.Replace("  ", " ");

            return new HtmlString(rtn);
        }
        public HtmlString TypeIcon()
        {
            return Common.DailyTypes.Single(x => x.typeId == typeId).Icon();
        }
    }
    public class Stat
    {
        public SysUserStat RetrieveAsOptionGroup(int typeId)
        {
            List<SysUserStat> list = Overview().Where(x => x.typeId == typeId).ToList();
            return new SysUserStat()
            {
                typeId = list.First().typeId,
                typeTitle = list.First().typeTitle,
                optionId = -1,
                optionTitle = "Option Group",
                qtyMorning = list.Sum(x => x.qtyMorning),
                qty24h = list.Sum(x => x.qty24h),
                qtyMidnight = list.Sum(x => x.qtyMidnight),
                lastTimeTypeId = list.Max(x => x.lastTimeTypeId),
                lastTimeOptionId = list.Max(x => x.lastTimeOptionId),
                createUserId = list.First().createUserId
            };
        }
        public SysUserStat Retrieve(int typeId)
        {
            return Overview().SingleOrDefault(x => x.typeId == typeId && !x.optionId.HasValue);
        }
        public SysUserStat Retrieve(int typeId, int optionId)
        {
            return Overview().SingleOrDefault(x => x.typeId == typeId && x.optionId == optionId);
        }
        public List<SysUserStat> Overview()
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["UserStat"] == null)
            {
                //using (var db = new Db())
                {
                    Common.User.Stat.Reload();
                }
            }
            return (List<SysUserStat>)HttpContext.Current.Session["UserStat"];
        }
        //public static void Reload(int userId)
        //{
        //    using (var db = new Db())
        //    {
        //        HttpContext.Current.Session["UserStat"] = db.Database.SqlQuery<SysUserStat>("SELECT * FROM dbo.vw_Daily_Statistics WHERE createUserId = " + userId).ToList();
        //        HttpContext.Current.Session["UserStatReload"] = DateTime.Now;
        //    }
        //}
        public void Reload()
        {
            using (var db = new Db())
            {
                HttpContext.Current.Session["UserStat"] = db.Database.SqlQuery<SysUserStat>("SELECT * FROM dbo.vw_Daily_Statistics WHERE createUserId = " + Common.User.userId).ToList();
                HttpContext.Current.Session["UserStatReload"] = DateTime.Now;
            }
        }
    }
    public partial class SysUser
    {
        internal string ApiAuthenticateKey()
        {
            string key="";
            try
            {   
                using (DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider())
                {
                    // First pass
                    byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(userId.ToString());
                    MemoryStream memoryStream = new MemoryStream();
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(ApiController._keyOne, ApiController._keyTwo), CryptoStreamMode.Write);

                    cryptoStream.Write(bytesToEncrypt, 0, bytesToEncrypt.Length);
                    cryptoStream.FlushFinalBlock();

                    byte[] bytesToEncryptSecond = memoryStream.ToArray();

                    // Second pass
                    memoryStream = new MemoryStream();
                    cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(ApiController._keyTwo, ApiController._keyThree), CryptoStreamMode.Write);

                    cryptoStream.Write(bytesToEncryptSecond, 0, bytesToEncryptSecond.Length);
                    cryptoStream.FlushFinalBlock();

                    key = Convert.ToBase64String(memoryStream.ToArray());
                }

                return key;
            }
            catch (Exception)
            {
                return null;
            }
        }
        internal static SysUser RetrieveByApiKey(string key)
        {
            try
            {
                using (DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider())
                {
                    // First pass
                    MemoryStream memoryStream = new MemoryStream();
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(ApiController._keyTwo, ApiController._keyThree), CryptoStreamMode.Write);
                    cryptoStream.Write(Convert.FromBase64String(key), 0, Convert.FromBase64String(key).Length);
                    cryptoStream.FlushFinalBlock();

                    byte[] encryptedString2 = memoryStream.ToArray();

                    // Second pass
                    memoryStream = new MemoryStream();
                    cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(ApiController._keyOne, ApiController._keyTwo), CryptoStreamMode.Write);
                    cryptoStream.Write(encryptedString2, 0, encryptedString2.Length);
                    cryptoStream.FlushFinalBlock();

                    int userId = int.Parse(Encoding.UTF8.GetString(memoryStream.ToArray()));

                    return Retrieve(userId);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        public virtual Stat Stat { get; set; }

        public SysUser() { Stat = new Stat(); }
        public string CompleteName()
        {
            return firstName + " " + lastName;
        }
        #region cRudo
        public static SysUser Retrieve(int userId)
        {
            using (var db = new Db())
            {
                var user = db.SysUser.SingleOrDefault(c => c.userId == userId);
                if (user != null)
                {
                    HttpContext.Current.Session["User"] = user;
                    user.Stat.Reload();
                }
                return user;
            }
        }
        public static SysUser Retrieve(string logonId, string password)
        {
            using (var db = new Db())
            {
                var user = db.SysUser.SingleOrDefault(c => c.emailAddress == logonId && c.password == password);
                if (user != null)
                {
                    HttpContext.Current.Session["User"] = user;
                    user.Stat.Reload();
                }
                return user;
            }
        }
        #endregion
    }
}
