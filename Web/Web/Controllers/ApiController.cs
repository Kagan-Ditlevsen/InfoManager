using dk.infomanager.Models;
using System;
using System.Web;
using System.Web.Mvc;
using Skd.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Skd;
using System.Text;

namespace dk.infomanager.Controllers
{
    public class ApiController : Controller
    {
        // stig: http://im.localhost/api/xnQu7ZZ6qZMumfa3ou2JMw==/authenticate
        internal static readonly byte[] _keyOne = Encoding.UTF8.GetBytes("Y5Q~H7Pe");
        internal static readonly byte[] _keyTwo = Encoding.UTF8.GetBytes("f*yMbv4d");
        internal static readonly byte[] _keyThree = Encoding.UTF8.GetBytes("%+(x8zR,");
        static JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Error
        };
        static SysUser user;
        static void Auth(string authId)
        {
            user = SysUser.RetrieveByApiKey(authId);
            if (user == null) throw new UnauthorizedAccessException();
        }

        public object Setup(string authId)
        {
            Auth(authId);

            var obj = new
            {
                SysUser = new SysUser(),
                Daily = new Daily(),
                DailyInfo= new DailyInfo(),
                DailyType = DailyType.Overview(),
                DailyTypeExtra = DailyTypeExtra.Overview(),
                DailyTypeOption = DailyTypeOption.Overview()
            };

            return JsonConvert.SerializeObject(obj, Formatting.None, serializerSettings);
        }

        #region Daily cRudO
        public string Daily_Retrieve(string authId, Guid id)
        {
            Auth(authId);

            Daily obj = Daily.Retrieve(id);

            return JsonConvert.SerializeObject(obj);
        }

        public string Daily_Overview(string authId, int count = 500)
        {
            Auth(authId);

            var obj = Daily.Overview(user.userId);

            return JsonConvert.SerializeObject(obj.Take(3), Formatting.None, serializerSettings);
        }
        #endregion

        string AuthenticateGet()
        {
            return SysUser.Retrieve(10001).ApiAuthenticateKey();
        }


    }
}
//var result = from o in Daily.Overview(userId)
//             select new
//             {
//                 Id = o.dailyId,
//                 RegisterDateTime = o.registerDateTime,
//                 TypeId = o.typeId,
//                 OptionId = o.optionId,
//                 Remark = o.remark,
//                 CreateDateTime = o.createDateTime
//             };