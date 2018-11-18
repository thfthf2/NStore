using System;
using System.Collections.Generic;

using NStore.Core;

namespace NStore.Services
{
    /// <summary>
    /// 禁止IP操作管理类
    /// </summary>
    public partial class BannedIPs
    {
        private static HttpHelper httpHelper = Commons.HttpHelper;

        /// <summary>
        /// 获得禁止的ip列表
        /// </summary>
        /// <returns></returns>
        public static HashSet<string> GetBannedIPList()
        {
            HashSet<string> ipList = NStore.Core.BMACache.Get(CacheKeys.MALL_BANNEDIP_HASHSET) as HashSet<string>;
            if (ipList == null)
            {
                if (Commons.DirectConnected)
                {
                    ipList = NStore.Data.BannedIPs.GetBannedIPList();
                }
                else
                {
                    var result = httpHelper.HttpGet<HashSet<string>>("api/GetBannedIPList");
                    if (result.Code == 0)
                    {
                        ipList = result.Data;
                    }
                    else
                    {
                        Log4NetHelper.Error(string.Format("GetBannedIPList请求失败，错误信息：{0}", result.Msg));
                        ipList = null;
                    }
                }
                NStore.Core.BMACache.Insert(CacheKeys.MALL_BANNEDIP_HASHSET, ipList);
            }
            return ipList;
        }

        /// <summary>
        /// 检查IP是否禁止
        /// </summary>
        /// <param name="ip">ip</param>
        /// <returns></returns>
        public static bool CheckIP(string ip)
        {
            HashSet<string> ipList = GetBannedIPList();
            if (ipList != null && ipList.Count > 0 && ip.Length > 0)
            {
                if (ipList.Contains(ip))
                    return true;
                if (ipList.Contains(StringHelper.SubString(ip, ip.LastIndexOf('.'))))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 获得禁止的ip
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public static BannedIPInfo GetBannedIPById(int id)
        {
            if (Commons.DirectConnected)
            {
                return NStore.Data.BannedIPs.GetBannedIPById(id);
            }
            else
            {
                var result = httpHelper.HttpGet<BannedIPInfo>(string.Format("api/GetBannedIPById/{0}", id));
                if (result.Code == 0)
                {
                    return result.Data;
                }
                else
                {
                    Log4NetHelper.Error(string.Format("GetBannedIPById请求失败，错误信息：{0}", result.Msg));
                    return null;
                }
            }
        }

        /// <summary>
        /// 获得禁止IP的id
        /// </summary>
        /// <param name="ip">ip</param>
        /// <returns></returns>
        public static int GetBannedIPIdByIP(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
                return 0;
            if (Commons.DirectConnected)
            {
                return NStore.Data.BannedIPs.GetBannedIPIdByIP(ip);
            }
            else
            {
                var result = httpHelper.HttpGet<int>(string.Format("api/GetBannedIPIdByIP/{0}", ip));
                if (result.Code == 0)
                {
                    return result.Data;
                }
                else
                {
                    Log4NetHelper.Error(string.Format("GetBannedIPIdByIP，错误信息：{0}", result.Msg));
                    return 0;
                }
            }
        }
    }
}
