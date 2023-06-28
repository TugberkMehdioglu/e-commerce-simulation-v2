using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.COMMON.Extensions
{
    public static class SessionExtension
    {
        public static void SetSession<T>(this ISession session, string key, T value) where T : class
        {
            string serializedValue = JsonConvert.SerializeObject(value);
            session.SetString(key, serializedValue);
        }

        public static T? GetSession<T>(this ISession session, string key) where T : class
        {
            string? serializedString = session.GetString(key);
            if (string.IsNullOrEmpty(serializedString)) return null;

            try
            {
                return JsonConvert.DeserializeObject<T>(serializedString);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
