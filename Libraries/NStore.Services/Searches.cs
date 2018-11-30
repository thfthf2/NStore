using System;
using System.Collections.Generic;

using NStore.Core;

namespace NStore.Services
{
    /// <summary>
    /// 搜索操作管理类
    /// </summary>
    public partial class Searches
    {
        private static ISearchStrategy _isearchstrategy = BMASearch.Instance;//搜索策略

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
        public static List<StoreProductInfo> SearchMallProducts(int pageSize, int pageNumber, string keyword, int cateId, int brandId, int filterPrice, string[] catePriceRangeList, List<int> attrValueIdList, int onlyStock, int sortColumn, int sortDirection)
        {
            return _isearchstrategy.SearchMallProducts(pageSize, pageNumber, keyword, cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock, sortColumn, sortDirection);
        }
        
        /// <summary>
        /// 搜索商城商品
        /// </summary>
        /// <param name="ids">商品ids</param>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static List<StoreProductInfo> SearchMallProducts(List<int> ids, int pageSize, int pageNumber, int sortColumn, int sortDirection)
        {
            return _isearchstrategy.SearchMallProducts(ids, pageSize, pageNumber, sortColumn, sortDirection);
        }

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
        public static int GetSearchMallProductCount(string keyword, int cateId, int brandId, int filterPrice, string[] catePriceRangeList, List<int> attrValueIdList, int onlyStock)
        {
            return _isearchstrategy.GetSearchMallProductCount(keyword, cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock);
        }

        /// <summary>
        /// 获得搜索商城商品数量
        /// </summary>
        /// <param name="ids">商品ids</param>
        /// <returns></returns>
        public static int GetSearchMallProductCount(List<int> ids)
        {
            return _isearchstrategy.GetSearchMallProductCount(ids);
        }

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
        public static List<PartProductInfo> SearchStoreProducts(int pageSize, int pageNumber, string keyword, int storeId, int storeCid, int startPrice, int endPrice, int sortColumn, int sortDirection)
        {
            return _isearchstrategy.SearchStoreProducts(pageSize, pageNumber, keyword, storeId, storeCid, startPrice, endPrice, sortColumn, sortDirection);
        }

        /// <summary>
        /// 获得搜索店铺商品数量
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <param name="storeId">店铺id</param>
        /// <param name="storeCid">店铺分类id</param>
        /// <param name="startPrice">开始价格</param>
        /// <param name="endPrice">结束价格</param>
        /// <returns></returns>
        public static int GetSearchStoreProductCount(string keyword, int storeId, int storeCid, int startPrice, int endPrice)
        {
            return _isearchstrategy.GetSearchStoreProductCount(keyword, storeId, storeCid, startPrice, endPrice);
        }

        /// <summary>
        /// 获得分类列表
        /// </summary>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public static List<CategoryInfo> GetCategoryListByKeyword(string keyword)
        {
            return _isearchstrategy.GetCategoryListByKeyword(keyword);
        }

        /// <summary>
        /// 获得分类品牌列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public static List<BrandInfo> GetCategoryBrandListByKeyword(int cateId, string keyword)
        {
            return _isearchstrategy.GetCategoryBrandListByKeyword(cateId, keyword);
        }

        /// <summary>
        /// 根据商品搜索词获取匹配商品关联对象列表
        /// </summary>
        /// <param name="name">搜索词</param>
        /// <returns></returns>
        public static ProductSearchKeyInfo GetProductSearchKey(string name)
        {
            return _isearchstrategy.GetProductSearchKey(name);
        }

        /// <summary>
        /// 根据专场id获取商品关联列表
        /// </summary>
        /// <param name="specialId">专场id</param>
        /// <returns></returns>
        public static List<ProductSpecialInfo> GetProductIdListBySpecialId(int specialId, int cateId, int brandId)
        {
            return _isearchstrategy.GetProductIdListBySpecialId(specialId, cateId, brandId);
        }

        /// <summary>
        /// 根据关键字id获取商品关联列表
        /// </summary>
        /// <param name="keyId">关键字id</param>
        /// <returns></returns>
        public static List<ProductKeywordInfo> GetProductIdListByKeyId(int keyId, int cateId, int brandId)
        {
            return _isearchstrategy.GetProductIdListByKeyId(keyId, cateId, brandId);
        }

        /// <summary>
        /// 根据属性id或属性值id获取商品关联列表
        /// </summary>
        /// <param name="attrId">属性id</param>
        /// <param name="attrValueId">属性值id</param>
        /// <returns></returns>
        public static List<ProductAttributeInfo> GetProductIdListByAttrId(int attrId, int attrValueId, int cateId, int brandId)
        {
            return _isearchstrategy.GetProductIdListByAttrId(attrId, attrValueId, cateId, brandId);
        }

        /// <summary>
        /// 获得分类商品列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public static List<StoreProductInfo> GetCategoryProductList(int pageSize, int pageNumber, int cateId)
        {
            return _isearchstrategy.GetCategoryProductList(pageSize, pageNumber, cateId);
        }

    }
}
