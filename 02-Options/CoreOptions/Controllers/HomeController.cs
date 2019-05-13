using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreOptions.Models;
using Microsoft.Extensions.Options;
using CoreOptions.Options;

namespace CoreOptions.Controllers
{
    public class HomeController : Controller
    {
        IOptions<EmailOption> _options;
        IOptionsMonitor<EmailOption> _optionsMonitor;
        IOptionsSnapshot<EmailOption> _optionsSnapshot;

        public HomeController(IOptions<EmailOption> options, IOptionsMonitor<EmailOption> optionsMonitor, IOptionsSnapshot<EmailOption> optionsSnapshot)
        {
            _options = options;
            _optionsMonitor = optionsMonitor;
            _optionsSnapshot = optionsSnapshot;
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

        public IActionResult Demo()
        {
            EmailOption defaultEmailOption = _options.Value;

            EmailOption defaultEmailOption1 = _optionsMonitor.CurrentValue;//_optionsMonitor.Get(Microsoft.Extensions.Options.Options.DefaultName);
            EmailOption fromMemoryEmailOption1 = _optionsMonitor.Get("FromMemory");
            EmailOption fromConfigurationEmailOption1 = _optionsMonitor.Get("FromConfiguration");

            EmailOption defaultEmailOption2 = _optionsSnapshot.Value;//_optionsSnapshot.Get(Microsoft.Extensions.Options.Options.DefaultName);
            EmailOption fromMemoryEmailOption2 = _optionsSnapshot.Get("FromMemory");
            EmailOption fromConfigurationEmailOption2 = _optionsSnapshot.Get("FromConfiguration");
            return View();
        }
    }
}
