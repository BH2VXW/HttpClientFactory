using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Biz126.WebDemo
{
    public class IocConfig
    {
        public static void RegisterDependencies()
        {
            ContainerBuilder builder = new ContainerBuilder();
            HttpConfiguration config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());



            #region 注入HttpClient
            //builder.Register(x => httpClient).As<HttpClient>().SingleInstance();  //注册HttpClient服务
            #endregion


            #region HttpClient简单封装后注入

            //HttpClient httpClient = new HttpClient
            //{
            //    BaseAddress = new Uri("http://localhost")
            //};
            //builder.Register(x => new HttpService.HttpService(httpClient)).As<HttpService.IHttpService>().SingleInstance(); //注册封装的HttpService方法
            #endregion

            #region 多个不同接口注入
            //var httpServices = new Dictionary<string, HttpService.IHttpService>
            //{
            //    {
            //        "demo1",
            //        new HttpService.HttpService(new HttpClient(){ BaseAddress=new Uri("http://localhost")})
            //    },
            //    {
            //        "demo2",
            //        new HttpService.HttpService(new HttpClient(){ BaseAddress=new Uri("http://localhost:8001")})
            //    }
            //};
            //builder.RegisterInstance(httpServices).As<Dictionary<string, HttpService.IHttpService>>().SingleInstance();
            #endregion

            #region 增加多接口配置文件的注入
            builder.RegisterInstance(new HttpService.HttpConfig().HttpServiceList()).As<Dictionary<string, HttpService.IHttpService>>().SingleInstance();
            #endregion

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}