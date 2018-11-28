using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStore.Core
{
    /// <summary>
    /// 商品搜索关联词信息
    /// </summary>
    public class ProductSearchKeyInfo
    {
        private string _name;//关联词
        private int _toid;//关联词目标id
        private int _keytype;//关联词类型：1商品类型，2品牌，3专场，4属性，5属性值，6关键词

        
        /// <summary>
        /// 关联词
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }    
           
        /// <summary>
        /// 关联词目标id
        /// </summary>
        public int ToId
        {
            set { _toid = value; }
            get { return _toid; }
        }   
            
        /// <summary>
        /// 关联词类型：1商品类型，2品牌，3专场，4属性，5属性值，6关键词
        /// </summary>
        public int keyType
        {
            set { _keytype = value; }
            get { return _keytype; }
        }
    }
}
