using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStore.Core
{
    public class RecommendProductInfo
    {
        private int _pid;//商品id
        private string _name = "";//商品名称
        private decimal _shopprice = 0M;//商品商城价
        private int _displayorder = 0;//商品排序
        private string _showimg = "";//商品展示图片
        private string _remark = "";//备注：几天内购买


        /// <summary>
        /// 商品id
        /// </summary>
        public int Pid
        {
            set { _pid = value; }
            get { return _pid; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 商品商城价
        /// </summary>
        public decimal ShopPrice
        {
            set { _shopprice = value; }
            get { return _shopprice; }
        }
        /// <summary>
        /// 商品排序
        /// </summary>
        public int DisplayOrder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }
        /// <summary>
        /// 商品展示图片
        /// </summary>
        public string ShowImg
        {
            set { _showimg = value; }
            get { return _showimg; }
        }
        /// <summary>
        /// 备注：几天内购买
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
    }
}
