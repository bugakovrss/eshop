using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Eshop.Core.Extensions
{
    public static class SwaggerExtensions
    {
        public static IApplicationBuilder UseSwaggerUIWithVersion(
            this IApplicationBuilder builder,
            IApiVersionDescriptionProvider versionProvider)
        {
            return builder.UseSwaggerUI(options =>
            {
                foreach (var versionDescription in versionProvider.ApiVersionDescriptions)
                    options.SwaggerEndpoint("/swagger/" + versionDescription.GroupName + "/swagger.json",
                        versionDescription.GroupName.ToUpperInvariant());
            });
        }

        /// <summary>
        /// Configure swagger
        /// </summary>
        /// <param name="serviceCollection">Services</param>
        /// <param name="applicationName">Application name</param>
        /// <param name="withBearer">Enable bearer authorization</param>
        /// <param name="xmlDescriptionFiles">App models description files</param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerApi(this IServiceCollection serviceCollection, string applicationName,
            bool withBearer = false, params string[] xmlDescriptionFiles)
        {
            serviceCollection.AddSwaggerInternal(applicationName,
                options =>
                {
                    string basePath = AppContext.BaseDirectory;

                    foreach (var xmlDescriptionFile in xmlDescriptionFiles)
                    {
                        options.IncludeXmlComments(Path.Combine(basePath, xmlDescriptionFile));
                    }
                    
                }, withBearer);

            return serviceCollection;
        }

        private static IServiceCollection AddSwaggerInternal(this IServiceCollection services,
            string appName, Action<SwaggerGenOptions> configure = null, bool withBearer = false)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(options =>
            {
                foreach (var versionDescription in services.BuildServiceProvider()
                    .GetRequiredService<IApiVersionDescriptionProvider>()
                    .ApiVersionDescriptions)
                    options.SwaggerDoc(versionDescription.GroupName, new OpenApiInfo
                    {
                        Title = appName,
                        Version = versionDescription.ApiVersion.ToString()
                    });

                if (withBearer)
                {
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
                }

                options.UseInlineDefinitionsForEnums();

                configure?.Invoke(options);
            });

            return services;
        }
    }
}
