using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Biz126.HttpService
{
    public class HttpService:IHttpService
    {
        private readonly HttpClient _client;

        /// <summary>
        /// 构建Http方法
        /// </summary>
        /// <param name="httpClient">HttpClient实例</param>
        /// <param name="config">配置</param>
        public HttpService(HttpClient httpClient,Config config)
        {            
            httpClient.BaseAddress = new Uri(config.BaseAddress);
            foreach (var header in config.Headers)
            {
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
            
            httpClient.Timeout = new TimeSpan(config.Timeout*10000000);
            httpClient.DefaultRequestHeaders.ExpectContinue = false;  //关闭Expect:[100-continue]，默认为开启状态   很多旧的HTTP/1.0和HTTP/1.1应用不支持Expect头部
            _client = httpClient;

            //忽略https证书有效性检查
            if (_client.BaseAddress.Scheme.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback =
                        new RemoteCertificateValidationCallback(CheckValidationResult);
            }
        }

        /// <summary>
        /// 忽略证书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }

        /// <summary>
        /// 异步Post提交
        /// </summary>
        /// <param name="path"></param>
        /// <param name="paramsData"></param>
        /// <param name="charset"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public async Task<string> PostAsync(string path,string paramsData,string charset,string contentType)
        {
            string result = "";
            try
            {
                using (var responseData = await _client.PostAsync(path, new StringContent(paramsData, Encoding.GetEncoding(charset), contentType)))
                {
                    return await responseData.Content.ReadAsStringAsync();
                }
            }
            catch (System.Threading.ThreadAbortException e)
            {
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                //if (e.Status == WebExceptionStatus.ProtocolError)
                //{

                //}
                throw (e);
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                
            }

            return result;
        }
    }
}
