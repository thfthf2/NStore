﻿using System;
using System.Text;
using System.Data;
using System.Collections.Generic;

using NStore.Core;

namespace NStore.Services
{
    /// <summary>
    /// 品牌操作管理类
    /// </summary>
    public partial class Brands
    {
        /// <summary>
        /// 获得品牌
        /// </summary>
        /// <param name="brandId">品牌id</param>
        /// <returns></returns>
        public static BrandInfo GetBrandById(int brandId)
        {
            BrandInfo brandInfo = NStore.Core.BMACache.Get(CacheKeys.MALL_BRAND_INFO + brandId) as BrandInfo;
            if (brandInfo == null)
            {
                brandInfo = NStore.Data.Brands.GetBrandById(brandId);
                NStore.Core.BMACache.Insert(CacheKeys.MALL_BRAND_INFO + brandId, brandInfo);
            }

            return brandInfo;
        }

        /// <summary>
        /// 根据品牌名称得到品牌id
        /// </summary>
        /// <param name="brandName">品牌名称</param>
        /// <returns></returns>
        public static int GetBrandIdByName(string brandName)
        {
            if (string.IsNullOrWhiteSpace(brandName))
                return 0;
            return NStore.Data.Brands.GetBrandIdByName(brandName);
        }

        /// <summary>
        /// 获得品牌关联的分类
        /// </summary>
        /// <param name="brandId">品牌id</param>
        /// <returns></returns>
        public static List<CategoryInfo> GetBrandCategoryList(int brandId)
        {
            List<CategoryInfo> categoryList = NStore.Core.BMACache.Get(CacheKeys.MALL_BRAND_CATEGORYLIST + brandId) as List<CategoryInfo>;
            if (categoryList == null)
            {
                categoryList = NStore.Data.Brands.GetBrandCategoryList(brandId);
                NStore.Core.BMACache.Insert(CacheKeys.MALL_BRAND_CATEGORYLIST + brandId, categoryList);
            }

            return categoryList;
        }

        /// <summary>
        /// 获得品牌列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="brandName">品牌名称</param>
        /// <returns></returns>
        public static List<BrandInfo> GetBrandList(int pageSize, int pageNumber, string brandName)
        {
            return NStore.Data.Brands.GetBrandList(pageSize, pageNumber, brandName);
        }

        /// <summary>
        /// 根据品牌id获得品牌列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="brandName">品牌名称</param>
        /// <returns></returns>
        public static List<BrandInfo> GetBrandListByIds(List<int> ids)
        {
            var brandList = GetBrandList(1000, 1, "");
            if (brandList == null)
            {
                return new List<BrandInfo>();
            }
            return brandList.FindAll(p => ids.Contains(p.BrandId));
        }

        /// <summary>
        /// 获得品牌数量
        /// </summary>
        /// <param name="brandName">品牌名称</param>
        /// <returns></returns>
        public static int GetBrandCount(string brandName)
        {
            return NStore.Data.Brands.GetBrandCount(brandName);
        }
    }
}
