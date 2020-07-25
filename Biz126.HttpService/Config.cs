using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz126.HttpService
{
    /// <summary>
    /// 配置
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Http请求服务器
        /// </summary>
        public string BaseAddress { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public int Timeout { get; set; } = 0;

        /// <summary>
        /// Http请求头
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }
    }
}
