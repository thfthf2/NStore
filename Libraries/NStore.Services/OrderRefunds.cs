using System;

using NStore.Core;

namespace NStore.Services
{
    /// <summary>
    /// 订单退款操作管理类
    /// </summary>
    public partial class OrderRefunds
    {
        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="orderRefundInfo">订单退款信息</param>
        public static void ApplyRefund(OrderRefundInfo orderRefundInfo)
        {
            NStore.Data.OrderRefunds.ApplyRefund(orderRefundInfo);
        }
    }
}
