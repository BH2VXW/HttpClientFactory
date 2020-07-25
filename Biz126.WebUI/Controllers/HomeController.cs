using Biz126.HttpService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Biz126.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private static Dictionary<string, IHttpService> _httpService;
        public HomeController(Dictionary<string, IHttpService> httpService)
        {
            _httpService = httpService;
        }

        /// <summary>
        /// MVC注入演示
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Home Page";

            var test = await _httpService["demo2"].PostAsync("/api/demo2", "type=demo", "utf-8", "text/html");
            ViewBag.Message = test;
            return View();
        }
    }
}
