﻿using System;
using System.Data;
using System.Collections.Generic;

using NStore.Core;

namespace NStore.Data
{
    /// <summary>
    /// 分类数据访问类
    /// </summary>
    public partial class Categories
    {
        #region 辅助方法

        /// <summary>
        /// 通过IDataReader创建CategoryInfo信息
        /// </summary>
        public static CategoryInfo BuildCategoryFromReader(IDataReader reader)
        {
            CategoryInfo categoryInfo = new CategoryInfo();

            categoryInfo.CateId = TypeHelper.ObjectToInt(reader["cateid"]);
            categoryInfo.DisplayOrder = TypeHelper.ObjectToInt(reader["displayorder"]);
            categoryInfo.Name = reader["name"].ToString();
            categoryInfo.PriceRange = reader["pricerange"].ToString();
            categoryInfo.ParentId = TypeHelper.ObjectToInt(reader["parentid"]);
            categoryInfo.Layer = TypeHelper.ObjectToInt(reader["layer"]);
            categoryInfo.HasChild = TypeHelper.ObjectToInt(reader["haschild"]);
            categoryInfo.Path = reader["path"].ToString();

            return categoryInfo;
        }

        /// <summary>
        /// 通过IDataReader创建AttributeGroupInfo信息
        /// </summary>
        public static AttributeGroupInfo BuildAttributeGroupFromReader(IDataReader reader)
        {
            AttributeGroupInfo attributeGroupInfo = new AttributeGroupInfo();

            attributeGroupInfo.AttrGroupId = TypeHelper.ObjectToInt(reader["attrgroupid"]);
            //attributeGroupInfo.CateId = TypeHelper.ObjectToInt(reader["cateid"]);
            attributeGroupInfo.Name = reader["name"].ToString();
            attributeGroupInfo.DisplayOrder = TypeHelper.ObjectToInt(reader["displayorder"]);

            return attributeGroupInfo;
        }

        /// <summary>
        /// 通过IDataReader创建AttributeInfo信息
        /// </summary>
        public static AttributeInfo BuildAttributeFromReader(IDataReader reader)
        {
            AttributeInfo attributeInfo = new AttributeInfo();

            attributeInfo.AttrId = TypeHelper.ObjectToInt(reader["attrid"]);
            attributeInfo.Name = reader["name"].ToString();
            attributeInfo.AttrGroupId = TypeHelper.ObjectToInt(reader["attrgroupid"]);
            attributeInfo.ShowType = TypeHelper.ObjectToInt(reader["showtype"]);
            attributeInfo.DisplayOrder = TypeHelper.ObjectToInt(reader["displayorder"]);

            //attributeInfo.CateId = TypeHelper.ObjectToInt(reader["cateid"]);
            //attributeInfo.IsFilter = TypeHelper.ObjectToInt(reader["isfilter"]);

            return attributeInfo;
        }

        /// <summary>
        /// 通过IDataReader创建AttributeValueInfo信息
        /// </summary>
        public static AttributeValueInfo BuildAttributeValueFromReader(IDataReader reader)
        {
            AttributeValueInfo attributeValueInfo = new AttributeValueInfo();

            attributeValueInfo.AttrValueId = TypeHelper.ObjectToInt(reader["attrvalueid"]);
            attributeValueInfo.AttrValue = reader["attrvalue"].ToString();
            attributeValueInfo.AttrValueDisplayOrder = TypeHelper.ObjectToInt(reader["attrvaluedisplayorder"]);
            attributeValueInfo.AttrId = TypeHelper.ObjectToInt(reader["attrid"]);
            attributeValueInfo.AttrName = reader["attrname"].ToString();
            attributeValueInfo.AttrShowType = TypeHelper.ObjectToInt(reader["attrshowtype"]);
            attributeValueInfo.AttrDisplayOrder = TypeHelper.ObjectToInt(reader["attrdisplayorder"]);
            attributeValueInfo.AttrGroupId = TypeHelper.ObjectToInt(reader["attrgroupid"]);
            attributeValueInfo.AttrGroupName = reader["attrgroupname"].ToString();
            attributeValueInfo.AttrGroupDisplayOrder = TypeHelper.ObjectToInt(reader["attrgroupdisplayorder"]);

            //attributeValueInfo.IsInput = TypeHelper.ObjectToInt(reader["isinput"]);

            return attributeValueInfo;
        }

        #endregion

        /// <summary>
        /// 获得分类列表
        /// </summary>
        /// <returns></returns>
        public static List<CategoryInfo> GetCategoryList()
        {
            List<CategoryInfo> categoryList = new List<CategoryInfo>();
            IDataReader reader = NStore.Core.BMAData.RDBS.GetCategoryList();
            while (reader.Read())
            {
                CategoryInfo categoryInfo = BuildCategoryFromReader(reader);
                categoryList.Add(categoryInfo);
            }
            reader.Close();
            return categoryList;
        }

        /// <summary>
        /// 更新分类
        /// </summary>
        public static void UpdateCategory(CategoryInfo categoryInfo)
        {
            NStore.Core.BMAData.RDBS.UpdateCategory(categoryInfo);
        }

        /// <summary>
        /// 创建分类
        /// </summary>
        public static int CreateCategory(CategoryInfo categoryInfo)
        {
            return NStore.Core.BMAData.RDBS.CreateCategory(categoryInfo);
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="cateId">分类id</param>
        public static void DeleteCategoryById(int cateId)
        {
            NStore.Core.BMAData.RDBS.DeleteCategoryById(cateId);
        }

        /// <summary>
        /// 获得分类关联的品牌
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public static List<BrandInfo> GetCategoryBrandList(int cateId)
        {
            List<BrandInfo> brandList = new List<BrandInfo>();
            IDataReader reader = NStore.Core.BMAData.RDBS.GetCategoryBrandList(cateId);
            while (reader.Read())
            {
                BrandInfo brandInfo = Brands.BuildBrandFromReader(reader);
                brandList.Add(brandInfo);
            }
            reader.Close();
            return brandList;
        }


        /// <summary>
        /// 获得分类的属性分组列表
        /// </summary>
        /// <returns></returns>
        public static List<AttributeGroupInfo> GetAttributeGroupList()
        {
            List<AttributeGroupInfo> attributeGroupList = new List<AttributeGroupInfo>();
            IDataReader reader = NStore.Core.BMAData.RDBS.GetAttributeGroupList();
            while (reader.Read())
            {
                AttributeGroupInfo attributeGroupInfo = BuildAttributeGroupFromReader(reader);
                attributeGroupList.Add(attributeGroupInfo);
            }
            reader.Close();
            return attributeGroupList;
        }

        /// <summary>
        /// 获得属性分组
        /// </summary>
        /// <param name="attrGroupId">属性分组id</param>
        public static AttributeGroupInfo GetAttributeGroupById(int attrGroupId)
        {
            AttributeGroupInfo attributeGroupInfo = null;
            IDataReader reader = NStore.Core.BMAData.RDBS.GetAttributeGroupById(attrGroupId);
            if (reader.Read())
            {
                attributeGroupInfo = BuildAttributeGroupFromReader(reader);
            }

            reader.Close();
            return attributeGroupInfo;
        }

        /// <summary>
        /// 更新属性分组
        /// </summary>
        /// <param name="newAttributeGroupInfo">新属性分组</param>
        /// <param name="oldAttributeGroupInfo">原属性分组</param>
        public static void UpdateAttributeGroup(AttributeGroupInfo attributeGroupInfo)
        {
            NStore.Core.BMAData.RDBS.UpdateAttributeGroup(attributeGroupInfo);
        }

        /// <summary>
        /// 创建属性分组
        /// </summary>
        public static void CreateAttributeGroup(AttributeGroupInfo attributeGroupInfo)
        {
            NStore.Core.BMAData.RDBS.CreateAttributeGroup(attributeGroupInfo);
        }

        /// <summary>
        /// 删除属性分组
        /// </summary>
        /// <param name="attrGroupId">属性分组id</param>
        public static void DeleteAttributeGroupById(int attrGroupId)
        {
            NStore.Core.BMAData.RDBS.DeleteAttributeGroupById(attrGroupId);
        }

        /// <summary>
        /// 通过分类id和属性分组名称获得分组id
        /// </summary>
        /// <param name="name">分组名称</param>
        /// <returns></returns>
        public static int GetAttributeGroupIdByName( string name)
        {
            return NStore.Core.BMAData.RDBS.GetAttributeGroupIdByName(name);
        }






        /// <summary>
        /// 获得属性列表
        /// </summary>
        /// <returns></returns>
        public static List<AttributeInfo> GetAttributeList()
        {
            List<AttributeInfo> attributeList = new List<AttributeInfo>();
            IDataReader reader = NStore.Core.BMAData.RDBS.GetAttributeList();
            while (reader.Read())
            {
                AttributeInfo attributeInfo = BuildAttributeFromReader(reader);
                attributeList.Add(attributeInfo);
            }
            reader.Close();
            return attributeList;
        }

        /// <summary>
        /// 获得属性列表
        /// </summary>
        /// <param name="attrGroupId">属性分组id</param>
        /// <returns></returns>
        public static List<AttributeInfo> GetAttributeListByAttrGroupId(int attrGroupId)
        {
            List<AttributeInfo> attributeList = new List<AttributeInfo>();
            IDataReader reader = NStore.Core.BMAData.RDBS.GetAttributeListByAttrGroupId(attrGroupId);
            while (reader.Read())
            {
                AttributeInfo attributeInfo = BuildAttributeFromReader(reader);
                attributeList.Add(attributeInfo);
            }
            reader.Close();
            return attributeList;
        }

        /// <summary>
        /// 后台获得属性列表
        /// </summary>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static DataTable AdminGetAttributeList(string sort)
        {
            return NStore.Core.BMAData.RDBS.AdminGetAttributeList(sort);
        }

        /// <summary>
        /// 后台获得属性列表排序
        /// </summary>
        /// <param name="sortColumn">排序字段</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string AdminGetAttributeListSort(string sortColumn, string sortDirection)
        {
            return NStore.Core.BMAData.RDBS.AdminGetAttributeListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 获得筛选属性列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public static List<AttributeInfo> GetFilterAttributeListByCateId(int cateId)
        {
            List<AttributeInfo> attributeList = new List<AttributeInfo>();
            IDataReader reader = NStore.Core.BMAData.RDBS.GetFilterAttributeListByCateId(cateId);
            while (reader.Read())
            {
                AttributeInfo attributeInfo = BuildAttributeFromReader(reader);
                attributeList.Add(attributeInfo);
            }
            reader.Close();
            return attributeList;
        }

        /// <summary>
        /// 获得筛选属性列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public static List<AttributeInfo> GetFilterAttributeList()
        {
            List<AttributeInfo> attributeList = new List<AttributeInfo>();
            IDataReader reader = NStore.Core.BMAData.RDBS.GetFilterAttributeList();
            while (reader.Read())
            {
                AttributeInfo attributeInfo = BuildAttributeFromReader(reader);
                attributeList.Add(attributeInfo);
            }
            reader.Close();
            return attributeList;
        }

        /// <summary>
        /// 根据属性id获得筛选属性列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public static List<AttributeInfo> GetFilterAttributeListByIds(List<int> ids)
        {
            var rattributeList = GetFilterAttributeList();
            if (rattributeList == null)
            {
                return new List<AttributeInfo>();
            }
            return rattributeList.FindAll(p => ids.Contains(p.AttrId));
        }

        /// <summary>
        /// 获得属性
        /// </summary>
        /// <param name="attrId">属性id</param>
        /// <returns></returns>
        public static AttributeInfo GetAttributeById(int attrId)
        {
            AttributeInfo attributeInfo = null;
            IDataReader reader = NStore.Core.BMAData.RDBS.GetAttributeById(attrId);
            if (reader.Read())
            {
                attributeInfo = BuildAttributeFromReader(reader);
            }

            reader.Close();
            return attributeInfo;
        }

        /// <summary>
        /// 更新属性
        /// </summary>
        /// <param name="newAttributeInfo">新属性</param>
        /// <param name="oldAttributeInfo">原属性</param>
        public static void UpdateAttribute(AttributeInfo attributeInfo)
        {
            NStore.Core.BMAData.RDBS.UpdateAttribute(attributeInfo);
        }

        /// <summary>
        /// 创建属性
        /// </summary>
        /// <param name="attributeInfo">属性信息</param>
        /// <param name="attrGroupId">属性组id</param>
        /// <param name="attrGroupName">属性组名称</param>
        /// <param name="attrGroupDisplayOrder">属性组排序</param>
        public static void CreateAttribute(AttributeInfo attributeInfo, int attrGroupId, string attrGroupName, int attrGroupDisplayOrder)
        {
            NStore.Core.BMAData.RDBS.CreateAttribute(attributeInfo, attrGroupId, attrGroupName, attrGroupDisplayOrder);
        }

        /// <summary>
        /// 删除属性
        /// </summary>
        /// <param name="attrId">属性id</param>
        public static void DeleteAttributeById(int attrId)
        {
            NStore.Core.BMAData.RDBS.DeleteAttributeById(attrId);
        }

        /// <summary>
        /// 通过属性名称获得属性id
        /// </summary>
        /// <param name="attributeName">属性名称</param>
        /// <returns></returns>
        public static int GetAttrIdByName(string attributeName)
        {
            return NStore.Core.BMAData.RDBS.GetAttrIdByName( attributeName);
        }





        /// <summary>
        /// 获得属性值列表
        /// </summary>
        /// <param name="attrId">属性id</param>
        /// <returns></returns>
        public static List<AttributeValueInfo> GetAttributeValueListByAttrId(int attrId)
        {
            List<AttributeValueInfo> attributeValueList = new List<AttributeValueInfo>();
            IDataReader reader = NStore.Core.BMAData.RDBS.GetAttributeValueListByAttrId(attrId);
            while (reader.Read())
            {
                AttributeValueInfo attributeValueInfo = BuildAttributeValueFromReader(reader);
                attributeValueList.Add(attributeValueInfo);
            }
            reader.Close();
            return attributeValueList;
        }

        /// <summary>
        /// 获得属性值
        /// </summary>
        /// <param name="attrValueId">属性值id</param>
        /// <returns></returns>
        public static AttributeValueInfo GetAttributeValueById(int attrValueId)
        {
            AttributeValueInfo attributeValueInfo = null;
            IDataReader reader = NStore.Core.BMAData.RDBS.GetAttributeValueById(attrValueId);
            if (reader.Read())
            {
                attributeValueInfo = BuildAttributeValueFromReader(reader);
            }

            reader.Close();
            return attributeValueInfo;
        }

        /// <summary>
        /// 创建属性值
        /// </summary>
        public static void CreateAttributeValue(AttributeValueInfo attributeValueInfo)
        {
            NStore.Core.BMAData.RDBS.CreateAttributeValue(attributeValueInfo);
        }

        /// <summary>
        /// 删除属性值
        /// </summary>
        /// <param name="attrValueId">属性值id</param>
        public static void DeleteAttributeValueById(int attrValueId)
        {
            NStore.Core.BMAData.RDBS.DeleteAttributeValueById(attrValueId);
        }

        /// <summary>
        /// 更新属性值
        /// </summary>
        public static void UpdateAttributeValue(AttributeValueInfo attributeValueInfo)
        {
            NStore.Core.BMAData.RDBS.UpdateAttributeValue(attributeValueInfo);
        }

        /// <summary>
        /// 通过属性id和属性值获得属性值的id
        /// </summary>
        /// <param name="attrId">属性id</param>
        /// <param name="attrValue">属性值</param>
        /// <returns></returns>
        public static int GetAttributeValueIdByAttrIdAndValue(int attrId, string attrValue)
        {
            return NStore.Core.BMAData.RDBS.GetAttributeValueIdByAttrIdAndValue(attrId, attrValue);
        }
    }
}
