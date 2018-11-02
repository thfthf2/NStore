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
    /// 店铺后台首页控制器类
    /// </summary>
    public partial class HomeController : BaseStoreAdminController
    {
        /// <summary>
        /// 首页
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 导航栏
        /// </summary>
        public ActionResult NavBar()
        {
            return View();
        }

        /// <summary>
        /// 菜单栏
        /// </summary>
        public ActionResult Menu()
        {
            return View();
        }

        /// <summary>
        /// 店铺运行信息
        /// </summary>
        public ActionResult StoreRunInfo()
        {
            StoreRunInfoModel model = new StoreRunInfoModel();

            model.WaitConfirmCount = AdminOrders.GetOrderCountByCondition(WorkContext.StoreId, (int)OrderState.Confirming, "", "");
            model.WaitPreProductCount = AdminOrders.GetOrderCountByCondition(WorkContext.StoreId, (int)OrderState.Confirmed, "", "");
            model.WaitSendCount = AdminOrders.GetOrderCountByCondition(WorkContext.StoreId, (int)OrderState.PreProducting, "", "");
            model.WaitPayCount = AdminOrders.GetOrderCountByCondition(WorkContext.StoreId, (int)OrderState.WaitPaying, "", "");

            MallUtils.SetAdminRefererCookie(Url.Action("storeruninfo"));
            return View(model);
        }
    }
}
