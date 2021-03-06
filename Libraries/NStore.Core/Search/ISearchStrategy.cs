﻿using System;
using System.Collections.Generic;

namespace NStore.Core
{
    /// <summary>
    /// 搜索策略接口
    /// </summary>
    public partial interface ISearchStrategy
    {
        /// <summary>
        /// 搜索商城商品
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="keyword">关键词</param>
        /// <param name="cateId">分类id</param>
        /// <param name="brandId">品牌id</param>
        /// <param name="filterPrice">筛选价格</param>
        /// <param name="catePriceRangeList">分类价格范围列表</param>
        /// <param name="attrValueIdList">属性值id列表</param>
        /// <param name="onlyStock">是否只显示有货</param>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        List<StoreProductInfo> SearchMallProducts(int pageSize, int pageNumber, string keyword, int cateId, int brandId, int filterPrice, string[] catePriceRangeList, List<int> attrValueIdList, int onlyStock, int sortColumn, int sortDirection);

        /// <summary>
        /// 搜索商城商品
        /// </summary>
        /// <param name="ids">商品ids</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        List<StoreProductInfo> SearchMallProducts(List<int> ids, int pageSize, int pageNumber, int sortColumn, int sortDirection);

        /// <summary>
        /// 获得搜索商城商品数量
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <param name="cateId">分类id</param>
        /// <param name="brandId">品牌id</param>
        /// <param name="filterPrice">筛选价格</param>
        /// <param name="catePriceRangeList">分类价格范围列表</param>
        /// <param name="attrValueIdList">属性值id列表</param>
        /// <param name="onlyStock">是否只显示有货</param>
        /// <returns></returns>
        int GetSearchMallProductCount(string keyword, int cateId, int brandId, int filterPrice, string[] catePriceRangeList, List<int> attrValueIdList, int onlyStock);

        /// <summary>
        /// 获得搜索商城商品数量
        /// </summary>
        /// <param name="ids">商品ids</param>
        /// <returns></returns>
        int GetSearchMallProductCount(List<int> ids);

        /// <summary>
        /// 搜索店铺商品
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="keyword">关键词</param>
        /// <param name="storeId">店铺id</param>
        /// <param name="storeCid">店铺分类id</param>
        /// <param name="startPrice">开始价格</param>
        /// <param name="endPrice">结束价格</param>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        List<PartProductInfo> SearchStoreProducts(int pageSize, int pageNumber, string keyword, int storeId, int storeCid, int startPrice, int endPrice, int sortColumn, int sortDirection);

        /// <summary>
        /// 获得搜索店铺商品数量
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <param name="storeId">店铺id</param>
        /// <param name="storeCid">店铺分类id</param>
        /// <param name="startPrice">开始价格</param>
        /// <param name="endPrice">结束价格</param>
        /// <returns></returns>
        int GetSearchStoreProductCount(string keyword, int storeId, int storeCid, int startPrice, int endPrice);

        /// <summary>
        /// 获得分类列表
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        List<CategoryInfo> GetCategoryListByKeyword(string keyword);

        /// <summary>
        /// 获得分类品牌列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        List<BrandInfo> GetCategoryBrandListByKeyword(int cateId, string keyword);

        /// <summary>
        /// 根据搜索词获取商品匹配关联词
        /// </summary>
        /// <param name="name">搜索词</param>
        /// <returns></returns>
        ProductSearchKeyInfo GetProductSearchKey(string name);

        /// <summary>
        /// 根据专场id获取商品关联列表
        /// </summary>
        /// <param name="specialId">专场id</param>
        /// <returns></returns>
        List<ProductSpecialInfo> GetProductIdListBySpecialId(int specialId, int cateId, int brandId);

        /// <summary>
        /// 根据关键字id获取商品关联列表
        /// </summary>
        /// <param name="keyId">关键字id</param>
        /// <returns></returns>
        List<ProductKeywordInfo> GetProductIdListByKeyId(int keyId, int cateId, int brandId);

        /// <summary>
        /// 根据属性id或属性值id获取商品关联列表
        /// </summary>
        /// <param name="attrId">属性id</param>
        /// <param name="attrValueId">属性值id</param>
        /// <returns></returns>
        List<ProductAttributeInfo> GetProductIdListByAttrId(int attrId, int attrValueId, int cateId, int brandId);
        
        /// <summary>
        /// 获得分类商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="cateId">分类id</param>
         List<StoreProductInfo> GetCategoryProductList(int pageSize, int pageNumber, int cateId);
    }
}
