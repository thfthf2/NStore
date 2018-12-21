using NStore.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStore.Data
{
    public class Invoice
    {
        private static IUserNOSQLStrategy _usernosql = BMAData.UserNOSQL;//用户非关系型数据库

        /// <summary>
        /// 获得发票信息列表
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static List<InvoiceInfo> GetInvoiceList(int uid)
        {
            List<InvoiceInfo> invoiceList = null;

            if (_usernosql != null)
            {
                invoiceList = _usernosql.GetInvoiceList(uid);
                if (invoiceList == null)
                {
                    invoiceList = new List<InvoiceInfo>();
                    IDataReader reader = NStore.Core.BMAData.RDBS.GetInvoiceList(uid);
                    while (reader.Read())
                    {
                        InvoiceInfo invoiceInfo = BuildInvoiceInfoFromReader(reader);
                        invoiceList.Add(invoiceInfo);
                    }
                    reader.Close();
                    _usernosql.CreateInvoiceList(uid, invoiceList);
                }
            }
            else
            {
                invoiceList = new List<InvoiceInfo>();
                IDataReader reader = NStore.Core.BMAData.RDBS.GetInvoiceList(uid);
                while (reader.Read())
                {
                    InvoiceInfo invoiceInfo = BuildInvoiceInfoFromReader(reader);
                    invoiceList.Add(invoiceInfo);
                }
                reader.Close();
            }

            return invoiceList;
        }

        /// <summary>
        /// 获得用户发票信息数量
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static int GetInvoiceCount(int uid)
        {
            if (_usernosql != null)
                return GetInvoiceList(uid).Count;
            return NStore.Core.BMAData.RDBS.GetInvoiceCount(uid);
        }

        /// <summary>
        /// 创建用户发票信息
        /// </summary>
        public static int CreateInvoice(InvoiceInfo invoiceInfo)
        {
            int invoiceId = NStore.Core.BMAData.RDBS.CreateInvoice(invoiceInfo);
            if (_usernosql != null)
                _usernosql.DeleteInvoiceList(invoiceInfo.Uid);
            return invoiceId;
        }


        /// <summary>
        /// 获得用户发票信息
        /// </summary>
        /// <param name="invoiceId">发票id</param>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static InvoiceInfo GetInvoicById(int invoiceId, int uid)
        {
            InvoiceInfo ivoiceInfo = null;

            if (_usernosql != null)
            {
                foreach (InvoiceInfo tempivoiceInfo in GetInvoiceList(uid))
                {
                    if (tempivoiceInfo.InvoiceId == invoiceId)
                    {
                        ivoiceInfo = tempivoiceInfo;
                        break;
                    }
                }
            }
            else
            {
                IDataReader reader = NStore.Core.BMAData.RDBS.GetShipAddressBySAId(invoiceId);
                if (reader.Read())
                {
                    ivoiceInfo = BuildInvoiceInfoFromReader(reader);
                }
                reader.Close();
            }

            return ivoiceInfo;
        }

        /// <summary>
        /// 更新用户发票信息
        /// </summary>
        public static void UpdateInvoic(InvoiceInfo invoiceInfo)
        {
            NStore.Core.BMAData.RDBS.UpdateInvoic(invoiceInfo);
            if (_usernosql != null)
                _usernosql.DeleteInvoiceList(invoiceInfo.Uid);
        }
        
        /// <summary>
        /// 删除用户发票信息
        /// </summary>
        /// <param name="invoiceId">发票id</param>
        /// <param name="uid">用户id</param>
        public static bool DeleteInvoic(int invoiceId, int uid)
        {
            bool result = NStore.Core.BMAData.RDBS.DeleteInvoic(invoiceId, uid);
            if (_usernosql != null)
                _usernosql.DeleteInvoice(invoiceId, uid);
            return result;
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
            bool result = NStore.Core.BMAData.RDBS.UpdateInvoicIsDefault(invoiceId, uid, isDefault);
            if (_usernosql != null)
                _usernosql.UpdateInvoiceIsDefault(invoiceId, uid, isDefault);
            return result;
        }

        /// <summary>
        /// 构建发票信息对象
        /// </summary>
        public static InvoiceInfo BuildInvoiceInfoFromReader(IDataReader reader)
        {
            InvoiceInfo invoiceInfo = new InvoiceInfo();

            invoiceInfo.InvoiceId = TypeHelper.ObjectToInt(reader["invoiceid"]);
            invoiceInfo.Uid = TypeHelper.ObjectToInt(reader["uid"]);
            invoiceInfo.IsDefault = TypeHelper.ObjectToInt(reader["isdefault"]);
            invoiceInfo.Alias = reader["alias"].ToString();
            invoiceInfo.Rise = reader["rise"].ToString();
            invoiceInfo.Address = reader["address"].ToString();
            invoiceInfo.Mobile = reader["mobile"].ToString();
            invoiceInfo.Account = reader["account"].ToString();
            invoiceInfo.Bank = reader["bank"].ToString();
            invoiceInfo.TaxId = reader["taxid"].ToString();
            invoiceInfo.Type = TypeHelper.ObjectToInt(reader["type"]);
            return invoiceInfo;
        }
    }
}
