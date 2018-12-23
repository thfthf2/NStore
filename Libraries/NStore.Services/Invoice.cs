using NStore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStore.Services
{
    public partial class Invoice
    {
        /// <summary>
        /// 获得发票信息列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static List<InvoiceInfo> GetInvoiceList(int uid)
        {
            return NStore.Data.Invoice.GetInvoiceList(uid);
        }


        /// <summary>
        /// 获得用户发票信息数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static int GetInvoiceCount(int uid)
        {
            return NStore.Data.Invoice.GetInvoiceCount(uid);
        }

        /// <summary>
        /// 创建用户发票信息
        /// </summary>
        public static int CreateInvoice(InvoiceInfo invoiceInfo)
        {
            return NStore.Data.Invoice.CreateInvoice(invoiceInfo);
        }

        /// <summary>
        /// 获得用户发票信息
        /// </summary>
        /// <param name="invoiceId">发票id</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static InvoiceInfo GetInvoicById(int invoiceId, int uid)
        {
            if (invoiceId < 1)
                return null;

            InvoiceInfo ivoiceInfo = NStore.Data.Invoice.GetInvoicById(invoiceId, uid);
            if (ivoiceInfo == null || ivoiceInfo.Uid != uid)
                return null;
            else
                return ivoiceInfo;
        }

        /// <summary>
        /// 更新用户发票信息
        /// </summary>
        public static void UpdateInvoic(InvoiceInfo ivoiceInfo)
        {
            NStore.Data.Invoice.UpdateInvoic(ivoiceInfo);
        }

        /// <summary>
        /// 删除用户发票信息
        /// </summary>
        /// <param name="invoiceId">发票id</param>
        /// <param name="uid">用户id</param>
        public static bool DeleteInvoic(int invoiceId, int uid)
        {
            if (invoiceId < 1)
                return false;
            return NStore.Data.Invoice.DeleteInvoic(invoiceId, uid);
        }

        /// <summary>
        /// 更新用户发票信息的默认状态
        /// </summary>
        /// <param name="invoiceId">发票id</param>
        /// <param name="uid">用户id</param>
        /// <param name="isDefault">状态</param>
        /// <returns></returns>
        public static bool UpdateInvoicIsDefault(int invoiceId, int uid, int isDefault)
        {
            if (invoiceId < 1)
                return false;

            if (isDefault != 0) isDefault = 1;
            return NStore.Data.Invoice.UpdateInvoicIsDefault(invoiceId, uid, isDefault);
        }
    }
}
