using System;

using NStore.Core;

namespace NStore.Services
{
    /// <summary>
    /// banner操作管理类
    /// </summary>
    public partial class Banners
    {
        private static HttpHelper httpHelper = Commons.HttpHelper;

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
                if (Commons.DirectConnected)
                {
                    bannerList = NStore.Data.Banners.GetHomeBannerList(type, DateTime.Now);
                }
                else
                {
                    var result = httpHelper.HttpGet<BannerInfo[]>(string.Format("api/GetHomeBannerList/{0}", type));
                    if(result.Code == 0)
                    {
                        bannerList = result.Data;
                    }
                    else
                    {
                        Log4NetHelper.Error(string.Format("GetHomeBannerList请求失败，错误信息：{0}", result.Msg));
                    }
                }
                NStore.Core.BMACache.Insert(CacheKeys.MALL_BANNER_HOMELIST + type, bannerList);
            }
            return bannerList;
        }
    }
}
