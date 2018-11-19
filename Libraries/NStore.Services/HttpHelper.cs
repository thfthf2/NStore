using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace NStore.Services
{
    public class HttpHelper
    {
        #region 私有变量
        //默认的编码
        private Encoding _encoding = Encoding.Default;
        //Post数据编码
        private Encoding _postencoding = Encoding.Default;

        private System.Net.Http.HttpClient _client;

        private HttpClientHandler _httpHandler;

        private HttpItem _httpItem;

        private HttpRequestMessage _requestMessage;

        private object objLock = null;
        #endregion

        #region HttpHelper
        ///初始化HttpClient对象
        ///静态构造函数
        internal HttpHelper()
        {
            _httpHandler = new HttpClientHandler();

            _httpItem = new HttpItem();

            _requestMessage = new HttpRequestMessage();

            objLock = new object();
        }
        #endregion

        #region SetHttpClient
        /// <summary>
        /// 为请求做准备工作
        /// </summary>
        internal void SetHttpClient()
        {
            // 设置代理
            WebProxy proxy = SetProxy(_httpItem);

            SetHttpClientHandler(proxy);

            _client = new System.Net.Http.HttpClient(_httpHandler);

            //设置Header默认参数
            if (_httpItem.Header != null && _httpItem.Header.Count > 0)
                foreach (string key in _httpItem.Header.AllKeys)
                {
                    _client.DefaultRequestHeaders.Add(key, _httpItem.Header[key]);
                }

            //设置HttpVersion
            if (_httpItem.ProtocolVersion != null)
                _requestMessage.Version = _httpItem.ProtocolVersion;

            //设置超时时间
            //_client.Timeout = new TimeSpan(_httpItem.Timeout);
            SetRequestTimeOut();

            if (!string.IsNullOrWhiteSpace(_httpItem.Host))
            {
                _client.BaseAddress = new Uri(_httpItem.Host);
            }
            //MaxResponseContentBufferSizee
            _client.MaxResponseContentBufferSize = _httpItem.MaxResponseContentBufferSize;
            //Accept 头
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_httpItem.Accept));
            //ContentType返回类型
            //UserAgent客户端的访问类型，包括浏览器版本和操作系统信息
            //User_Agent头
            _client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");

        }
        #endregion

        #region SetHttpClientHandler
        /// <summary>
        /// 设置代理服务器等信息
        /// </summary>
        /// <param name="proxy"></param>
        internal void SetHttpClientHandler(WebProxy proxy)
        {
            //是否执行跳转功能
            //httpHandler.AllowAutoRedirect = httpItem.Allowautoredirect;
            //设置安全凭证
            //httpHandler.Credentials = httpItem.ICredentials;
            //设置接受Content的缓冲大小
            //httpHandler.MaxRequestContentBufferSize = HttpHelper.httpItem.MaxRequestContentBufferSize;
            //设置代理服务器
            if (proxy != null)
            {
                _httpHandler.Proxy = proxy;
            }

        }
        #endregion

        #region CheckJson
        /// <summary>
        /// 检查Json格式的准确性
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal string CheckJson(string value)
        {
            Regex reg = new Regex(@"^\[.*\]$");
            if (reg.IsMatch(value))
            {
                return value;
            }
            else
            {
                return "[" + value + "]";
            }
        }
        #endregion

        #region SetProxy
        /// <summary>
        /// 设置代理
        /// </summary>
        /// <param name="httpItem">参数对象</param>
        private WebProxy SetProxy(HttpItem httpItem)
        {
            if (httpItem == null)
            {
                httpItem = this._httpItem;
            }
            //初始化参数 是否使用IE代理配置
            bool isIeProxy = false;

            if (!string.IsNullOrWhiteSpace(httpItem.ProxyIp))
            {
                isIeProxy = httpItem.ProxyIp.ToLower().Contains("ieproxy");
            }
            if (!string.IsNullOrWhiteSpace(httpItem.ProxyIp) && !isIeProxy)
            {
                //设置代理服务器 服务器地址+端口号
                if (httpItem.ProxyPort > 0)
                {
                    //设置端口号和服务器地址
                    WebProxy myProxy = new WebProxy(httpItem.ProxyIp, httpItem.ProxyPort);
                    //建议连接的用户名和密码
                    myProxy.Credentials = new NetworkCredential(httpItem.ProxyUserName, httpItem.ProxyPwd);
                    //返回当前请求对象
                    return myProxy;
                }
                else
                {
                    WebProxy myProxy = new WebProxy(httpItem.ProxyIp, false);
                    //建议连接
                    myProxy.Credentials = new NetworkCredential(httpItem.ProxyUserName, httpItem.ProxyPwd);
                    //给当前请求对象
                    return myProxy;
                }
            }
            else if (isIeProxy)
            {

                return null;
            }
            else
            {
                return httpItem.WebProxy;
            }
        }
        #endregion

        #region SetDefaultHeaders
        private void SetDefaultHeaders(WebHeaderCollection webHeaders)
        {
            lock (objLock)
            {
                if (_httpItem.Security)
                {
                    if (webHeaders != null && webHeaders.Count > 0)
                    {
                        foreach (string key in webHeaders.AllKeys)
                        {
                            if (_client.DefaultRequestHeaders.Contains(key))
                            {
                                _client.DefaultRequestHeaders.Remove(key);
                            }
                            _client.DefaultRequestHeaders.Add(key, webHeaders[key]);
                        }
                    }
                }
                else
                {   //移除指定的标头
                    if (webHeaders != null && webHeaders.Count > 0)
                    {
                        foreach (string key in webHeaders.AllKeys)
                        {
                            if (_client.DefaultRequestHeaders.Contains(key))
                            {
                                _client.DefaultRequestHeaders.Remove(key);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region SetRequestTimeOut
        /// <summary>
        /// 设置请求超时
        /// </summary>
        private void SetRequestTimeOut()
        {
            if (_httpItem.Timeout <= 0)
            {
                _client.Timeout = new TimeSpan(_httpItem.DefaultTimeOut);
            }
            else
            {
                _client.Timeout = new TimeSpan(_httpItem.Timeout);
            }
        }
        #endregion

        #region SetHttpItem
        /// <summary>
        /// 获取HttpHelper类的Http参数信息
        /// </summary>
        /// <returns></returns>
        public HttpItem SetHttpItem
        {
            get { return _httpItem; }
            set { _httpItem = value; }
        }
        #endregion

        #region SetHttpClienBuffer
        /// <summary>
        /// 单位字节
        /// </summary>
        public long SetHttpBufferSize
        {
            set {
                if (_httpItem != null)
                {
                    _httpItem.MaxResponseContentBufferSize = value;
                }
                if (_client != null)
                {
                    _client.MaxResponseContentBufferSize = value;
                }
            }
        }
        #endregion

        #region HttpHost
        public string HttpHost
        {
            get { return _httpItem.Host; }
        }
        #endregion

        #region HttpGet
        /// <summary>
        /// Http Get方式请求/查询数据
        /// </summary>
        /// <param name="url">接口地址</param>
        /// <param name="webHeaders">HttpHeader 可增加一些验证信息</param>
        /// <param name="resultType">返回结果的类型</param>
        /// <returns></returns>
        public ResponseResult<T> HttpGet<T>(string url,WebHeaderCollection webHeaders = null, 
            ResponseResultType resultType=ResponseResultType.ResultInfo)
        {
            try
            {
                //设置全局定义的个性化安全认证
                SetDefaultHeaders(_httpItem.SecurityHeaders);
                //加载或者更新私定的安全认证
                SetDefaultHeaders(webHeaders);

                var GetResult = _client.GetAsync(url);
                //处理数据解析
                return JsonConvertResultData<T>(GetResult,resultType);
            }
            catch (InvalidOperationException ex)
            {
                return new ResponseResult<T>() { Code = (int)HttpStatusCode.BadRequest, Msg = ex.StackTrace };
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>() { Code = (int)HttpStatusCode.BadRequest, Msg = ex.StackTrace };
            }
        }
        #endregion

        #region HttpPost
        /// <summary>
        /// Http Post方式新增数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public ResponseResult<T> HttpPost<T>(string url, object content, WebHeaderCollection webHeaders = null,
            ResponseResultType resultType = ResponseResultType.ResultInfo)
        {
            try
            {
                //设置全局定义的个性化安全认证
                SetDefaultHeaders(_httpItem.SecurityHeaders);
                //加载或者更新私定的安全认证
                SetDefaultHeaders(webHeaders);

                var postData = JsonConvert.SerializeObject(content);
                var httpContent = new StringContent(postData, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json") { CharSet = "utf-8" };
                var postResult = _client.PostAsync(url, httpContent);
                //处理数据解析
                return JsonConvertResultData<T>(postResult,resultType);
            }
            catch (InvalidOperationException ex)
            {
                return new ResponseResult<T>() { Code = (int)HttpStatusCode.BadRequest, Msg = ex.StackTrace };
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>() { Code = (int)HttpStatusCode.BadRequest, Msg = ex.StackTrace };
            }
        }
        #endregion

        #region HttpPut
        /// <summary>
        /// Http Put方式更新数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public ResponseResult<T> HttpPut<T>(string url, object content, WebHeaderCollection webHeaders = null,
            ResponseResultType resultType = ResponseResultType.ResultInfo)
        {
            try
            {
                //设置全局定义的个性化安全认证
                SetDefaultHeaders(_httpItem.SecurityHeaders);
                //加载或者更新私定的安全认证
                SetDefaultHeaders(webHeaders);

                var postData = JsonConvert.SerializeObject(content);
                var httpContent = new StringContent(postData, Encoding.UTF8);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json") { CharSet = "utf-8" };
                var postResult = _client.PutAsync(url, httpContent);
                //处理数据解析
                return JsonConvertResultData<T>(postResult, resultType);
            }
            catch (InvalidOperationException ex)
            {
                return new ResponseResult<T>() { Code = (int)HttpStatusCode.BadRequest, Msg = ex.StackTrace };
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>() { Code = (int)HttpStatusCode.BadRequest, Msg = ex.StackTrace };
            }
        }
        #endregion

        #region HttpDelete
        /// <summary>
        /// Http DELETE请求方式 删除数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public ResponseResult<T> HttpDelete<T>(string url, WebHeaderCollection webHeaders = null,
            ResponseResultType resultType = ResponseResultType.ResultInfo)
        {
            try
            {

                //设置全局定义的个性化安全认证
                SetDefaultHeaders(_httpItem.SecurityHeaders);
                //加载或者更新私定的安全认证
                SetDefaultHeaders(webHeaders);

                //var postDate = JsonConvert.SerializeObject("");
                //var httpContent = new StringContent(postDate, Encoding.UTF8);
                //httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json") { CharSet = "utf-8" };
                var postResult = _client.DeleteAsync(url);
                //处理数据解析
                return JsonConvertResultData<T>(postResult, resultType);
            }
            catch (InvalidOperationException ex)
            {
                return new ResponseResult<T>() { Code = (int)HttpStatusCode.BadRequest, Msg = ex.StackTrace };
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>() { Code = (int)HttpStatusCode.BadRequest, Msg = ex.StackTrace };
            }
        }
        #endregion

        #region HttpClientRequest
        /// <summary>
        /// HttpClient请求 采用SendAsync方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestUri">请求地址</param>
        /// <param name="content">发送数据</param>
        /// <param name="httpMethod">请求方式</param>
        /// <returns></returns>
        public ResponseResult<T> HttpClientRequest<T>(string requestUri, object content,string httpMethod, 
            WebHeaderCollection webHeaders = null,
            ResponseResultType resultType = ResponseResultType.ResultInfo)
        {
            try
            {
                //设置全局定义的个性化安全认证
                SetDefaultHeaders(_httpItem.SecurityHeaders);
                //加载或者更新私定的安全认证
                SetDefaultHeaders(webHeaders);

                var method = new HttpMethod(httpMethod);
                var postData = JsonConvert.SerializeObject(content);
                var httpContent = new StringContent(postData, Encoding.UTF8);

                var request = new HttpRequestMessage(method, requestUri)
                {
                    Content = httpContent
                };
                var responseResult = _client.SendAsync(request);
                //处理数据解析
                return JsonConvertResultData<T>(responseResult, resultType);
            }
            catch (InvalidOperationException ex)
            {
                return new ResponseResult<T>() { Code = (int)HttpStatusCode.BadRequest, Msg = ex.Message };
            }
        }
        #endregion

        #region JsonConvertResultData<T>
        /// <summary>
        /// 解析Task<HttpResponseMessage>对象
        /// 返回的结果类型ResponseResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpResponse"></param>
        /// <returns>ResponseResult<T></returns>
        private ResponseResult<T> JsonConvertResultData<T>(Task<HttpResponseMessage> httpResponse,ResponseResultType resultType)
        {
            try
            {
                httpResponse.Result.EnsureSuccessStatusCode();

                HttpResponseMessage response = httpResponse.Result;

                if (response.IsSuccessStatusCode)
                {
                    //请求成功 200
                    //将HTTP 内容序列化到字符串以作为异步操作
                    var Readresult = response.Content.ReadAsStringAsync();

                    //没有任何返回内容
                    if (string.IsNullOrEmpty(Readresult.Result))
                    {
                        return new ResponseResult<T>() { Code = 0, Msg = "没有返回内容!" };
                    }
                    else
                    {
                        try
                        {
                            if (resultType == ResponseResultType.ResultInfo)
                            {
                                var ResultList = JsonConvert.DeserializeObject<ResponseResult<T>>(Readresult.Result);
                                return ResultList;
                            }
                            else
                            {
                                var Result = JsonConvert.DeserializeObject<T>(Readresult.Result);
                                return new ResponseResult<T>()
                                {
                                    Code = 0,
                                    Msg = "",
                                    Data = Result
                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            return new ResponseResult<T>() { Code = 0, Msg = Readresult.Result };
                        }
                    }
                }
                else
                {
                    return new ResponseResult<T>() { Code = (int)HttpStatusCode.BadRequest, Msg = "HttpCode：" + response.StatusCode.ToString() };
                }
            }
            catch (HttpRequestException hre)
            {
                return new ResponseResult<T>() { Code = (int)HttpStatusCode.BadRequest, Msg = hre.Message };
            }
            catch (Exception ex)
            {
                return new ResponseResult<T>() { Code = (int)HttpStatusCode.BadRequest, Msg = ex.Message };
            }
        }
        #endregion 

        #region SetPostData方法
        /// <summary>
        /// 设置Post数据
        /// </summary>
        /// <param name="item">Http参数</param>
        //private static void SetPostData(HttpItem item)
        //{

        //    //验证在得到结果时是否有传入数据
        //    if (client.Method.Trim().ToLower().Contains("post"))
        //    {
        //        if (item.PostEncoding != null)
        //        {
        //            postencoding = item.PostEncoding;
        //        }
        //        byte[] buffer = null;
        //        //写入Byte类型
        //        if (item.PostDataType == PostDataType.Byte && item.PostdataByte != null && item.PostdataByte.Length > 0)
        //        {
        //            //验证在得到结果时是否有传入数据
        //            buffer = item.PostdataByte;
        //        }//写入文件
        //        else if (item.PostDataType == PostDataType.FilePath && !string.IsNullOrWhiteSpace(item.Postdata))
        //        {
        //            StreamReader r = new StreamReader(item.Postdata, postencoding);
        //            buffer = postencoding.GetBytes(r.ReadToEnd());
        //            r.Close();
        //        } //写入字符串
        //        else if (!string.IsNullOrWhiteSpace(item.Postdata))
        //        {
        //            buffer = postencoding.GetBytes(item.Postdata);
        //        }
        //        if (buffer != null)
        //        {
        //            client.ContentLength = buffer.Length;
        //            client.GetRequestStream().Write(buffer, 0, buffer.Length);
        //        }
        //    }
        //}
        #endregion

        #region demo代码
        //设置UserAgent
        //var webRequest = WebRequest.Create(url) as HttpWebRequest;
        //webRequest.Headers.Add("UserAgent", "contact@cnblogs.com");
        //HttpClient httpClient = new HttpClient();
        //httpClient.MaxResponseContentBufferSize = 256000;
        //httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/36.0.1985.143 Safari/537.36");
        //String url = "http://passport.cnblogs.com/login.aspx";
        //HttpResponseMessage response = httpClient.GetAsync(new Uri(url)).Result;
        //String result = response.Content.ReadAsStringAsync().Result;


        //中文问题

        //        HttpResponseMessage response = task.Result;
        //        MediaTypeHeaderValue contentType = response.Content.Headers.ContentType; 
        //if(string.IsNullOrEmpty(contentType.CharSet)) 
        //{ 
        //contentType.CharSet = "GBK"; 
        //}

        //请求内容过长 超过64K则会抛出异常HttpRequestException，导致Get失败。
        //HttpClient client = new HttpClient() { MaxResponseContentBufferSize = 1024 * 1024 }; 


        //自定义请求头 HttpClientHandler
        //protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        //{
        //    request.Headers.Referrer = new Uri("http://www.google.com/");
        //    request.Headers.Add("UserAgent", "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; .NET CLR 2.0.50727)");

        //    Task<HttpResponseMessage> task = base.SendAsync(request, cancellationToken);
        //    HttpResponseMessage response = task.Result;
        //    MediaTypeHeaderValue contentType = response.Content.Headers.ContentType;
        //    if (string.IsNullOrEmpty(contentType.CharSet))
        //    {
        //        contentType.CharSet = "GBK";
        //    }
        //    return task;
        //}
        #endregion

    }


    public class ResponseResult<T>
    {
        public int Code { get; set; }
        public string Msg { get; set; }
        public T Data { get; set; }
    }

    /// <summary>
    /// Http请求参考类
    /// </summary>
    public class HttpItem
    {
        /// <summary>
        /// 请求URL必须填写
        /// </summary>
        public string URL { get; set; }
        private string _Method = "GET";
        /// <summary>
        /// 请求方式默认为GET方式,当为POST方式时必须设置Postdata的值
        /// </summary>
        public string Method
        {
            get { return _Method; }
            set { _Method = value; }
        }
        private int _Timeout = 300000000;
        private int _DefaultTimeOut = 300000000;
        /// <summary>
        /// 默认请求超时时间
        /// </summary>
        public int Timeout
        {
            get { return _Timeout; }
            set { _Timeout = value * 10000000; }
        }
        public int DefaultTimeOut
        {
            get { return _DefaultTimeOut; }
        }
        int _ReadWriteTimeout = 30000;
        /// <summary>
        /// 默认写入Post数据超时间
        /// </summary>
        public int ReadWriteTimeout
        {
            get { return _ReadWriteTimeout; }
            set { _ReadWriteTimeout = value; }
        }
        private long _MaxResponseContentBufferSizee = 5242880; 
        public long MaxResponseContentBufferSize
        {
            get { return _MaxResponseContentBufferSizee; }
            set { _MaxResponseContentBufferSizee = value; }
        }
        /// <summary>
        /// 设置Host的标头信息
        /// </summary>
        public string Host { get; set; }

        Boolean _KeepAlive = true;
        /// <summary>
        ///  获取或设置一个值，该值指示是否与 Internet 资源建立持久性连接默认为true。
        /// </summary>
        public Boolean KeepAlive
        {
            get { return _KeepAlive; }
            set { _KeepAlive = value; }
        }
        string _Accept = "application/json";
        /// <summary>
        /// 默认值 application/json
        /// 请求标头值 text/html, application/xhtml+xml, */*
        /// </summary>
        public string Accept
        {
            get { return _Accept; }
            set { _Accept = value; }
        }
        string _ContentType = "application/json";
        /// <summary>
        /// 请求返回类型默认 text/html
        /// </summary>
        public string ContentType
        {
            get { return _ContentType; }
            set { _ContentType = value; }
        }

        string _UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
        /// <summary>
        /// 客户端访问信息默认Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)
        /// </summary>
        public string UserAgent
        {
            get { return _UserAgent; }
            set { _UserAgent = value; }
        }
        /// <summary>
        /// 返回数据编码默认为NUll,可以自动识别,一般为utf-8,gbk,gb2312
        /// </summary>
        public Encoding Encoding { get; set; }
        private PostDataType _PostDataType = PostDataType.String;
        /// <summary>
        /// Post的数据类型
        /// </summary>
        public PostDataType PostDataType
        {
            get { return _PostDataType; }
            set { _PostDataType = value; }
        }
        /// <summary>
        /// Post请求时要发送的字符串Post数据
        /// </summary>
        public string Postdata { get; set; }
        /// <summary>
        /// Post请求时要发送的Byte类型的Post数据
        /// </summary>
        public byte[] PostdataByte { get; set; }
        /// <summary>
        /// Cookie对象集合
        /// </summary>
        public CookieCollection CookieCollection { get; set; }
        /// <summary>
        /// 请求时的Cookie
        /// </summary>
        public string Cookie { get; set; }
        /// <summary>
        /// 来源地址，上次访问地址
        /// </summary>
        public string Referer { get; set; }
        /// <summary>
        /// 证书绝对路径
        /// </summary>
        public string CerPath { get; set; }

        public bool Security { get; set; }

        /// <summary>
        /// 设置代理对象，不想使用IE默认配置就设置为Null，而且不要设置ProxyIp
        /// </summary>
        public WebProxy WebProxy { get; set; }

        private Boolean isToLower = false;
        /// <summary>
        /// 是否设置为全文小写，默认为不转化
        /// </summary>
        public Boolean IsToLower
        {
            get { return isToLower; }
            set { isToLower = value; }
        }
        private Boolean allowautoredirect = false;
        /// <summary>
        /// 支持跳转页面，查询结果将是跳转后的页面，默认是不跳转
        /// </summary>
        public Boolean Allowautoredirect
        {
            get { return allowautoredirect; }
            set { allowautoredirect = value; }
        }
        private int connectionlimit = 1024;
        /// <summary>
        /// 最大连接数
        /// </summary>
        public int Connectionlimit
        {
            get { return connectionlimit; }
            set { connectionlimit = value; }
        }
        /// <summary>
        /// 代理Proxy 服务器用户名
        /// </summary>
        public string ProxyUserName { get; set; }
        /// <summary>
        /// 代理Proxy 服务器密码
        /// </summary>
        public string ProxyPwd { get; set; }
        /// <summary>
        /// 代理服务IP地址,如果要使用IE代理就设置为ieproxy
        /// </summary>
        private string proxyip = "ieproxy";
        private int proxyport = 0;

        public string ProxyIp
        {
            get { return proxyip; }
            set
            {
                if (value.Contains(":"))
                {
                    //分割端口号和服务器地址
                    string[] plist = value.Split(':');
                    proxyip = plist[0].Trim();
                    proxyport = Convert.ToInt32(plist[1].Trim());
                }
                else
                {
                    proxyip = value.Trim();
                    proxyport = 0;
                }
            }
        }
        public int ProxyPort
        {
            get { return proxyport; }
            set
            {
                if (value > 0 && value < 65535)
                {
                    proxyport = value;
                }
            }
        }

        private ResultType resulttype = ResultType.String;
        /// <summary>
        /// 设置返回类型String和Byte
        /// </summary>
        public ResultType ResultType
        {
            get { return resulttype; }
            set { resulttype = value; }
        }

        private WebHeaderCollection securityheader = new WebHeaderCollection();

        public WebHeaderCollection SecurityHeaders
        {
            get { return securityheader; }
            set { securityheader = value; }
        }

        private WebHeaderCollection header = new WebHeaderCollection();
        /// <summary>
        /// header对象
        /// </summary>
        public WebHeaderCollection Header
        {
            get { return header; }
            set { header = value; }
        }
        /// <summary>
        //     获取或设置用于请求的 HTTP 版本。返回结果:用于请求的 HTTP 版本。默认为 System.Net.HttpVersion.Version11。
        /// </summary>
        public Version ProtocolVersion { get; set; }
        private Boolean _expect100continue = true;
        /// <summary>
        ///  获取或设置一个 System.Boolean 值，该值确定是否使用 100-Continue 行为。如果 POST 请求需要 100-Continue 响应，则为 true；否则为 false。默认值为 true。
        /// </summary>
        public Boolean Expect100Continue
        {
            get { return _expect100continue; }
            set { _expect100continue = value; }
        }
        /// <summary>
        /// 设置509证书集合
        /// </summary>
        public X509CertificateCollection ClentCertificates { get; set; }
        /// <summary>
        /// 设置或获取Post参数编码,默认的为Default编码
        /// </summary>
        public Encoding PostEncoding { get; set; }
        private ResultCookieType _ResultCookieType = ResultCookieType.String;
        /// <summary>
        /// Cookie返回类型,默认的是只返回字符串类型
        /// </summary>
        public ResultCookieType ResultCookieType
        {
            get { return _ResultCookieType; }
            set { _ResultCookieType = value; }
        }
        private ICredentials _ICredentials = CredentialCache.DefaultCredentials;
        /// <summary>
        /// 获取或设置请求的身份验证信息。
        /// </summary>
        public ICredentials ICredentials
        {
            get { return _ICredentials; }
            set { _ICredentials = value; }
        }

        public HttpItem()
        {
            Security = true;
        }
    }

    /// <summary>
    /// Http返回参数类
    /// </summary>
    public class HttpResult
    {
        /// <summary>
        /// Http请求返回的Cookie
        /// </summary>
        public string Cookie { get; set; }
        /// <summary>
        /// Cookie对象集合
        /// </summary>
        public CookieCollection CookieCollection { get; set; }
        /// <summary>
        /// 返回的String类型数据 只有ResultType.String时才返回数据，其它情况为空
        /// </summary>
        public string Html { get; set; }
        /// <summary>
        /// 返回的Byte数组 只有ResultType.Byte时才返回数据，其它情况为空
        /// </summary>
        public byte[] ResultByte { get; set; }
        /// <summary>
        /// header对象
        /// </summary>
        public WebHeaderCollection Header { get; set; }
        /// <summary>
        /// 返回状态说明
        /// </summary>
        public string StatusDescription { get; set; }
        /// <summary>
        /// 返回状态码,默认为OK
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
    }

    /// <summary>
    /// 返回类型
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// 表示只返回字符串 只有Html有数据
        /// </summary>
        String,
        /// <summary>
        /// 表示返回字符串和字节流 ResultByte和Html都有数据返回
        /// </summary>
        Byte
    }

    /// <summary>
    /// Post的数据格式默认为string
    /// </summary>
    public enum PostDataType
    {
        /// <summary>
        /// 字符串类型，这时编码Encoding可不设置
        /// </summary>
        String,
        /// <summary>
        /// Byte类型，需要设置PostdataByte参数的值编码Encoding可设置为空
        /// </summary>
        Byte,
        /// <summary>
        /// 传文件，Postdata必须设置为文件的绝对路径，必须设置Encoding的值
        /// </summary>
        FilePath
    }

    /// <summary>
    /// Cookie返回类型
    /// </summary>
    public enum ResultCookieType
    {
        /// <summary>
        /// 只返回字符串类型的Cookie
        /// </summary>
        String,
        /// <summary>
        /// CookieCollection格式的Cookie集合同时也返回String类型的cookie
        /// </summary>
        CookieCollection
    }

    public enum RequestMethodType
    {

    }

    public enum ResponseResultType
    {
        /// <summary>
        /// 客服端使用的ResultInfo封装结果
        /// {Code:string,Msg:string,Data:T}
        /// </summary>
        ResultInfo = 0,
        /// <summary>
        /// 标准的HttpResponseMessage
        /// </summary>
        HttpResponseMessage=1
    }
}
