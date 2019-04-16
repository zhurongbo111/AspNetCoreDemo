using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CoreConfiguration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                //https://github.com/aspnet/AspNetCore/blob/master/src/DefaultBuilder/src/WebHost.cs
                //config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                //      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                //config.AddEnvironmentVariables();
                //config.AddCommandLine(args);
                config.AddJsonFile("config01.json");
                config.AddIniFile("config02.ini");
                config.AddXmlFile("config03.xml");
                config.AddInMemoryCollection(new Dictionary<string, string>() {
                    { "Author:Name","Gary"},
                    { "Author:Age","28" },
                    { "A","A"}
                });
                
            })
            .UseStartup<Startup>();
    }
}
