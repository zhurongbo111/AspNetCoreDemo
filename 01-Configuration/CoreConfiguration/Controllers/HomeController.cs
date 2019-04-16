using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreConfiguration.Models;
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
            var children = _configuration.GetChildren();
            foreach (var childSection in children)
            {
                Console.WriteLine("1");
            }
            return View(new HomeViewModel());
        }
    }
}
