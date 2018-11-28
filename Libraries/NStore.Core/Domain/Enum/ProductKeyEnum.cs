using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStore.Core.Domain.Enum
{
    public enum ProductKeyEnum
    {
        //1商品类型，2品牌，3专场，4属性，5属性值，6关键词

        Category=1,
        Brand=2,
        Special=3,
        Attribute=4,
        AttributeValue=5,
        KeyWord=6
    }
}
