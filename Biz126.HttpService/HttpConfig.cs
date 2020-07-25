using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Web;

namespace Biz126.HttpService
{
    /// <summary>
    /// 处理配置
    /// </summary>
    public class HttpConfig
    {
        string configPath;
        public HttpConfig()
        {
            configPath = string.Format("{0}{1}", HttpRuntime.AppDomainAppPath, System.Web.Configuration.WebConfigurationManager.AppSettings["httpConfig"]);
        }

        public HttpConfig(string path)
        {
            configPath = string.Format("{0}{1}", HttpRuntime.AppDomainAppPath, path);
        }

        /// <summary>
        /// 取链接配置
        /// </summary>
        /// <returns></returns>
        public List<Config> GetConfig()
        {
            if (File.Exists(configPath))
            {
                using (StreamReader f2 = new StreamReader(configPath, System.Text.Encoding.GetEncoding("gb2312")))
                {
                    return JsonConvert.DeserializeObject<List<Config>>(f2.ReadToEnd());
                }                   
            }
            return new List<Config>();
        }

        /// <summary>
        /// 指定链接的配置
        /// </summary>
        /// <param name="UrlAddress"></param>
        /// <returns></returns>
        public Config GetConfig(string key)
        {
            var list = GetConfig();
            if (list.Count > 0)
            {
                return list.Where(x => (x.Key.Equals(key, StringComparison.OrdinalIgnoreCase))).FirstOrDefault();
            }
            return new Config();
        }

        /// <summary>
        /// 生成HttpService列表
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, IHttpService> HttpServiceList()
        {
            var dict=new Dictionary<string, IHttpService>();
            GetConfig().ForEach(x =>
            {
                var httpClient = new HttpClient();
                dict.Add(x.Key, new HttpService(httpClient, x));
            });
            return dict;
        }
    }
}
