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


        /// <summary>
        /// 后台获得专场列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetSpecialList(int pageSize, int pageNumber, string sort)
        {
            return NStore.Core.BMAData.RDBS.AdminGetSpecialList(pageSize, pageNumber, sort);
        }

        /// <summary>
        /// 后台获得专场数量
        /// </summary>
        /// <returns></returns>
        public static int AdminGetSpecialCount()
        {
            return NStore.Core.BMAData.RDBS.AdminGetSpecialCount();
        }

        /// <summary>
        /// 根据专场名称得到品牌id
        /// </summary>
        /// <param name="brandName">品牌名称</param>
        /// <returns></returns>
        public static int GetSpecialIdByName(string specialName)
        {
            return NStore.Core.BMAData.RDBS.GetSpecialIdByName(specialName);
        }

        /// <summary>
        /// 创建专场
        /// </summary>
        /// <param name="brandInfo"></param>
        public static void CreateSpecial(SpecialPerformanceInfo specialInfo)
        {
            NStore.Core.BMAData.RDBS.CreateSpecial(specialInfo);
        }
    }
}
