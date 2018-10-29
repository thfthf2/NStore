using System;
using System.Data;
using System.Collections.Generic;

using NStore.Core;

namespace NStore.Data
{
    /// <summary>
    /// 筛选词数据访问类
    /// </summary>
    public partial class FilterWords
    {
        /// <summary>
        /// 获得筛选词列表
        /// </summary>
        /// <returns></returns>
        public static List<FilterWordInfo> GetFilterWordList()
        {
            List<FilterWordInfo> filterWordList = new List<FilterWordInfo>();
            IDataReader reader = NStore.Core.BMAData.RDBS.GetFilterWordList();
            while (reader.Read())
            {
                FilterWordInfo filterWordInfo = new FilterWordInfo();
                filterWordInfo.Id = TypeHelper.ObjectToInt(reader["id"]);
                filterWordInfo.Match = reader["match"].ToString();
                filterWordInfo.Replace = reader["replace"].ToString();
                filterWordList.Add(filterWordInfo);
            }
            reader.Close();
            return filterWordList;
        }

        /// <summary>
        /// 添加筛选词
        /// </summary>
        public static void AddFilterWord(FilterWordInfo filterWordInfo)
        {
            NStore.Core.BMAData.RDBS.AddFilterWord(filterWordInfo);
        }

        /// <summary>
        /// 更新筛选词
        /// </summary>
        public static void UpdateFilterWord(FilterWordInfo filterWordInfo)
        {
            NStore.Core.BMAData.RDBS.UpdateFilterWord(filterWordInfo);
        }

        /// <summary>
        /// 删除筛选词
        /// </summary>
        /// <param name="idList">id列表</param>
        public static void DeleteFilterWordById(string idList)
        {
            NStore.Core.BMAData.RDBS.DeleteFilterWordById(idList);
        }
    }
}
