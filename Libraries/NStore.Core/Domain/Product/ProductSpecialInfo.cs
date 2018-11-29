using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStore.Core
{
    public class ProductSpecialInfo
    {
        private int _specialid;//专场id
        private int _pid;//商品id
        private int _cateid;//商品类型id
        private int _brandid;//品牌id

        /// <summary>
        /// 专场id
        /// </summary>
        public int SpecialId
        {
            set { _specialid = value; }
            get { return _specialid; }
        }
        /// <summary>
        /// 商品id
        /// </summary>
        public int Pid
        {
            set { _pid = value; }
            get { return _pid; }
        }

        /// <summary>
        /// 商品类型id
        /// </summary>
        public int CateId
        {
            set { _cateid = value; }
            get { return _cateid; }
        }

        /// <summary>
        /// 品牌id
        /// </summary>
        public int BrandId
        {
            set { _brandid = value; }
            get { return _brandid; }
        }

    }
}
