using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreConfiguration.Models;
using CoreConfiguration.Options;
using Microsoft.Extensions.Configuration;

namespace CoreConfiguration.Controllers
{
    public class HomeController : Controller
    {
        IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View(new HomeViewModel());
        }

        public IActionResult Start()
        {
            ViewBag.Item1 = _configuration["EnableCache"];
            ViewBag.Item2 = _configuration["Email"];

            IConfigurationSection emailConfiguration = _configuration.GetSection("Email");
            ViewBag.Item3 = emailConfiguration["To"];
            ViewBag.Item4 = emailConfiguration["From"];

            ViewBag.Item5 = _configuration["Email:To"];// why is ":" ?
            ViewBag.Item6 = _configuration["Email:From"];

            IConfigurationSection emailToConfiguration = emailConfiguration.GetSection("To");
            ViewBag.Item7 = emailToConfiguration.Value;//Same with emailConfiguration["To"]

            ViewBag.Item8 = _configuration.GetSection("Type").GetSection("Assembly").GetSection("Namespace")["FullName"];

            return View();
        }

        /// <summary>
        /// Microsoft.Extensions.Configuration.Binder
        /// </summary>
        /// <returns></returns>
        public IActionResult Binder()
        {
            IConfigurationSection emailConfiguration = _configuration.GetSection("Email");
            var emailOption = emailConfiguration.Get<EmailOption>();
            ViewBag.Item1 = emailOption.From;
            ViewBag.Item2 = emailOption.To;

            //string str1 = _configuration["EnableCache"];
            //bool enableCache = bool.Parse(str1);
            bool enableCache = _configuration.GetSection("EnableCache").Get<bool>();
            ViewBag.Item3 = enableCache;

            return View();
        }

        public IActionResult MultiSource()
        {
            IConfigurationSection emailConfiguration = _configuration.GetSection("Email");
            MultiEmailOption multiEmailOption = emailConfiguration.Get<MultiEmailOption>();

            //InMemory
            ViewBag.Item1 = _configuration["LogLevel"];
            ViewBag.Item2 = emailConfiguration["UseSSL"];

            //Xml
            ViewBag.Item3 = _configuration["RetryCount"];
            ViewBag.Item4 = emailConfiguration["BodyType"];

            //Ini
            ViewBag.Item5 = _configuration["LogType"];
            ViewBag.Item6 = emailConfiguration["Subject"];

            ViewBag.Item7 = Newtonsoft.Json.JsonConvert.SerializeObject(multiEmailOption);
            return View();
        }
    }
}
