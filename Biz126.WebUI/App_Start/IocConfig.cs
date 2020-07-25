using Autofac;
using Autofac.Integration.WebApi;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Biz126.HttpService;
using System.Net;
using System.Web.Mvc;

namespace Biz126.WebUI
{
    public class IocConfig
    {
        public static void RegisterDependencies()
        {
            ContainerBuilder builder = new ContainerBuilder();
            HttpConfiguration config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());//用于注入WebAPI
            builder.RegisterControllers(Assembly.GetExecutingAssembly());//用于注入MVC

            //设置最大连接数
            ServicePointManager.DefaultConnectionLimit = int.MaxValue;
            //把我们自己封装的Http方法注册到服务中，注册的生命周期为
            builder.RegisterInstance(new HttpConfig().HttpServiceList()).As<Dictionary<string, IHttpService>>().SingleInstance();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container); //用于注入WebAPI
            //config.DependencyResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));   //用于注入MVC
        }
    }
}