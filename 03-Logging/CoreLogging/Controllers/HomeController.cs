using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreLogging.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.Console;

namespace CoreLogging.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ILogger<HomeController> loggerT, ILoggerFactory loggerFactory,
            ILoggerProvider loggerProviders, IOptionsMonitor<LoggerFilterOptions> filter,
            ILoggerProviderConfigurationFactory loggerProviderConfigurationFactory, ILoggerProviderConfiguration<ConsoleLoggerProvider> loggerProviderConfiguration)
        {
            var testLog = loggerFactory.CreateLogger("Test");
            loggerT.LogTrace("HomeController: Trace");
            loggerT.LogDebug("HomeController: Debug");
            loggerT.LogInformation("HomeController: Info");
            loggerT.LogWarning("HomeController: Warning");
            loggerT.LogError("HomeController: Error");
            loggerT.LogCritical("HomeController: Critical");

            testLog.LogTrace("Test: Trace");
            testLog.LogDebug("Test: Debug");
            testLog.LogInformation("Test: Info");
            testLog.LogWarning("Test: Warning");
            testLog.LogError("Test: Error");
            testLog.LogCritical("Test: Critical");

            using (var scope = loggerT.BeginScope(filter))
            {
                loggerT.LogError("Scope Error");
            }
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
