using System;
using System.Web;
using System.Web.Mvc;

using NStore.Core;
using NStore.Services;
using NStore.Web.Framework;
using NStore.Web.StoreAdmin.Models;

namespace NStore.Web.StoreAdmin.Controllers
{
    /// <summary>
    /// 店铺后台分类控制器类
    /// </summary>
    public partial class CategoryController : BaseStoreAdminController
    {
        /// <summary>
        /// 获得分类的属性及其值json列表
        /// </summary>
        /// <param name="cateId">分类id</param>
        /// <returns></returns>
        public ContentResult AAndVJsonList(int cateId = -1)
        {
            return Content(AdminCategories.GetCategoryAAndVListJsonCache(cateId));
        }
    }
}

