using NStore.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStore.Services
{
    public partial class SpecialPerformance
    {
        /// <summary>
        /// 获得分类列表
        /// </summary>
        /// <returns></returns>
        public static List<SpecialPerformanceInfo> GetSpecialList()
        {
            List<SpecialPerformanceInfo> specialList = NStore.Core.BMACache.Get(CacheKeys.MALL_CATEGORY_LIST) as List<SpecialPerformanceInfo>;
            if (specialList == null)
            {
                specialList = NStore.Data.SpecialPerformance.GetSpecialList();
                NStore.Core.BMACache.Insert(CacheKeys.MALL_SPECIAL_LIST, specialList);
            }
            return specialList;
        }
    }
}
