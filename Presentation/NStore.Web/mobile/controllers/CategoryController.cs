using System;
using System.Web.Mvc;
using System.Collections.Generic;

using NStore.Core;
using NStore.Services;
using NStore.Web.Framework;
using NStore.Web.Mobile.Models;

namespace NStore.Web.Mobile.Controllers
{
    /// <summary>
    /// 分类控制器类
    /// </summary>
    public partial class CategoryController : BaseMobileController
    {
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            int cateId = GetRouteInt("cateId");
            if (cateId == 0)
                cateId = WebHelper.GetQueryInt("cateId");

            CategoryInfo categoryInfo = null;
            List<CategoryInfo> categoryList = Categories.GetCategoryList();
            if (cateId > 0)
            {
                categoryInfo = Categories.GetCategoryById(cateId, categoryList);
                if (categoryInfo != null)
                    categoryList = Categories.GetChildCategoryList(cateId, categoryInfo.Layer, categoryList);
            }

            CategoryListModel model = new CategoryListModel();
            model.CategoryInfo = categoryInfo;
            model.CategoryList = categoryList;

            return View(model);
        }
    }
}
