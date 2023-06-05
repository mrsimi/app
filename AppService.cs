using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace app
{
    public static class AppService
    {
        public static void SetQuestions(this ISession session, string key, string value)
        {
            session.SetString(key, value);
        }
        public static List<string> GetQuestion(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<List<string>>(value);
        }
    }
}