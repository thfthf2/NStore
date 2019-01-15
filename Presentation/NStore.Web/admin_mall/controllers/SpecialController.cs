using NStore.Core;
using NStore.Services;
using NStore.Services.Admin;
using NStore.Web.Framework;
using NStore.Web.MallAdmin.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NStore.Web.MallAdmin.controllers
{
    public class SpecialController : BaseMallAdminController
    {
        /// <summary>
        /// 后台专场列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List( string sortColumn, string sortDirection, int pageSize = 15, int pageNumber = 1)
        {
            PageModel pageModel = new PageModel(pageSize, pageNumber, AdminSpecial.AdminGetSpecialCount());

            SpecialListModel model = new SpecialListModel()
            {
                SpecialList = AdminSpecial.AdminGetSpecialList(pageModel.PageSize, pageModel.PageNumber, "[displayorder]"),
                PageModel = pageModel,
                SortColumn = sortColumn,
                SortDirection = sortDirection
            };
            MallUtils.SetAdminRefererCookie(string.Format("{0}?pageNumber={1}&pageSize={2}&sortColumn={3}&sortDirection={4}",
                                                          Url.Action("list"),
                                                          pageModel.PageNumber,
                                                          pageModel.PageSize,
                                                          sortColumn,
                                                          sortDirection));
            return View(model);
        }

        
        /// <summary>
        /// 添加专场
        /// </summary>
        [HttpGet]
        public ActionResult Add()
        {
            SpecialModel model = new SpecialModel();
            ViewData["referer"] = MallUtils.GetMallAdminRefererCookie();
            return View(model);
        }

        /// <summary>
        /// 添加专场
        /// </summary>
        [HttpPost]
        public ActionResult Add(SpecialModel model)
        {
            if (AdminSpecial.GetSpecialIdByName(model.SpecialName) > 0)
                ModelState.AddModelError("SpecialName", "名称已经存在");

            if (ModelState.IsValid)
            {
                SpecialPerformanceInfo specialInfo = new SpecialPerformanceInfo()
                {
                    DisplayOrder = model.DisplayOrder,
                    Name = model.SpecialName,
                    State = model.State
                };

                AdminSpecial.CreateSpecial(specialInfo);
                AddMallAdminLog("添加专场", "添加专场,专场为:" + model.SpecialName);
                return PromptView("专场添加成功");
            }
         
            ViewData["referer"] = MallUtils.GetMallAdminRefererCookie();
            return View(model);
        }

        
        ///// <summary>
        ///// 编辑专场
        ///// </summary>
        //[HttpGet]
        //public ActionResult Edit(int specialId = -1)
        //{
        //    BrandInfo brandInfo = AdminBrands.GetBrandById(brandId);
        //    if (brandInfo == null)
        //        return PromptView("品牌不存在");

        //    BrandModel model = new BrandModel();
        //    model.DisplayOrder = brandInfo.DisplayOrder;
        //    model.BrandName = brandInfo.Name;
        //    model.Logo = brandInfo.Logo;
        //    Load();

        //    return View(model);
        //}

        ///// <summary>
        ///// 编辑专场
        ///// </summary>
        //[HttpPost]
        //public ActionResult Edit(BrandModel model, int brandId = -1)
        //{
        //    BrandInfo brandInfo = AdminBrands.GetBrandById(brandId);
        //    if (brandInfo == null)
        //        return PromptView("品牌不存在");

        //    int brandId2 = AdminBrands.GetBrandIdByName(model.BrandName);
        //    if (brandId2 > 0 && brandId2 != brandId)
        //        ModelState.AddModelError("BrandName", "名称已经存在");

        //    if (ModelState.IsValid)
        //    {
        //        brandInfo.DisplayOrder = model.DisplayOrder;
        //        brandInfo.Name = model.BrandName;
        //        brandInfo.Logo = model.Logo;

        //        AdminBrands.UpdateBrand(brandInfo);
        //        AddMallAdminLog("修改品牌", "修改品牌,品牌ID为:" + brandId);
        //        return PromptView("品牌修改成功");
        //    }

        //    Load();
        //    return View(model);
        //}

    }
}