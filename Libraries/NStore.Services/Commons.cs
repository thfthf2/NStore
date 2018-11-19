using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStore.Services
{
    public static class Commons
    {
        public static string BaseAddr { get; set; }

        public static bool DirectConnected { get; set; }

        public static HttpHelper HttpHelper { get; set; }

        static Commons()
        {
            BaseAddr = "http://localhost:2096";
            DirectConnected = true;
            HttpHelper= HttpManager.GetHttpHelper(BaseAddr);
        }
    }
}