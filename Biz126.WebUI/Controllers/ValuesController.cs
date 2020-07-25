using Biz126.HttpService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Biz126.WebUI.Controllers
{
    public class ValuesController : ApiController
    {
        private static Dictionary<string, IHttpService> _httpService;
        public ValuesController(Dictionary<string, IHttpService> httpService)
        {
            _httpService = httpService;
        }

        /// <summary>
        /// WebAPI 注入实例，POST请求
        /// </summary>
        /// <returns></returns>
        public async Task<string> Post() => await _httpService["demo1"].PostAsync("/api/login", "username=admin&password=admin888", "utf-8", "application/json");
    }
}
