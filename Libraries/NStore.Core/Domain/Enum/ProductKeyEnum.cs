using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStore.Core.Domain.Enum
{
    public enum ProductKeyEnum
    {
        /// <summary>
        /// 1商品类型(分来跳转)
        /// </summary>
        Category= 1,
        
        /// <summary>
        /// 2品牌(品牌跳转+搜索词匹配)
        /// </summary>
        Brand = 2,

        /// <summary>
        /// 3专场(专场跳转+搜索词匹配)
        /// </summary>
        Special = 3,

        /// <summary>
        /// 4属性(搜索词匹配)
        /// </summary>
        Attribute = 4,

        /// <summary>
        /// 5属性值(搜索词匹配)
        /// </summary>
        AttributeValue = 5,

        /// <summary>
        /// 6关键词(搜索词匹配)
        /// </summary>
        KeyWord = 6,
            
        /// <summary>
        /// 7商品类型(搜索词匹配)
        /// </summary>
        CategorySearch = 7,
    }
}
