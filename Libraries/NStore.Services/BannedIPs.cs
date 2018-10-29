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
        /// <summary>
        /// 获得禁止的ip列表
        /// </summary>
        /// <returns></returns>
        public static HashSet<string> GetBannedIPList()
        {
            HashSet<string> ipList = NStore.Core.BMACache.Get(CacheKeys.MALL_BANNEDIP_HASHSET) as HashSet<string>;
            if (ipList == null)
            {
                ipList = NStore.Data.BannedIPs.GetBannedIPList();
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
            if (ipList.Count > 0 && ip.Length > 0)
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
            return NStore.Data.BannedIPs.GetBannedIPById(id);
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
            return NStore.Data.BannedIPs.GetBannedIPIdByIP(ip);
        }
    }
}
