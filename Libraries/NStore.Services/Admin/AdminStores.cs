using System;
using System.Data;
using System.Collections.Generic;

using NStore.Core;

namespace NStore.Services
{
    /// <summary>
    /// 后台店铺行业操作管理类
    /// </summary>
    public partial class AdminStores : Stores
    {
        /// <summary>
        /// 创建店铺
        /// </summary>
        /// <param name="storeInfo">店铺信息</param>
        /// <param name="storeKeeperInfo">店主信息</param>
        /// <returns>店铺id</returns>
        public static int CreateStore(StoreInfo storeInfo, StoreKeeperInfo storeKeeperInfo)
        {
            int storeId = NStore.Data.Stores.CreateStore(storeInfo);
            if (storeId > 0)
            {
                storeKeeperInfo.StoreId = storeId;
                CreateStoreKeeper(storeKeeperInfo);
            }
            return storeId;
        }

        /// <summary>
        /// 更新店铺
        /// </summary>
        /// <param name="storeInfo">店铺信息</param>
        public static void UpdateStore(StoreInfo storeInfo)
        {
            NStore.Data.Stores.UpdateStore(storeInfo);
        }

        /// <summary>
        /// 后台获得店铺列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetStoreList(int pageSize, int pageNumber, string condition, string sort)
        {
            return NStore.Data.Stores.AdminGetStoreList(pageSize, pageNumber, condition, sort);
        }

        /// <summary>
        /// 后台获得店铺选择列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static DataTable AdminGetStoreSelectList(int pageSize, int pageNumber, string condition)
        {
            return NStore.Data.Stores.AdminGetStoreSelectList(pageSize, pageNumber, condition);
        }

        /// <summary>
        /// 后台获得店铺列表条件
        /// </summary>
        /// <param name="storeName">店铺名称</param>
        /// <param name="storeRid">店铺等级id</param>
        /// <param name="storeIid">店铺行业id</param>
        /// <param name="state">店铺状态</param>
        /// <returns></returns>
        public static string AdminGetStoreListCondition(string storeName, int storeRid, int storeIid, int state)
        {
            return NStore.Data.Stores.AdminGetStoreListCondition(storeName, storeRid, storeIid, state);
        }

        /// <summary>
        /// 后台获得店铺列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetStoreListSort(string sortColumn, string sortDirection)
        {
            return NStore.Data.Stores.AdminGetStoreListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 后台获得店铺数量
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public static int AdminGetStoreCount(string condition)
        {
            return NStore.Data.Stores.AdminGetStoreCount(condition);
        }

        /// <summary>
        /// 后台根据店铺名称得到店铺id
        /// </summary>
        /// <param name="storeName">店铺名称</param>
        /// <returns></returns>
        public static int AdminGetStoreIdByName(string storeName)
        {
            return NStore.Data.Stores.AdminGetStoreIdByName(storeName);
        }

        /// <summary>
        /// 更新店铺状态
        /// </summary>
        /// <param name="storeId">店铺id</param>
        /// <param name="state">状态</param>
        /// <param name="stateEndTime">状态结束时间</param>
        public static void UpdateStoreState(int storeId, StoreState state, DateTime stateEndTime)
        {
            NStore.Data.Stores.UpdateStoreState(storeId, state, stateEndTime);
        }

        /// <summary>
        /// 删除店铺
        /// </summary>
        /// <param name="storeId">店铺id</param>
        /// <returns>-1代表此店铺下还有商品未删除，0代表此店铺不存在，1代表删除成功</returns>
        public static int DeleteStoreById(int storeId)
        {
            StoreInfo storeInfo = GetStoreById(storeId);
            if (storeInfo != null)
            {
                if (AdminProducts.AdminGetStoreProductCount(storeId) > 0)
                    return -1;

                NStore.Data.Stores.DeleteStoreById(storeId);
                NStore.Core.BMACache.Remove(CacheKeys.MALL_STORE_CLASSLIST + storeId);
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 获得店铺数量通过店铺行业id
        /// </summary>
        /// <param name="storeIid">店铺行业id</param>
        /// <returns></returns>
        public static int GetStoreCountByStoreIid(int storeIid)
        {
            return NStore.Data.Stores.GetStoreCountByStoreIid(storeIid);
        }

        /// <summary>
        /// 获得店铺数量通过店铺等级id
        /// </summary>
        /// <param name="storeRid">店铺等级id</param>
        /// <returns></returns>
        public static int GetStoreCountByStoreRid(int storeRid)
        {
            return NStore.Data.Stores.GetStoreCountByStoreRid(storeRid);
        }





        /// <summary>
        /// 创建店长
        /// </summary>
        /// <param name="storeKeeperInfo">店长信息</param>
        public static void CreateStoreKeeper(StoreKeeperInfo storeKeeperInfo)
        {
            NStore.Data.Stores.CreateStoreKeeper(storeKeeperInfo);
        }

        /// <summary>
        /// 更新店长
        /// </summary>
        /// <param name="storeKeeperInfo">店长信息</param>
        public static void UpdateStoreKeeper(StoreKeeperInfo storeKeeperInfo)
        {
            NStore.Data.Stores.UpdateStoreKeeper(storeKeeperInfo);
        }





        /// <summary>
        /// 创建店铺分类
        /// </summary>
        public static void CreateStoreClass(StoreClassInfo storeClassInfo)
        {
            if (storeClassInfo.ParentId > 0)
            {
                List<StoreClassInfo> storeClassList = NStore.Data.Stores.GetStoreClassList(storeClassInfo.StoreId);
                StoreClassInfo parentStoreClassInfo = storeClassList.Find(x => x.StoreCid == storeClassInfo.ParentId);
                storeClassInfo.Layer = parentStoreClassInfo.Layer + 1;
                storeClassInfo.HasChild = 0;
                storeClassInfo.Path = "";
                int storeCid = NStore.Data.Stores.CreateStoreClass(storeClassInfo);

                storeClassInfo.StoreCid = storeCid;
                storeClassInfo.Path = parentStoreClassInfo.Path + "," + storeCid;
                NStore.Data.Stores.UpdateStoreClass(storeClassInfo);

                if (parentStoreClassInfo.HasChild == 0)
                {
                    parentStoreClassInfo.HasChild = 1;
                    NStore.Data.Stores.UpdateStoreClass(parentStoreClassInfo);
                }
            }
            else
            {
                storeClassInfo.Layer = 1;
                storeClassInfo.HasChild = 0;
                storeClassInfo.Path = "";
                int storeCid = NStore.Data.Stores.CreateStoreClass(storeClassInfo);

                storeClassInfo.StoreCid = storeCid;
                storeClassInfo.Path = storeCid.ToString();
                NStore.Data.Stores.UpdateStoreClass(storeClassInfo);
            }


            NStore.Core.BMACache.Remove(CacheKeys.MALL_STORE_CLASSLIST + storeClassInfo.StoreId);
        }

        /// <summary>
        /// 更新店铺分类
        /// </summary>
        public static void UpdateStoreClass(StoreClassInfo storeClassInfo, int oldParentId)
        {
            if (storeClassInfo.ParentId != oldParentId)//父分类改变时
            {
                int changeLayer = 0;
                List<StoreClassInfo> storeClassList = NStore.Data.Stores.GetStoreClassList(storeClassInfo.StoreId);
                StoreClassInfo oldParentStoreClassInfo = storeClassList.Find(x => x.StoreCid == oldParentId);//旧的父分类
                if (storeClassInfo.ParentId > 0)//非顶层分类时
                {
                    StoreClassInfo newParentStoreClassInfo = storeClassList.Find(x => x.StoreCid == storeClassInfo.ParentId);//新的父分类
                    if (oldParentStoreClassInfo == null)
                    {
                        changeLayer = newParentStoreClassInfo.Layer - 1;
                    }
                    else
                    {
                        changeLayer = newParentStoreClassInfo.Layer - oldParentStoreClassInfo.Layer;
                    }
                    storeClassInfo.Layer = newParentStoreClassInfo.Layer + 1;
                    storeClassInfo.Path = newParentStoreClassInfo.Path + "," + storeClassInfo.StoreCid;

                    if (newParentStoreClassInfo.HasChild == 0)
                    {
                        newParentStoreClassInfo.HasChild = 1;
                        NStore.Data.Stores.UpdateStoreClass(newParentStoreClassInfo);
                    }
                }
                else//顶层分类时
                {
                    changeLayer = 1 - oldParentStoreClassInfo.Layer;
                    storeClassInfo.Layer = 1;
                    storeClassInfo.Path = storeClassInfo.StoreCid.ToString();
                }

                if (oldParentId > 0 && storeClassList.FindAll(x => x.ParentId == oldParentId).Count == 1)
                {
                    oldParentStoreClassInfo.HasChild = 0;
                    NStore.Data.Stores.UpdateStoreClass(oldParentStoreClassInfo);
                }

                foreach (StoreClassInfo info in storeClassList.FindAll(x => x.ParentId == storeClassInfo.StoreCid))
                {
                    UpdateChildStoreClassLayerAndPath(storeClassList, info, changeLayer, storeClassInfo.Path);
                }
            }

            NStore.Data.Stores.UpdateStoreClass(storeClassInfo);

            NStore.Core.BMACache.Remove(CacheKeys.MALL_STORE_CLASSLIST + storeClassInfo.StoreId);
        }

        /// <summary>
        /// 删除店铺分类
        /// </summary>
        /// <param name="storeId">店铺id</param>
        /// <param name="storeCid">店铺分类id</param>
        /// <returns>-2代表店铺分类不存在,-1代表有子店铺分类,0代表店铺分类下还有商品,1代表删除成功</returns>
        public static int DeleteStoreClassByStoreIdAndStoreCid(int storeId, int storeCid)
        {
            List<StoreClassInfo> storeClassList = NStore.Data.Stores.GetStoreClassList(storeId);
            StoreClassInfo storeClassInfo = storeClassList.Find(x => x.StoreCid == storeCid);
            if (storeClassInfo == null)
                return -2;
            if (storeClassInfo.HasChild == 1)
                return -1;
            if (AdminProducts.AdminGetStoreClassProductCount(storeCid) > 0)
                return 0;

            NStore.Data.Stores.DeleteStoreClassById(storeCid);

            if (storeClassInfo.Layer > 1 && storeClassList.FindAll(x => x.ParentId == storeClassInfo.ParentId).Count == 1)
            {
                StoreClassInfo parentStoreClassInfo = storeClassList.Find(x => x.StoreCid == storeClassInfo.ParentId);
                parentStoreClassInfo.HasChild = 0;
                NStore.Data.Stores.UpdateStoreClass(parentStoreClassInfo);
            }

            NStore.Core.BMACache.Remove(CacheKeys.MALL_STORE_CLASSLIST + storeId);
            return 1;
        }

        /// <summary>
        /// 递归更新店铺分类及其子店铺分类的级别和路径
        /// </summary>
        /// <param name="storeClassList">店铺分类列表</param>
        /// <param name="storeClassInfo">店铺分类信息</param>
        /// <param name="changeLayer">变化的级别</param>
        /// <param name="parentPath">父路径</param>
        private static void UpdateChildStoreClassLayerAndPath(List<StoreClassInfo> storeClassList, StoreClassInfo storeClassInfo, int changeLayer, string parentPath)
        {
            storeClassInfo.Layer = storeClassInfo.Layer + changeLayer;
            storeClassInfo.Path = parentPath + "," + storeClassInfo.StoreCid;
            NStore.Data.Stores.UpdateStoreClass(storeClassInfo);
            foreach (StoreClassInfo info in storeClassList.FindAll(x => x.ParentId == storeClassInfo.StoreCid))
            {
                UpdateChildStoreClassLayerAndPath(storeClassList, info, changeLayer, storeClassInfo.Path);
            }
        }




        /// <summary>
        /// 创建店铺配送模板
        /// </summary>
        /// <param name="storeShipTemplateInfo">店铺配送模板信息</param>
        public static int CreateStoreShipTemplate(StoreShipTemplateInfo storeShipTemplateInfo)
        {
            return NStore.Data.Stores.CreateStoreShipTemplate(storeShipTemplateInfo);
        }

        /// <summary>
        /// 更新店铺配送模板
        /// </summary>
        /// <param name="storeShipTemplateInfo">店铺配送模板信息</param>
        public static void UpdateStoreShipTemplate(StoreShipTemplateInfo storeShipTemplateInfo)
        {
            NStore.Data.Stores.UpdateStoreShipTemplate(storeShipTemplateInfo);
            NStore.Core.BMACache.Remove(CacheKeys.MALL_STORE_SHIPTEMPLATEINFO + storeShipTemplateInfo.StoreSTid);
        }

        /// <summary>
        /// 删除店铺配送模板
        /// </summary>
        /// <param name="storeSTid">店铺配送模板id</param>
        /// <returns>-1代表此店铺模板下还有商品未删除，0代表此店铺模板不存在，1代表删除成功</returns>
        public static int DeleteStoreShipTemplateById(int storeSTid)
        {
            StoreShipTemplateInfo storeShipTemplateInfo = GetStoreShipTemplateById(storeSTid);
            if (storeShipTemplateInfo == null)
                return 0;
            if (AdminProducts.AdminGetStoreShipTemplateProductCount(storeSTid) > 0)
                return -1;
            NStore.Data.Stores.DeleteStoreShipTemplateById(storeSTid);
            NStore.Core.BMACache.Remove(CacheKeys.MALL_STORE_SHIPTEMPLATEINFO + storeSTid);
            return 1;
        }

        /// <summary>
        /// 获得店铺配送模板列表
        /// </summary>
        /// <param name="storeId">店铺id</param>
        /// <returns></returns>
        public static List<StoreShipTemplateInfo> GetStoreShipTemplateList(int storeId)
        {
            return NStore.Data.Stores.GetStoreShipTemplateList(storeId);
        }




        /// <summary>
        /// 创建店铺配送费用
        /// </summary>
        /// <param name="storeShipFeeInfo">店铺配送费用信息</param>
        public static void CreateStoreShipFee(StoreShipFeeInfo storeShipFeeInfo)
        {
            NStore.Data.Stores.CreateStoreShipFee(storeShipFeeInfo);
        }

        /// <summary>
        /// 更新店铺配送费用
        /// </summary>
        /// <param name="storeShipFeeInfo">店铺配送费用信息</param>
        public static void UpdateStoreShipFee(StoreShipFeeInfo storeShipFeeInfo)
        {
            NStore.Data.Stores.UpdateStoreShipFee(storeShipFeeInfo);
        }

        /// <summary>
        /// 删除店铺配送费用
        /// </summary>
        /// <param name="recordId">记录id</param>
        public static void DeleteStoreShipFeeById(int recordId)
        {
            NStore.Data.Stores.DeleteStoreShipFeeById(recordId);
        }

        /// <summary>
        /// 获得店铺配送费用
        /// </summary>
        /// <param name="recordId">记录id</param>
        /// <returns></returns>
        public static StoreShipFeeInfo GetStoreShipFeeById(int recordId)
        {
            return NStore.Data.Stores.GetStoreShipFeeById(recordId);
        }

        /// <summary>
        /// 后台获得店铺配送费用列表
        /// </summary>
        /// <param name="storeSTid">店铺配送模板id</param>
        /// <returns></returns>
        public static DataTable AdminGetStoreShipFeeList(int storeSTid)
        {
            return NStore.Data.Stores.AdminGetStoreShipFeeList(storeSTid);
        }
    }
}
