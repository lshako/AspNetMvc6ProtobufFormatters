﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using AspNetCoreProtobuf.Formatters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;

namespace AspNetCoreProtobuf
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
        }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.InputFormatters.Add(new ProtobufInputFormatter());
                options.OutputFormatters.Add(new ProtobufOutputFormatter());
                options.FormatterMappings.SetMediaTypeMappingForFormat("protobuf",  MediaTypeHeaderValue.Parse("application/x-protobuf"));
            });
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
