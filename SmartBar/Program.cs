using Microsoft.EntityFrameworkCore;
using Persistence;
using Microsoft.OpenApi.Models;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors.Infrastructure;
using SmartBar;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
   // var builder = WebApplication.CreateBuilder(args);
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });


    //builder.Services.AddDbContext<SmartBarDbContext>(options =>
    //                options.UseSqlite(@"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SmartBarDB;Integrated Security=True;"));

    //var app = builder.Build();

    // Configure the HTTP request pipeline.

    
}
