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
        /// 1商品类型
        /// </summary>
        Category = 1,
        
        /// <summary>
        /// 2品牌
        /// </summary>
        Brand = 2,

        /// <summary>
        /// 3专场
        /// </summary>
        Special = 3,

        /// <summary>
        /// 4属性
        /// </summary>
        Attribute = 4,

        /// <summary>
        /// 5属性值
        /// </summary>
        AttributeValue = 5,

        /// <summary>
        /// 6关键词
        /// </summary>
        KeyWord = 6
    }
}
