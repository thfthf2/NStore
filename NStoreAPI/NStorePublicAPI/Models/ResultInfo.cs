using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NStorePublicAPI.Models
{
    /// <summary>
    /// Response 消息实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultInfo<T>
    {
        /// <summary>
        /// 等于0表示成功，反之表示失败
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public T Data { get; set; }
    }
}