using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DataAccess
{
    public static class ExtentionHelper
    {
        public static string ToJSON(this object obj, Formatting formatting = Formatting.None, bool ConvertToDate = false)
        {
            var _serializer = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore,
            };
            if (ConvertToDate)
                _serializer.DateFormatString = "dd-MM-yyyy hh:mm tt";
            _serializer.Converters.Add(new StringEnumConverter());

            return JsonConvert.SerializeObject(obj, formatting, _serializer);
        }
        public static T ToStringJSON<T>(this string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }
    }
}
