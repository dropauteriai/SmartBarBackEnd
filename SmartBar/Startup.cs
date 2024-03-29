﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Persistence;
using Services;
using System.Net;

namespace SmartBar
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SmartBar API",
                    Description = "Easy bar work",
                    Version = "v1"
                });
            });

         
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyPath",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            //builder.Services.AddDbContext<SmartBarDb>(options => options.UseInMemoryDatabase("items"));
            string connectionString = Configuration.GetConnectionString("SmartBarDb");

            //var connectionString = builder.Configuration.GetConnectionString("Tables") ?? "Data Source=Tables.db";

            services.AddDbContext<SmartBarDb>(options => options.UseSqlServer(connectionString));
            //builder.Services.AddSqlite<SmartBarDb>(connectionString);
            //services.AddSqlite<SmartBarDb>(connectionString);

            services.AddScoped<ITableService, TableService>();

            services.AddSingleton<SmartBarDb>();

            services.AddSwaggerGen();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(builder => builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader());
            
            app.UseHttpsRedirection();
            
            app.UseAuthentication();

            app.UseRouting();


            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "smartbar api v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.MapControllers();

            //app.Run();
        }
    }
}
