using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreLocalization.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;

namespace CoreLocalization.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly AllStringLocalizer _allStringLocalizer;

        public HomeController(IStringLocalizer<HomeController> localizer, AllStringLocalizer allStringLocalizer )
        {
            _localizer = localizer;
            _allStringLocalizer = allStringLocalizer;
        }

        public IActionResult Index()
        {
            var cultureFeature = HttpContext.Features.Get<IRequestCultureFeature>();
            var word = _localizer.GetString("Hello");

            word = _allStringLocalizer.GetString("TestKey");

            var zhLocalizer = _localizer.WithCulture(new System.Globalization.CultureInfo("zh-CN"));
            var enLocalizer = _localizer.WithCulture(new System.Globalization.CultureInfo("en-US"));
            word = zhLocalizer.GetString("Hello");
            word = enLocalizer.GetString("Hello");
            word = _localizer.GetString("Hello");

            word = _allStringLocalizer.GetString("TestKey");

            System.Globalization.CultureInfo.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            word = zhLocalizer.GetString("Hello");
            word = enLocalizer.GetString("Hello");
            word = _localizer.GetString("Hello");

            word = _allStringLocalizer.GetString("TestKey");

            System.Globalization.CultureInfo.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
            word = zhLocalizer.GetString("Hello");
            word = enLocalizer.GetString("Hello");
            word = _localizer.GetString("Hello");

            word = _allStringLocalizer.GetString("TestKey");
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
