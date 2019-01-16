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


        /// <summary>
        /// 编辑专场
        /// </summary>
        [HttpGet]
        public ActionResult Edit(int specialId = -1)
        {
            SpecialPerformanceInfo specialInfo = AdminSpecial.GetSpecialById(specialId);
            if (specialInfo == null)
                return PromptView("专场不存在");

            SpecialModel model = new SpecialModel();
            model.DisplayOrder = specialInfo.DisplayOrder;
            model.SpecialName = specialInfo.Name;
            model.State = specialInfo.State;
         
            ViewData["referer"] = MallUtils.GetMallAdminRefererCookie();

            return View(model);
        }

        /// <summary>
        /// 编辑专场
        /// </summary>
        [HttpPost]
        public ActionResult Edit(SpecialModel model, int specialId = -1)
        {
            SpecialPerformanceInfo specialInfo = AdminSpecial.GetSpecialById(specialId);
           if (specialInfo == null)
                return PromptView("专场不存在");

            int specialId2 = AdminSpecial.GetSpecialIdByName(model.SpecialName);
            if (specialId2 > 0 && specialId2 != specialId)
                ModelState.AddModelError("SpecialName", "名称已经存在");

            if (ModelState.IsValid)
            {
                specialInfo.DisplayOrder = model.DisplayOrder;
                specialInfo.Name = model.SpecialName;
                specialInfo.State = model.State;

                AdminSpecial.UpdateSpecial(specialInfo);
                AddMallAdminLog("修改专场", "修改专场,专场ID为:" + specialId);
                return PromptView("专场修改成功");
            }
            
            ViewData["referer"] = MallUtils.GetMallAdminRefererCookie();
            return View(model);
        }
        
        /// <summary>
        /// 删除专场
        /// </summary>
        public ActionResult Del(int specialId = -1)
        {
            AdminSpecial.DeleteSpecialById(specialId);
            AddMallAdminLog("删除专场", "删除专场,专场ID为:" + specialId);
            return PromptView("专场删除成功");
        }

    }
}