var menuList = [
    { "title": "商品管理", "subMenuList": [
        { "title": "添加商品", "url": "/MallAdmin/product/addproduct" },
        { "title": "添加SKU", "url": "/MallAdmin/product/addsku" },
        { "title": "在售商品", "url": "/MallAdmin/product/onsaleproductlist" },
        { "title": "下架商品", "url": "/MallAdmin/product/outsaleproductlist" },
        { "title": "定时商品", "url": "/MallAdmin/product/timeproductlist" },
        { "title": "回收站", "url": "/MallAdmin/product/recyclebinproductlist" }]
    },
    { "title": "促销活动", "subMenuList": [
      { "title": "单品促销", "url": "/MallAdmin/promotion/singlepromotionlist" },
      { "title": "买送促销", "url": "/MallAdmin/promotion/buysendpromotionlist" },
      { "title": "赠品促销", "url": "/MallAdmin/promotion/giftpromotionlist" },
      { "title": "套装促销", "url": "/MallAdmin/promotion/suitpromotionlist" },
      { "title": "满赠促销", "url": "/MallAdmin/promotion/fullsendpromotionlist" },
      { "title": "满减促销", "url": "/MallAdmin/promotion/fullcutpromotionlist" },
      { "title": "专题管理", "url": "/MallAdmin/topic/list" },
      { "title": "优惠劵", "url": "/MallAdmin/coupon/coupontypelist" }]
    },
    { "title": "订单管理", "subMenuList": [
      { "title": "订单列表", "url": "/MallAdmin/order/orderlist" },
      { "title": "退款列表", "url": "/MallAdmin/order/refundlist" }]
    },
    { "title": "咨询评价", "subMenuList": [
      { "title": "商品评价", "url": "/MallAdmin/productreview/productreviewlist" },
      { "title": "商品咨询", "url": "/MallAdmin/productconsult/productconsultlist" },
      { "title": "咨询类型", "url": "/MallAdmin/productconsult/productconsulttypelist" }]
    },
    { "title": "用户管理", "subMenuList": [
      { "title": "用户列表", "url": "/MallAdmin/user/list" },
      { "title": "会员等级", "url": "/MallAdmin/userrank/list" },
      { "title": "管理员组", "url": "/MallAdmin/MallAdmingroup/list" }]
    },
    { "title": "店铺管理", "subMenuList": [
      { "title": "店铺列表", "url": "/MallAdmin/store/storelist" },
      { "title": "店铺行业", "url": "/MallAdmin/storeindustry/list" },
      { "title": "店铺等级", "url": "/MallAdmin/storerank/list" }]
    },
    { "title": "新闻管理", "subMenuList": [
      { "title": "新闻类型", "url": "/MallAdmin/news/newstypelist" },
      { "title": "新闻列表", "url": "/MallAdmin/news/newslist" }]
    },
    { "title": "广告管理", "subMenuList": [
      { "title": "广告位置", "url": "/MallAdmin/advert/advertpositionlist" },
      { "title": "广告列表", "url": "/MallAdmin/advert/advertlist" }]
    },
    { "title": "商城内容", "subMenuList": [
      { "title": "导航菜单", "url": "/MallAdmin/nav/list" },
      { "title": "商城帮助", "url": "/MallAdmin/help/list" },
      { "title": "友情链接", "url": "/MallAdmin/friendlink/list" },
      { "title": "Banner", "url": "/MallAdmin/banner/list" }]
    },
    { "title": "商品性质", "subMenuList": [
      { "title": "商品品牌", "url": "/MallAdmin/brand/list" },
      { "title": "分类管理", "url": "/MallAdmin/category/categorylist" },
      { "title": "专场管理", "url": "/MallAdmin/special/list" },
      { "title": "属性管理", "url": "/MallAdmin/category/attributelist" }]
    },
    { "title": "报表统计", "subMenuList": [
      { "title": "在线用户", "url": "/MallAdmin/stat/onlineuserlist" },
      { "title": "搜索分析", "url": "/MallAdmin/stat/searchwordstatlist" },
      { "title": "商品统计", "url": "/MallAdmin/stat/productstat" },
      { "title": "销售明细", "url": "/MallAdmin/stat/salelist" },
      { "title": "销售趋势", "url": "/MallAdmin/stat/saletrend" },
      { "title": "地区统计", "url": "/MallAdmin/stat/regionstat" },
      { "title": "客户端统计", "url": "/MallAdmin/stat/clientstat" }]
    },
    { "title": "系统设置", "subMenuList": [
      { "title": "站点信息", "url": "/MallAdmin/set/site" },
      { "title": "商城设置", "url": "/MallAdmin/set/mall" },
      { "title": "账号设置", "url": "/MallAdmin/set/account" },
      { "title": "上传设置", "url": "/MallAdmin/set/upload" },
      { "title": "性能设置", "url": "/MallAdmin/set/performance" },
      { "title": "访问控制", "url": "/MallAdmin/set/access" },
      { "title": "邮箱设置", "url": "/MallAdmin/set/email" },
      { "title": "短信设置", "url": "/MallAdmin/set/sms" },
      { "title": "积分设置", "url": "/MallAdmin/set/credit" },
      { "title": "打印订单", "url": "/MallAdmin/set/printorder" },
      { "title": "配送公司", "url": "/MallAdmin/shipcompany/list" },
      { "title": "禁止IP", "url": "/MallAdmin/bannedip/list" },
      { "title": "筛选词", "url": "/MallAdmin/filterword/list" }]
    },
    { "title": "商城插件", "subMenuList": [
      { "title": "授权插件", "url": "/MallAdmin/plugin/list?type=0" },
      { "title": "支付插件", "url": "/MallAdmin/plugin/list?type=1" }]
    },
    { "title": "日志管理", "subMenuList": [
      { "title": "商城日志", "url": "/MallAdmin/log/MallAdminloglist" },
      { "title": "店铺日志", "url": "/MallAdmin/log/MallAdminloglist" },
      { "title": "积分日志", "url": "/MallAdmin/log/creditloglist" }]
    },
    { "title": "开发人员", "subMenuList": [
      { "title": "事件管理", "url": "/MallAdmin/event/list" },
      { "title": "数据库管理", "url": "/MallAdmin/database/manage" }]
    }]