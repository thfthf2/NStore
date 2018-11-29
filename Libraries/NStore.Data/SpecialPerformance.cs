using NStore.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStore.Data
{
    /// <summary>
    /// 专场数据访问类
    /// </summary>
    public partial class SpecialPerformance
    {
        
        #region 辅助方法

        /// <summary>
        /// 通过IDataReader创建SpecialPerformanceInfo信息
        /// </summary>
        public static SpecialPerformanceInfo BuildSpecialFromReader(IDataReader reader)
        {
            SpecialPerformanceInfo specialInfo = new SpecialPerformanceInfo();

            specialInfo.Specialid = TypeHelper.ObjectToInt(reader["specialid"]);
            specialInfo.Name = reader["name"].ToString();
            specialInfo.DisplayOrder = TypeHelper.ObjectToInt(reader["displayorder"]);
            specialInfo.State = TypeHelper.ObjectToInt(reader["state"]);

            return specialInfo;
        }

        #endregion

        
        /// <summary>
        /// 获得专场列表
        /// </summary>
        /// <returns></returns>
        public static List<SpecialPerformanceInfo> GetSpecialList()
        {
            List<SpecialPerformanceInfo> specialList = new List<SpecialPerformanceInfo>();
            IDataReader reader = NStore.Core.BMAData.RDBS.GetSpecialList();
            while (reader.Read())
            {
                SpecialPerformanceInfo info = BuildSpecialFromReader(reader);
                specialList.Add(info);
            }
            reader.Close();
            return specialList;
        }
    }
}
