using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebStore.Helpers
{
    public static class SessionExtension
    {
        public static void SetSubject(this ISession session, string key, object obj)
        {
            session.SetString(key, JsonConvert.SerializeObject(obj));
        }

        public static T GetSubject<T>(this ISession session, string key)
        {
            string stringValue = session.GetString(key);
            return string.IsNullOrEmpty(stringValue) ? default(T) 
                : JsonConvert.DeserializeObject<T>(stringValue);
        }
    }
}
