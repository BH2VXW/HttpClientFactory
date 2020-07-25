using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz126.HttpService
{
    public interface IHttpService
    {
        /// <summary>
        /// 异步POST方法
        /// </summary>
        /// <param name="path">请求路径</param>
        /// <param name="paramsData">参数</param>
        /// <param name="charset">编码</param>
        /// <param name="contentType">类型</param>
        /// <returns></returns>
        Task<string> PostAsync(string path, string paramsData, string charset, string contentType);
    }
}
