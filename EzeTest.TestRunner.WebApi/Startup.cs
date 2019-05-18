namespace EzeTest.TestRunner.WebApi
{
    using EzeTest.Framework.Http;
    using EzeTest.Framework.Mapping;
    using EzeTest.TestRunner.Factory;
    using EzeTest.TestRunner.Repositories;
    using EzeTest.TestRunner.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Swashbuckle.AspNetCore.Swagger;
    using System;
    using System.IO;
    using System.Reflection;

    public class Startup
    {
        private const string serviceName = "EzeTest.TestRunner";

        public Startup()
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IObjectMapper<,>), typeof(AutoMapperObjectMapper<,>));

            services.AddScoped<IHttpAuthProvider, HttpAuthProvider>();
            services.AddScoped<ITestRunNotificationService, TestRunNotificationService>();
            services.AddScoped<ITestCommandFactory, TestCommandFactory>();
            services.AddScoped<ITestRepository, TestRepository>();
            services.AddScoped<ITestRunnerService, TestRunnerService>();
            services.AddScoped<ITestOrchestrationService, TestOrchestrationService>();

            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = serviceName, Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", serviceName);
            });

            app.UseMvc();
        }
    }
}
