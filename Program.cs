using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace my_expense_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment != "Development") {
                DotNetEnv.Env.Load();
            } else {
                DotNetEnv.Env.Load(".env.Development");
                
                Console.WriteLine("Development yan");
            }

            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.FullName;
         
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup(assemblyName);
                });
        }
    }
}
