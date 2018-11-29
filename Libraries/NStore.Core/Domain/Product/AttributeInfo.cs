﻿using System;

namespace NStore.Core
{
    /// <summary>
    /// 属性信息类
    /// </summary>
    public class AttributeInfo
    {
        private int _attrid;//属性id
        private string _name = "";//属性名称
        private int _attrgroupid;//分组id
        private int _displayorder = 0;//排序
        private int _state = 0;//状态，0有效，1无效

        private int _cateid = 0;//分类id
        private int _showtype = 0;//展示类型(0代表文字,1代表图片)
        private int _isfilter = 0;//是否是筛选属性


        /// <summary>
        /// 属性id
        /// </summary>
        public int AttrId
        {
            set { _attrid = value; }
            get { return _attrid; }
        }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name
        {
            set { _name = value.TrimEnd(); }
            get { return _name; }
        }

        /// <summary>
        /// 分组id
        /// </summary>
        public int AttrGroupId
        {
            set { _attrgroupid = value; }
            get { return _attrgroupid; }
        }

        /// <summary>
        /// 分类id
        /// </summary>
        public int CateId
        {
            set { _cateid = value; }
            get { return _cateid; }
        }
        /// <summary>
        /// 展示类型(0代表文字,1代表图片)
        /// </summary>
        public int ShowType
        {
            set { _showtype = value; }
            get { return _showtype; }
        }
        /// <summary>
        /// 是否是筛选属性
        /// </summary>
        public int IsFilter
        {
            set { _isfilter = value; }
            get { return _isfilter; }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }

        /// <summary>
        /// 状态，0有效，1无效
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }


        public override int GetHashCode()
        {
            //return base.GetHashCode();
            return AttrId.GetHashCode();
        }
    }
}

