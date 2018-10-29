using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using NStore.Core;
using NStore.Services;
using NStore.Web.Framework;

namespace NStore.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //商品路由
            routes.MapRoute("product",
                            "{pid}.html",
                            new { controller = "catalog", action = "product" },
                            new[] { "NStore.Web.Controllers" });
            //分类路由
            routes.MapRoute("category",
                            "list/{filterAttr}-{cateId}-{brandId}-{filterPrice}-{onlyStock}-{sortColumn}-{sortDirection}-{page}.html",
                            new { controller = "catalog", action = "category" },
                            new[] { "NStore.Web.Controllers" });
            //分类路由
            routes.MapRoute("shortcategory",
                            "list/{cateId}.html",
                            new { controller = "catalog", action = "category" },
                            new[] { "NStore.Web.Controllers" });
            //商城搜索路由
            routes.MapRoute("mallsearch",
                            "search",
                            new { controller = "catalog", action = "search" },
                            new[] { "NStore.Web.Controllers" });
            //店铺路由
            routes.MapRoute("store",
                            "store/{storeId}.html",
                            new { controller = "store", action = "index" },
                            new[] { "NStore.Web.Controllers" });
            //店铺分类路由
            routes.MapRoute("storeclass",
                            "storeclass/{storeId}-{storeCid}-{startPrice}-{endPrice}-{sortColumn}-{sortDirection}-{page}.html",
                            new { controller = "store", action = "class" },
                            new[] { "NStore.Web.Controllers" });
            //店铺分类路由
            routes.MapRoute("shortstoreclass",
                            "storeclass/{storeId}-{storeCid}.html",
                            new { controller = "store", action = "class" },
                            new[] { "NStore.Web.Controllers" });
            //店铺搜索路由
            routes.MapRoute("storesearch",
                            "searchstore",
                            new { controller = "store", action = "search" },
                            new[] { "NStore.Web.Controllers" });
            //默认路由(此路由不能删除)
            routes.MapRoute("default",
                            "{controller}/{action}",
                            new { controller = "home", action = "index" },
                            new[] { "NStore.Web.Controllers" });
        }

        protected void Application_Start()
        {
            //将默认视图引擎替换为ThemeRazorViewEngine引擎
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new ThemeRazorViewEngine());

            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            //启动事件机制
            BMAEvent.Start();
            //服务器宕机启动后重置在线用户表
            if (Environment.TickCount > 0 && Environment.TickCount < 900000)
                OnlineUsers.ResetOnlineUserTable();
        }
    }
}