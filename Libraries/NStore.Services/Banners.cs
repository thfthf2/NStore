using System;

using NStore.Core;

namespace NStore.Services
{
    /// <summary>
    /// banner操作管理类
    /// </summary>
    public partial class Banners
    {
        /// <summary>
        /// 获得首页banner列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static BannerInfo[] GetHomeBannerList(int type)
        {
            BannerInfo[] bannerList = NStore.Core.BMACache.Get(CacheKeys.MALL_BANNER_HOMELIST + type) as BannerInfo[];
            if (bannerList == null)
            {
                bannerList = NStore.Data.Banners.GetHomeBannerList(type, DateTime.Now);
                NStore.Core.BMACache.Insert(CacheKeys.MALL_BANNER_HOMELIST + type, bannerList);
            }
            return bannerList;
        }
    }
}
