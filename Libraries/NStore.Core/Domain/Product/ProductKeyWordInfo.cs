﻿using System;

namespace NStore.Core
{
    /// <summary>
    /// 商品关键词信息类
    /// </summary>
    public class ProductKeywordInfo
    {
        private int _keywordid;//关键词id
        private int _pid;//商品id
        private int _cateid;//商品类型id
        private int _brandid;//品牌id


        private string _keyword;//关键词
        private int _relevancy;//相关性

        /// <summary>
        /// 关键词id
        /// </summary>
        public int KeywordId
        {
            set { _keywordid = value; }
            get { return _keywordid; }
        }
        /// <summary>
        /// 商品id
        /// </summary>
        public int Pid
        {
            set { _pid = value; }
            get { return _pid; }
        }

        /// <summary>
        /// 商品类型id
        /// </summary>
        public int CateId
        {
            set { _cateid = value; }
            get { return _cateid; }
        }

        /// <summary>
        /// 品牌id
        /// </summary>
        public int BrandId
        {
            set { _brandid = value; }
            get { return _brandid; }
        }

        /// <summary>
        /// 关键词
        /// </summary>
        public string Keyword
        {
            set { _keyword = value.Trim(); }
            get { return _keyword; }
        }
        /// <summary>
        /// 相关性
        /// </summary>
        public int Relevancy
        {
            set { _relevancy = value; }
            get { return _relevancy; }
        }
    }
}
