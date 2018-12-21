using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStore.Core
{
    public class InvoiceInfo
    {
        private int _invoiceid;//发票信息id
        private int _uid;//用户id
        private int _isdefault;//是否是默认发票信息
        private string _alias;//别名
        private string _rise;//抬头
        private string _address;//地址
        private string _mobile;//电话
        private string _account;//开户行账号
        private string _bank;//开户行
        private string _taxid;//税务登记号
        private int _type;//发票类型：0个人，1企业
        
        /// <summary>
        /// 发票信息id
        /// </summary>
        public int InvoiceId
        {
            get { return _invoiceid; }
            set { _invoiceid = value; }
        }

        /// <summary>
        /// 用户id
        /// </summary>
        public int Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }

        /// <summary>
        /// 是否是默认发票信息
        /// </summary>
        public int IsDefault
        {
            get { return _isdefault; }
            set { _isdefault = value; }
        }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias
        {
            get { return _alias; }
            set { _alias = value; }
        }

        /// <summary>
        /// 抬头
        /// </summary>
        public string Rise
        {
            get { return _rise; }
            set { _rise = value; }
        }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        /// <summary>
        /// 电话
        /// </summary>
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }

        /// <summary>
        /// 开户行账号
        /// </summary>
        public string Account
        {
            get { return _account; }
            set { _account = value; }
        }

        /// <summary>
        /// 开户行
        /// </summary>
        public string Bank
        {
            get { return _bank; }
            set { _bank = value; }
        }

        /// <summary>
        /// 税务登记号
        /// </summary>
        public string TaxId
        {
            get { return _taxid; }
            set { _taxid = value; }
        }

        /// <summary>
        /// 发票类型：0个人，1企业
        /// </summary>
        public int Type
        {
            get { return _type; }
            set { _type = value; }
        }
    }
}
