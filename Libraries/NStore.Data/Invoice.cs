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
        /// 获得完整用户配送地址列表
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
