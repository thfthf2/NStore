using System;
using System.Collections.Generic;

using NStore.Core;

namespace NStore.Services
{
    /// <summary>
    /// 广告操作管理类
    /// </summary>
    public partial class Adverts
    {
        private static HttpHelper httpHelper = Commons.HttpHelper;

        /// <summary>
        /// 获得广告位置列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        public static List<AdvertPositionInfo> GetAdvertPositionList(int pageSize, int pageNumber)
        {
            if (Commons.DirectConnected)
            {
                return NStore.Data.Adverts.GetAdvertPositionList(pageSize, pageNumber);
            }
            else
            {
                var result = httpHelper.HttpGet<List<AdvertPositionInfo>>(string.Format("api/GetAdvertPositionList/{0}/{1}", pageSize, pageNumber));
                if (result.Code == 0)
                {
                    return result.Data;
                }
                else
                {
                    Log4NetHelper.Error(string.Format("GetAdvertPositionList请求失败，错误信息：{0}", result.Msg));
                    return null;
                }
            }         
        }

        /// <summary>
        /// 获得广告位置数量
        /// </summary>
        /// <returns></returns>
        public static int GetAdvertPositionCount()
        {
            if (Commons.DirectConnected)
            {
                return NStore.Data.Adverts.GetAdvertPositionCount();
            }
            else
            {
                var result = httpHelper.HttpGet<int>("api/GetAdvertPositionCount");
                if (result.Code == 0)
                {
                    return result.Data;
                }
                else
                {
                    Log4NetHelper.Error(string.Format("GetAdvertPositionCount请求失败，错误信息：{0}", result.Msg));
                    return 0;
                }
            }        
        }

        /// <summary>
        /// 获得全部广告位置
        /// </summary>
        /// <returns></returns>
        public static List<AdvertPositionInfo> GetAllAdvertPosition()
        {
            if (Commons.DirectConnected)
            {
                return NStore.Data.Adverts.GetAllAdvertPosition();
            }
            else
            {
                var result = httpHelper.HttpGet<List<AdvertPositionInfo>>("api/GetAllAdvertPosition");
                if (result.Code == 0)
                {
                    return result.Data;
                }
                else
                {
                    Log4NetHelper.Error(string.Format("GetAllAdvertPosition请求失败，错误信息：{0}", result.Msg));
                    return null;
                }
            }
        }

        /// <summary>
        /// 获得广告位置
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        /// <returns></returns>
        public static AdvertPositionInfo GetAdvertPositionById(int adPosId)
        {
            if (adPosId <= 0)
            {
                return null;
            }
            if (Commons.DirectConnected)
            {
                return NStore.Data.Adverts.GetAdvertPositionById(adPosId);
            }
            else
            {
                var result = httpHelper.HttpGet<AdvertPositionInfo>(string.Format("api/GetAdvertPositionById/{0}", adPosId));
                if (result.Code == 0)
                {
                    return result.Data;
                }
                else
                {
                    Log4NetHelper.Error(string.Format("GetAdvertPositionById请求失败，错误信息：{0}", result.Msg));
                    return null;
                }
            }
        }

        /// <summary>
        /// 获得广告列表
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        /// <returns></returns>
        public static List<AdvertInfo> GetAdvertList(int adPosId)
        {
            List<AdvertInfo> advertList = NStore.Core.BMACache.Get(CacheKeys.MALL_ADVERT_LIST + adPosId) as List<AdvertInfo>;
            if (advertList == null)
            {
                if (Commons.DirectConnected)
                {
                    advertList = NStore.Data.Adverts.GetAdvertList(adPosId, DateTime.Now);
                }
                else
                {
                    var result = httpHelper.HttpGet<List<AdvertInfo>>(string.Format("api/GetAdvertList/{0}", adPosId));
                    if (result.Code == 0)
                    {
                        advertList = result.Data;
                    }
                    else
                    {
                        Log4NetHelper.Error(string.Format("GetAdvertList请求失败，错误信息：{0}", result.Msg));
                    }
                }
                NStore.Core.BMACache.Insert(CacheKeys.MALL_ADVERT_LIST + adPosId, advertList);
            }
            return advertList;
        }
    }
}
