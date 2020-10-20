using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TweetBook.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using TweetBook.Options;
using Microsoft.OpenApi.Models;
using TweetBook.Installers;

namespace TweetBook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.InstallServicesIntoAssembly(_configuration);
          
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {

                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            var swaggerOptions = new SwaggerOption();
            _configuration.GetSection(nameof(SwaggerOption)).Bind(swaggerOptions);
            app.UseSwagger(options =>
           {
               options.RouteTemplate = "swagger/{documentName}/swagger.Json";
           });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(_configuration["swaggerOPtions:UIEndpoint"], "Our Api");
            });


            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseMvc();
        }
    } 
}

