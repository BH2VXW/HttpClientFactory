using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Biz126.WebDemo.Controllers
{
    public class DemoController : ApiController
    {
        #region 0.HttpClient的常规用法
        //public async Task<string> Post()
        //{
        //    using (var httpClient = new HttpClient() { BaseAddress=new Uri("http://localhost") })
        //    {
        //        using (var responseData = await httpClient.PostAsync("/", new StringContent("")))
        //        {
        //            return await responseData.Content.ReadAsStringAsync();
        //        }
        //    }
        //}
        #endregion

        #region 1.HttpClient静态复用
        //返回的HashCode是一致的，可以确定已经只实例化了一次，实现了复用
        //private static readonly HttpClient httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost") };
        //public async Task<string> Post()
        //{
        //    using (var responseData = await httpClient.PostAsync("/", new StringContent("")))
        //    {
        //        return await responseData.Content.ReadAsStringAsync();
        //    }
        //}
        #endregion

        #region 2.依赖注入 - 注入HttpClient服务

        //private static HttpClient _httpClient;

        //public DemoController(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;
        //}

        //public async Task<string> Post()
        //{
        //    using (var responseData = await _httpClient.PostAsync("/", new StringContent("")))
        //    {
        //        return await responseData.Content.ReadAsStringAsync();
        //    }
        //}
        #endregion

        #region 3.依赖注入 - 对HttpClient进行简单的封装
        //private readonly HttpService.IHttpService _httpService;

        //public DemoController(HttpService.IHttpService httpService)
        //{
        //    _httpService = httpService;
        //}

        //public async Task<string> Post()
        //{
        //    return await _httpService.PostAsync("/", "", "utf-8", "text/html");
        //}
        #endregion

        #region 4.依赖注入 - 请求多个不同的Http服务

        private readonly Dictionary<string, HttpService.IHttpService> _httpServices;

        public DemoController(Dictionary<string, HttpService.IHttpService> httpService)
        {
            _httpServices = httpService;
        }

        public async Task<string> Post([FromUri]string key="demo1")
        {
            return await _httpServices[key].PostAsync("/", "", "utf-8", "text/html");
        }
        #endregion
    }
}
