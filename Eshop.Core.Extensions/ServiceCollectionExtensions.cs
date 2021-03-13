using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Eshop.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Formatting json response
        /// </summary>
        /// <param name="mvcCoreBuilder"></param>
        /// <returns></returns>
        public static IMvcBuilder FormatResponse(this IMvcBuilder mvcCoreBuilder)
        {
            mvcCoreBuilder.AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    opt.SerializerSettings.Formatting = Formatting.Indented;
                    opt.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                    opt.SerializerSettings.Converters.Add(new StringEnumConverter(typeof(CamelCaseNamingStrategy)));
                })
                .AddJsonOptions(options =>
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase))); ;

            return mvcCoreBuilder;
        }
    }
}
