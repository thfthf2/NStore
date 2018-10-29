using System;
using System.Collections.Generic;

using NStore.Core;
using NStore.Services;
using NStore.Web.Framework;

namespace NStore.Web.Mobile.Models
{
    /// <summary>
    /// 分类列表模型类
    /// </summary>
    public class CategoryListModel
    {
        public CategoryInfo CategoryInfo { get; set; }
        public List<CategoryInfo> CategoryList { get; set; }
    }
}