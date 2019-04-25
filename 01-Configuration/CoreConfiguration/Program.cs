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
                #region Default
                //https://github.com/aspnet/AspNetCore/blob/master/src/DefaultBuilder/src/WebHost.cs
                //config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                //      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                //Microsoft.Extensions.Configuration.EnvironmentVariables
                //config.AddEnvironmentVariables();
                //Microsoft.Extensions.Configuration.CommandLine
                //config.AddCommandLine(args);
                #endregion

                // Microsoft.Extensions.Configuration.Abstractions
                // Microsoft.Extensions.Configuration
                // Microsoft.Extensions.Configuration.FileExtensions
                // Microsoft.Extensions.Configuration.Json
                config.AddJsonFile("config01.json");

                #region Multi-ConfigSource
                config.AddInMemoryCollection(new Dictionary<string, string>() {
                    { "LogLevel","Debug"},
                    { "Email:UseSSL","true"}
                });

                //Microsoft.Extensions.Configuration.Ini
                config.AddIniFile("config02.ini");

                //Microsoft.Extensions.Configuration.Xml
                config.AddXmlFile("config03.xml");

                config.AddJsonFile("config04.json",true,true);
                #endregion 

            })
            .UseStartup<Startup>();
    }
}
