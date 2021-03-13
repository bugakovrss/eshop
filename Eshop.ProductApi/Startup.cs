using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Eshop.Core.Extensions;
using Eshop.ProductApi.ErrorHandling;
using Eshop.ProductApi.Extensions;
using Eshop.ProductApi.Mapping;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Eshop.ProductApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(options =>
                    {
                        options.Filters.Add<ApiExceptionFilter>();
                    }).FormatResponse();

            services.AddSwaggerApi("Eshop.ProductApi",
                false,
                "Eshop.ProductApi.xml");

            services.AddInMemoryContext();

            services.AddAutoMapper(Assembly.GetAssembly(typeof(ProductProfile)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider versionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUIWithVersion(versionProvider);
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
