﻿using System.Text.Json;

namespace Client
{
    public static class SessionExtension
    {
        public static void SetObject(this ISession session, string key, object data)
        {
            session.SetString(key, JsonSerializer.Serialize(data));
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            var sessionJsonData = session.GetString(key);
            if (sessionJsonData != null)
            {
                return JsonSerializer.Deserialize<T>(sessionJsonData);
            }
            return default(T);
        }
    }
}
