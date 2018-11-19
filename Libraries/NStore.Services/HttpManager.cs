using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NStore.Services
{
    public class HttpManager
    {
        private static Dictionary<string, HttpHelper> dicHelpers = new Dictionary<string, HttpHelper>();
        private static object objLock = new object();
        public static HttpHelper GetHttpHelper(string baseAddress, string id="", HttpItem httpItem= null)
        {
            lock (objLock)
            {
                if(id=="")
                {
                    id = baseAddress;
                }
                if (dicHelpers.ContainsKey(id))
                {
                    return dicHelpers[id];
                }
                else
                {
                    HttpHelper httpHelper = new HttpHelper();

                    if (httpItem != null)
                    {
                        httpHelper.SetHttpItem = httpItem;
                    }
                    //设置Host地址
                    httpHelper.SetHttpItem.Host = baseAddress;

                    httpHelper.SetHttpClient();

                    dicHelpers.Add(id, httpHelper);

                    return httpHelper;
                }
            }
        }
    }
}
