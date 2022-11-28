using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using infomanager.DAL;

namespace infomanager.Api
{
    public partial class ApiHelper
    {
        internal static JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            MaxDepth = 1
        };
        public static infomanager_dk_db_mainEntities Db()
        {
            var context = new infomanager_dk_db_mainEntities();
            context.Configuration.LazyLoadingEnabled = false; // Required to serialize to json

            return context;
        }
    }
}