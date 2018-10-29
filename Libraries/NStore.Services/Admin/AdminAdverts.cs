using System;
using System.Data;
using System.Collections.Generic;

using NStore.Core;

namespace NStore.Services
{
    /// <summary>
    /// 后台广告操作管理类
    /// </summary>
    public partial class AdminAdverts : Adverts
    {
        /// <summary>
        /// 创建广告位置
        /// </summary>
        public static void CreateAdvertPosition(AdvertPositionInfo advertPositionInfo)
        {
            NStore.Data.Adverts.CreateAdvertPosition(advertPositionInfo);
        }

        /// <summary>
        /// 更新广告位置
        /// </summary>
        public static void UpdateAdvertPosition(AdvertPositionInfo advertPositionInfo)
        {
            NStore.Data.Adverts.UpdateAdvertPosition(advertPositionInfo);
        }

        /// <summary>
        /// 删除广告位置
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        public static void DeleteAdvertPositionById(int adPosId)
        {
            NStore.Data.Adverts.DeleteAdvertPositionById(adPosId);
            NStore.Core.BMACache.Remove(CacheKeys.MALL_ADVERT_LIST + adPosId);

        }




        /// <summary>
        /// 创建广告
        /// </summary>
        public static void CreateAdvert(AdvertInfo advertInfo)
        {
            NStore.Data.Adverts.CreateAdvert(advertInfo);
            NStore.Core.BMACache.Remove(CacheKeys.MALL_ADVERT_LIST + advertInfo.AdPosId);
        }

        /// <summary>
        /// 更新广告
        /// </summary>
        public static void UpdateAdvert(int oldAdPosId, AdvertInfo advertInfo)
        {
            NStore.Data.Adverts.UpdateAdvert(advertInfo);
            if (oldAdPosId == advertInfo.AdPosId)
            {
                NStore.Core.BMACache.Remove(CacheKeys.MALL_ADVERT_LIST + advertInfo.AdPosId);
            }
            else
            {
                NStore.Core.BMACache.Remove(CacheKeys.MALL_ADVERT_LIST + oldAdPosId);
                NStore.Core.BMACache.Remove(CacheKeys.MALL_ADVERT_LIST + advertInfo.AdPosId);
            }
        }

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="adId">广告id</param>
        public static void DeleteAdvertById(int adId)
        {
            AdvertInfo advertInfo = AdminGetAdvertById(adId);
            if (advertInfo != null)
            {
                NStore.Data.Adverts.DeleteAdvertById(adId);
                NStore.Core.BMACache.Remove(CacheKeys.MALL_ADVERT_LIST + advertInfo.AdPosId);
            }
        }

        /// <summary>
        /// 后台获得广告
        /// </summary>
        /// <param name="adId">广告id</param>
        /// <returns></returns>
        public static AdvertInfo AdminGetAdvertById(int adId)
        {
            return NStore.Data.Adverts.AdminGetAdvertById(adId);
        }

        /// <summary>
        /// 后台获得广告列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="adPosId">广告位置id</param>
        /// <returns></returns>
        public static DataTable AdminGetAdvertList(int pageSize, int pageNumber, int adPosId)
        {
            return NStore.Data.Adverts.AdminGetAdvertList(pageSize, pageNumber, adPosId);
        }

        /// <summary>
        /// 后台获得广告数量
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        /// <returns></returns>
        public static int AdminGetAdvertCount(int adPosId)
        {
            return NStore.Data.Adverts.AdminGetAdvertCount(adPosId);
        }
    }
}
