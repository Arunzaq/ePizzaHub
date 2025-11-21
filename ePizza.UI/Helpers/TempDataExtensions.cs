using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ePizza.UI.Helpers
{
    public static class TempDataExtensions
    {
        public static void Set<T>(this ITempDataDictionary tempdata, string key, string value) where T : class
        {
            JsonSerializerOptions options
                    = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.IgnoreCycles
                    };
            tempdata[key] = JsonSerializer.Serialize(value, options);
        }
    }
}
