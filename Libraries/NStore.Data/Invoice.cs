using NStore.Core;
using System;
using System.Collections.Generic;
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
                if (fullShipAddressList == null)
                {
                    fullShipAddressList = new List<FullShipAddressInfo>();
                    IDataReader reader = NStore.Core.BMAData.RDBS.GetFullShipAddressList(uid);
                    while (reader.Read())
                    {
                        FullShipAddressInfo fullShipAddressInfo = BuildFullShipAddressFromReader(reader);
                        fullShipAddressList.Add(fullShipAddressInfo);
                    }
                    reader.Close();
                    _usernosql.CreateFullShipAddressList(uid, fullShipAddressList);
                }
            }
            else
            {
                fullShipAddressList = new List<FullShipAddressInfo>();
                IDataReader reader = NStore.Core.BMAData.RDBS.GetFullShipAddressList(uid);
                while (reader.Read())
                {
                    FullShipAddressInfo fullShipAddressInfo = BuildFullShipAddressFromReader(reader);
                    fullShipAddressList.Add(fullShipAddressInfo);
                }
                reader.Close();
            }

            return fullShipAddressList;
        }
    }
}
