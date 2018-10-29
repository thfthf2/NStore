using System;
using System.Data;
using System.Collections.Generic;

using NStore.Core;

namespace NStore.Services
{
    /// <summary>
    /// 后台商品评价操作管理类
    /// </summary>
    public partial class AdminProductReviews : ProductReviews
    {
        /// <summary>
        /// 后台获得商品评价
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <returns></returns>
        public static ProductReviewInfo AdminGetProductReviewById(int reviewId)
        {
            return NStore.Data.ProductReviews.AdminGetProductReviewById(reviewId);
        }

        /// <summary>
        /// 删除商品评价
        /// </summary>
        /// <param name="reviewId">评价id</param>
        public static void DeleteProductReviewById(int reviewId)
        {
            if (reviewId > 0)
                NStore.Data.ProductReviews.DeleteProductReviewById(reviewId);
        }

        /// <summary>
        /// 后台获得商品评价列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetProductReviewList(int pageSize, int pageNumber, string condition, string sort)
        {
            return NStore.Data.ProductReviews.AdminGetProductReviewList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得商品评价列表条件
        /// </summary>
        /// <param name="storeId">店铺id</param>
        /// <param name="pid">商品id</param>
        /// <param name="message">评价内容</param>
        /// <param name="startTime">评价开始时间</param>
        /// <param name="endTime">评价结束时间</param>
        /// <returns></returns>
        public static string AdminGetProductReviewListCondition(int storeId, int pid, string message, string startTime, string endTime)
        {
            return NStore.Data.ProductReviews.AdminGetProductReviewListCondition(storeId, pid, message, startTime, endTime);
        }

        /// <summary>
        /// 后台获得商品评价列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetProductReviewListSort(string sortColumn, string sortDirection)
        {
            return NStore.Data.ProductReviews.AdminGetProductReviewListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得商品评价数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetProductReviewCount(string condition)
        {
            return NStore.Data.ProductReviews.AdminGetProductReviewCount(condition);
        }

        /// <summary>
        /// 后台获得商品评价回复列表
        /// </summary>
        /// <param name="reviewId">评价id</param>
        /// <returns></returns>
        public static DataTable AdminGetProductReviewReplyList(int reviewId)
        {
            return NStore.Data.ProductReviews.AdminGetProductReviewReplyList(reviewId);
        }
    }
}
