using System;
using System.Data;

using NStore.Core;

namespace NStore.Services
{
    public partial class AdminBannedIPs : BannedIPs
    {
        /// <summary>
        /// 添加禁止的ip
        /// </summary>
        public static void AddBannedIP(BannedIPInfo bannedIPInfo)
        {
            NStore.Data.BannedIPs.AddBannedIP(bannedIPInfo);
            NStore.Core.BMACache.Remove(CacheKeys.MALL_BANNEDIP_HASHSET);
        }

        /// <summary>
        /// 更新禁止的ip
        /// </summary>
        public static void UpdateBannedIP(BannedIPInfo bannedIPInfo)
        {
            NStore.Data.BannedIPs.UpdateBannedIP(bannedIPInfo);
            NStore.Core.BMACache.Remove(CacheKeys.MALL_BANNEDIP_HASHSET);
        }

        /// <summary>
        /// 删除禁止的ip
        /// </summary>
        /// <param name="idList">id列表</param>
        public static void DeleteBannedIPById(int[] idList)
        {
            if (idList != null && idList.Length > 0)
            {
                NStore.Data.BannedIPs.DeleteBannedIPById(CommonHelper.IntArrayToString(idList));
                NStore.Core.BMACache.Remove(CacheKeys.MALL_BANNEDIP_HASHSET);
            }
        }

        /// <summary>
        /// 后台获得禁止的ip列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="ip">ip</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetBannedIPList(int pageSize, int pageNumber, string ip, string sort)
        {
            return NStore.Data.BannedIPs.AdminGetBannedIPList(pageSize, pageNumber, ip, sort);
        }

        /// <summary>
        /// 后台获得禁止的ip列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetBannedIPListSort(string sortColumn, string sortDirection)
        {
            return NStore.Data.BannedIPs.AdminGetBannedIPListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得禁止的ip数量
        /// </summary>
        /// <param name="ip">ip</param>
        /// <returns></returns>
        public static int AdminGetBannedIPCount(string ip)
        {
            return NStore.Data.BannedIPs.AdminGetBannedIPCount(ip);
        }
    }
}
