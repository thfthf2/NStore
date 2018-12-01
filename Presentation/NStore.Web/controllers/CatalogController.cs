using System;
using System.Text;
using System.Data;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections.Generic;

using NStore.Core;
using NStore.Services;
using NStore.Web.Framework;
using NStore.Web.Models;
using NStore.Core.Domain.Enum;
using System.Linq;

namespace NStore.Web.Controllers
{
    /// <summary>
    /// 商城目录控制器类
    /// </summary>
    public partial class CatalogController : BaseWebController
    {
        /// <summary>
        /// 商品
        /// </summary>
        public ActionResult Product()
        {
            //商品id
            int pid = GetRouteInt("pid");
            if (pid == 0)
                pid = WebHelper.GetQueryInt("pid");

            //判断商品是否存在
            ProductInfo productInfo = Products.GetProductById(pid);
            if (productInfo == null)
                return PromptView("/", "你访问的商品不存在");

            //店铺信息
            StoreInfo storeInfo = Stores.GetStoreById(productInfo.StoreId);
            if (storeInfo.State != (int)StoreState.Open)
                return PromptView("/", "你访问的商品不存在");

            //商品存在时
            ProductModel model = new ProductModel();
            //商品id
            model.Pid = pid;
            //商品信息
            model.ProductInfo = productInfo;
            //商品分类
            model.CategoryInfo = Categories.GetCategoryById(productInfo.CateId);
            //商品品牌
            model.BrandInfo = Brands.GetBrandById(productInfo.BrandId);
            //店铺信息
            model.StoreInfo = storeInfo;
            //店长信息
            model.StoreKeeperInfo = Stores.GetStoreKeeperById(storeInfo.StoreId);
            //店铺区域
            model.StoreRegion = Regions.GetRegionById(storeInfo.RegionId);
            //店铺等级信息
            model.StoreRankInfo = StoreRanks.GetStoreRankById(storeInfo.StoreRid);
            //商品图片列表
            model.ProductImageList = Products.GetProductImageList(pid);
            //扩展商品属性列表
            model.ExtProductAttributeList = Products.GetExtProductAttributeList(pid);
            //商品SKU列表
            model.ProductSKUList = Products.GetProductSKUListBySKUGid(productInfo.SKUGid);
            //商品库存数量
            model.StockNumber = Products.GetProductStockNumberByPid(pid);


            //单品促销
            model.SinglePromotionInfo = Promotions.GetSinglePromotionByPidAndTime(pid, DateTime.Now);
            //买送促销活动列表
            model.BuySendPromotionList = Promotions.GetBuySendPromotionList(productInfo.StoreId, pid, DateTime.Now);
            //赠品促销活动
            model.GiftPromotionInfo = Promotions.GetGiftPromotionByPidAndTime(pid, DateTime.Now);
            //赠品列表
            if (model.GiftPromotionInfo != null)
                model.ExtGiftList = Promotions.GetExtGiftList(model.GiftPromotionInfo.PmId);
            //套装商品列表
            model.SuitProductList = Promotions.GetProductAllSuitPromotion(pid, DateTime.Now);
            //满赠促销活动
            model.FullSendPromotionInfo = Promotions.GetFullSendPromotionByStoreIdAndPidAndTime(productInfo.StoreId, pid, DateTime.Now);
            //满减促销活动
            model.FullCutPromotionInfo = Promotions.GetFullCutPromotionByStoreIdAndPidAndTime(productInfo.StoreId, pid, DateTime.Now);

            //广告语
            model.Slogan = model.SinglePromotionInfo == null ? "" : model.SinglePromotionInfo.Slogan;
            //商品促销信息
            model.PromotionMsg = Promotions.GeneratePromotionMsg(model.SinglePromotionInfo, model.BuySendPromotionList, model.FullSendPromotionInfo, model.FullCutPromotionInfo);
            //商品折扣价格
            model.DiscountPrice = Promotions.ComputeDiscountPrice(model.ProductInfo.ShopPrice, model.SinglePromotionInfo);

            //关联商品列表
            model.RelateProductList = Products.GetRelateProductList(pid);

            //用户浏览历史
            model.UserBrowseHistory = BrowseHistories.GetUserBrowseHistory(WorkContext.Uid, pid);

            //商品咨询类型列表
            model.ProductConsultTypeList = ProductConsults.GetProductConsultTypeList();

            //更新浏览历史
            if (WorkContext.Uid > 0)
                Asyn.UpdateBrowseHistory(WorkContext.Uid, pid);
            //更新商品统计
            Asyn.UpdateProductStat(pid, WorkContext.RegionId);

            return View(model);
        }

        /// <summary>
        /// 套装
        /// </summary>
        public ActionResult Suit()
        {
            //套装id
            int pmId = GetRouteInt("pmId");
            if (pmId == 0)
                pmId = WebHelper.GetQueryInt("pmId");

            //判断套装是否存在或过期
            SuitPromotionInfo suitPromotionInfo = Promotions.GetSuitPromotionByPmIdAndTime(pmId, DateTime.Now);
            if (suitPromotionInfo == null)
                return PromptView("/", "你访问的套装不存在或过期");

            //店铺信息
            StoreInfo storeInfo = Stores.GetStoreById(suitPromotionInfo.StoreId);
            if (storeInfo.State != (int)StoreState.Open)
                return PromptView("/", "你访问的套装不存在");

            //扩展套装商品列表
            List<ExtSuitProductInfo> extSuitProductList = Promotions.GetExtSuitProductList(pmId);

            SuitModel model = new SuitModel();
            model.SuitPromotionInfo = suitPromotionInfo;
            model.SuitProductList = extSuitProductList;
            model.StoreInfo = storeInfo;
            model.StoreKeeperInfo = Stores.GetStoreKeeperById(storeInfo.StoreId);
            model.StoreRegion = Regions.GetRegionById(storeInfo.RegionId);
            model.StoreRankInfo = StoreRanks.GetStoreRankById(storeInfo.StoreRid);

            foreach (ExtSuitProductInfo extSuitProductInfo in extSuitProductList)
            {
                model.SuitDiscount += extSuitProductInfo.Number * extSuitProductInfo.Discount;
                model.ProductAmount += extSuitProductInfo.Number * extSuitProductInfo.ShopPrice;
            }
            model.SuitAmount = model.ProductAmount - model.SuitDiscount;

            return View(model);
        }

        /// <summary>
        /// 分类
        /// </summary>
        public ActionResult Category()
        {
            //分类id
            int cateId = GetRouteInt("cateId");
            if (cateId == 0)
                cateId = WebHelper.GetQueryInt("cateId");
            //品牌id
            int brandId = GetRouteInt("brandId");
            if (brandId == 0)
                brandId = WebHelper.GetQueryInt("brandId");
            //专场id
            int specialId = GetRouteInt("specialId");
            if (specialId == 0)
                specialId = WebHelper.GetQueryInt("specialId");
            ////关键词
            //string keyword = GetRouteString("keyword");
            //if (string.IsNullOrEmpty(keyword))
            //    keyword = WebHelper.GetQueryString("keyword");
            //筛选价格
            int filterPrice = GetRouteInt("filterPrice");
            if (filterPrice == 0)
                filterPrice = WebHelper.GetQueryInt("filterPrice");
            //筛选属性
            string filterAttr = GetRouteString("filterAttr");
            if (filterAttr.Length == 0)
                filterAttr = WebHelper.GetQueryString("filterAttr");
            //是否只显示有货
            int onlyStock = GetRouteInt("onlyStock");
            if (onlyStock == 0)
                onlyStock = WebHelper.GetQueryInt("onlyStock");
            //排序列
            int sortColumn = GetRouteInt("sortColumn");
            if (sortColumn == 0)
                sortColumn = WebHelper.GetQueryInt("sortColumn");
            //排序方向
            int sortDirection = GetRouteInt("sortDirection");
            if (sortDirection == 0)
                sortDirection = WebHelper.GetQueryInt("sortDirection");
            //当前页数
            int page = GetRouteInt("page");
            if (page == 0)
                page = WebHelper.GetQueryInt("page");

            //分类信息
            CategoryInfo categoryInfo = Categories.GetCategoryById(cateId);
            if (categoryInfo == null)
                return PromptView("/", "此分类不存在");

            //分类关联品牌列表
            List<BrandInfo> brandList = Categories.GetCategoryBrandList(cateId);
            //分类筛选属性及其值列表
            List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>> cateAAndVList = Categories.GetCategoryFilterAAndVList();

            //分类价格范围列表
            string[] catePriceRangeList = StringHelper.SplitString(categoryInfo.PriceRange, "\r\n");

            //筛选属性处理
            List<int> attrValueIdList = new List<int>();
            string[] filterAttrValueIdList = StringHelper.SplitString(filterAttr, "-");
            if (filterAttrValueIdList.Length != cateAAndVList.Count)//当筛选属性和分类的筛选属性数目不对应时，重置筛选属性
            {
                if (cateAAndVList.Count == 0)
                {
                    filterAttr = "0";
                }
                else
                {
                    int count = cateAAndVList.Count;
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < count; i++)
                        sb.Append("0-");
                    filterAttr = sb.Remove(sb.Length - 1, 1).ToString();
                }
            }
            else
            {
                foreach (string attrValueId in filterAttrValueIdList)
                {
                    int temp = TypeHelper.StringToInt(attrValueId);
                    if (temp > 0) attrValueIdList.Add(temp);
                }
            }

            //分页对象
            PageModel pageModel = new PageModel(20, page, Products.GetCategoryProductCount(cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock));
            //视图对象
            CategoryModel model = new CategoryModel()
            {
                CateId = cateId,
                BrandId = brandId,
                FilterPrice = filterPrice,
                FilterAttr = filterAttr,
                OnlyStock = onlyStock,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                CategoryInfo = categoryInfo,
                BrandList = brandList,
                CatePriceRangeList = catePriceRangeList,
                AAndVList = cateAAndVList,
                PageModel = pageModel,
                ProductList = Products.GetCategoryProductList(pageModel.PageSize, pageModel.PageNumber, cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock, sortColumn, sortDirection)
            };

            return View(model);
        }

        /// <summary>
        /// 搜索
        /// </summary>
        public ActionResult Search()
        {
            //搜索词
            string keyword = WebHelper.GetQueryString("keyword");
            WorkContext.SearchWord = WebHelper.HtmlEncode(keyword);
            if (keyword.Length == 0)
                return PromptView(WorkContext.UrlReferrer, "请输入搜索词");
            if (!SecureHelper.IsSafeSqlString(keyword))
                return PromptView(WorkContext.UrlReferrer, "您搜索的商品不存在");

            //异步保存搜索历史
            Asyn.UpdateSearchHistory(WorkContext.Uid, keyword);

            //判断搜索词是否为分类名称，如果是则重定向到分类页面
            int cateId = Categories.GetCateIdByName(keyword);
            if (cateId > 0)
            {
                return Redirect(Url.Action("category", new RouteValueDictionary { { "cateId", cateId } }));
            }
            else
            {
                cateId = WebHelper.GetQueryInt("cateId");
            }

            //分类列表
            List<CategoryInfo> categoryList = null;
            //分类信息
            CategoryInfo categoryInfo = null;
            //品牌列表
            List<BrandInfo> brandList = null;

            //品牌id
            int brandId = Brands.GetBrandIdByName(keyword);
            if (brandId > 0)//当搜索词为品牌名称时
            {
                //获取品牌相关的分类
                categoryList = Brands.GetBrandCategoryList(brandId);

                //由于搜索结果的展示是以分类为基础的，所以当分类不存在时直接将搜索结果设为“搜索的商品不存在”
                if (categoryList.Count == 0)
                    return PromptView(WorkContext.UrlReferrer, "您搜索的商品不存在");

                if (cateId > 0)
                {
                    categoryInfo = Categories.GetCategoryById(cateId);
                }
                else
                {
                    //当没有进行分类的筛选时，将分类列表中的首项设为当前选中的分类
                    categoryInfo = categoryList[0];
                    cateId = categoryInfo.CateId;
                }
                brandList = new List<BrandInfo>();
                brandList.Add(Brands.GetBrandById(brandId));
            }
            else//当搜索词为商品关键词时
            {
                //获取商品关键词相关的分类
                categoryList = Searches.GetCategoryListByKeyword(keyword);

                //由于搜索结果的展示是以分类为基础的，所以当分类不存在时直接将搜索结果设为“搜索的商品不存在”
                if (categoryList.Count == 0)
                    return PromptView(WorkContext.UrlReferrer, "您搜索的商品不存在");

                if (cateId > 0)
                {
                    categoryInfo = Categories.GetCategoryById(cateId);
                }
                else
                {
                    categoryInfo = categoryList[0];
                    cateId = categoryInfo.CateId;
                }
                //根据商品关键词获取分类相关的品牌
                brandList = Searches.GetCategoryBrandListByKeyword(cateId, keyword);
                if (brandList.Count == 0)
                    return PromptView(WorkContext.UrlReferrer, "您搜索的商品不存在");
                brandId = WebHelper.GetQueryInt("brandId");
            }
            //最后再检查一遍分类是否存在
            if (categoryInfo == null)
                return PromptView(WorkContext.UrlReferrer, "您搜索的商品不存在");

            //筛选价格
            int filterPrice = WebHelper.GetQueryInt("filterPrice");
            //筛选属性
            string filterAttr = WebHelper.GetQueryString("filterAttr");
            //是否只显示有货
            int onlyStock = WebHelper.GetQueryInt("onlyStock");
            //排序列
            int sortColumn = WebHelper.GetQueryInt("sortColumn");
            //排序方向
            int sortDirection = WebHelper.GetQueryInt("sortDirection");
            //当前页数
            int page = WebHelper.GetQueryInt("page");

            //分类筛选属性及其值列表
            List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>> cateAAndVList = Categories.GetCategoryFilterAAndVList(cateId);
            //分类价格范围列表
            string[] catePriceRangeList = StringHelper.SplitString(categoryInfo.PriceRange, "\r\n");

            //筛选属性处理
            List<int> attrValueIdList = new List<int>();
            string[] filterAttrValueIdList = StringHelper.SplitString(filterAttr, "-");
            if (filterAttrValueIdList.Length != cateAAndVList.Count)//当筛选属性和分类的筛选属性数目不对应时，重置筛选属性
            {
                if (cateAAndVList.Count == 0)
                {
                    filterAttr = "0";
                }
                else
                {
                    int count = cateAAndVList.Count;
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < count; i++)
                        sb.Append("0-");
                    filterAttr = sb.Remove(sb.Length - 1, 1).ToString();
                }
            }
            else
            {
                foreach (string attrValueId in filterAttrValueIdList)
                {
                    int temp = TypeHelper.StringToInt(attrValueId);
                    if (temp > 0) attrValueIdList.Add(temp);
                }
            }


            //分页对象
            PageModel pageModel = new PageModel(20, page, Searches.GetSearchMallProductCount(keyword, cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock));
            //视图对象
            MallSearchModel model = new MallSearchModel()
            {
                Word = keyword,
                CateId = cateId,
                BrandId = brandId,
                FilterPrice = filterPrice,
                FilterAttr = filterAttr,
                OnlyStock = onlyStock,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                CategoryList = categoryList,
                //CategoryInfo = categoryInfo,
                BrandList = brandList,
                CatePriceRangeList = catePriceRangeList,
                AAndVList = cateAAndVList,
                PageModel = pageModel,
                ProductList = Searches.SearchMallProducts(pageModel.PageSize, pageModel.PageNumber, keyword, cateId, brandId, filterPrice, catePriceRangeList, attrValueIdList, onlyStock, sortColumn, sortDirection)
            };

            return View(model);
        }

        /// <summary>
        /// 活动专题
        /// </summary>
        public ActionResult Topic()
        {
            //专题id
            int topicId = GetRouteInt("topicId");
            if (topicId == 0)
                topicId = WebHelper.GetQueryInt("topicId");

            TopicInfo topicInfo = Topics.GetTopicById(topicId);
            if (topicInfo == null)
                return PromptView("此活动专题不存在");

            return View(topicInfo);
        }

        /// <summary>
        /// 商品评价列表
        /// </summary>
        public ActionResult ProductReviewList()
        {
            int pid = WebHelper.GetQueryInt("pid");
            int reviewType = WebHelper.GetQueryInt("reviewType");
            int page = WebHelper.GetQueryInt("page");

            //判断商品是否存在
            PartProductInfo productInfo = Products.GetPartProductById(pid);
            if (productInfo == null)
                return PromptView("/", "你访问的商品不存在");

            //店铺信息
            StoreInfo storeInfo = Stores.GetStoreById(productInfo.StoreId);
            if (storeInfo.State != (int)StoreState.Open)
                return PromptView("/", "你访问的商品不存在");

            if (reviewType < 0 || reviewType > 3) reviewType = 0;

            PageModel pageModel = new PageModel(10, page, ProductReviews.GetProductReviewCount(pid, reviewType));
            ProductReviewListModel model = new ProductReviewListModel()
            {
                ProductInfo = productInfo,
                CategoryInfo = Categories.GetCategoryById(productInfo.CateId),
                BrandInfo = Brands.GetBrandById(productInfo.BrandId),
                StoreInfo = storeInfo,
                StoreKeeperInfo = Stores.GetStoreKeeperById(storeInfo.StoreId),
                StoreRegion = Regions.GetRegionById(storeInfo.RegionId),
                StoreRankInfo = StoreRanks.GetStoreRankById(storeInfo.StoreRid),
                ReviewType = reviewType,
                PageModel = pageModel,
                ProductReviewList = ProductReviews.GetProductReviewList(pid, reviewType, pageModel.PageSize, pageModel.PageNumber)
            };

            return View(model);
        }

        /// <summary>
        /// 商品评价列表
        /// </summary>
        public ActionResult AjaxProductReviewList()
        {
            int pid = WebHelper.GetQueryInt("pid");
            int reviewType = WebHelper.GetQueryInt("reviewType");
            int page = WebHelper.GetQueryInt("page");

            if (reviewType < 0 || reviewType > 3) reviewType = 0;

            PageModel pageModel = new PageModel(10, page, ProductReviews.GetProductReviewCount(pid, reviewType));
            AjaxProductReviewListModel model = new AjaxProductReviewListModel()
            {
                Pid = pid,
                ReviewType = reviewType,
                PageModel = pageModel,
                ProductReviewList = ProductReviews.GetProductReviewList(pid, reviewType, pageModel.PageSize, pageModel.PageNumber)
            };

            return View(model);
        }

        /// <summary>
        /// 商品咨询列表
        /// </summary>
        public ActionResult ProductConsultList()
        {
            int pid = WebHelper.GetQueryInt("pid");
            int consultTypeId = WebHelper.GetQueryInt("consultTypeId");
            string consultMessage = WebHelper.GetQueryString("consultMessage");
            int page = WebHelper.GetQueryInt("page");

            //判断商品是否存在
            PartProductInfo productInfo = Products.GetPartProductById(pid);
            if (productInfo == null)
                return PromptView("/", "你访问的商品不存在");

            if (!SecureHelper.IsSafeSqlString(consultMessage))
                return PromptView(WorkContext.UrlReferrer, "您搜索的内容不存在");

            //店铺信息
            StoreInfo storeInfo = Stores.GetStoreById(productInfo.StoreId);
            if (storeInfo.State != (int)StoreState.Open)
                return PromptView("/", "你访问的商品不存在");

            PageModel pageModel = new PageModel(10, page, ProductConsults.GetProductConsultCount(pid, consultTypeId, consultMessage));
            ProductConsultListModel model = new ProductConsultListModel()
            {
                ProductInfo = productInfo,
                CategoryInfo = Categories.GetCategoryById(productInfo.CateId),
                BrandInfo = Brands.GetBrandById(productInfo.BrandId),
                StoreInfo = storeInfo,
                StoreKeeperInfo = Stores.GetStoreKeeperById(storeInfo.StoreId),
                StoreRegion = Regions.GetRegionById(storeInfo.RegionId),
                StoreRankInfo = StoreRanks.GetStoreRankById(storeInfo.StoreRid),
                ConsultTypeId = consultTypeId,
                ConsultMessage = consultMessage,
                PageModel = pageModel,
                ProductConsultList = ProductConsults.GetProductConsultList(pageModel.PageSize, pageModel.PageNumber, pid, consultTypeId, consultMessage),
                ProductConsultTypeList = ProductConsults.GetProductConsultTypeList(),
                IsVerifyCode = CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.MallConfig.VerifyPages)
            };

            return View(model);
        }

        /// <summary>
        /// 商品咨询列表
        /// </summary>
        public ActionResult AjaxProductConsultList()
        {
            int pid = WebHelper.GetQueryInt("pid");
            int consultTypeId = WebHelper.GetQueryInt("consultTypeId");
            string consultMessage = WebHelper.GetQueryString("consultMessage");
            int page = WebHelper.GetQueryInt("page");

            if (!SecureHelper.IsSafeSqlString(consultMessage))
                return View(new AjaxProductConsultListModel());

            PageModel pageModel = new PageModel(10, page, ProductConsults.GetProductConsultCount(pid, consultTypeId, consultMessage));
            AjaxProductConsultListModel model = new AjaxProductConsultListModel()
            {
                Pid = pid,
                ConsultTypeId = consultTypeId,
                ConsultMessage = consultMessage,
                PageModel = pageModel,
                ProductConsultList = ProductConsults.GetProductConsultList(pageModel.PageSize, pageModel.PageNumber, pid, consultTypeId, consultMessage),
                ProductConsultTypeList = ProductConsults.GetProductConsultTypeList(),
                IsVerifyCode = CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.MallConfig.VerifyPages)
            };

            return View(model);
        }

        /// <summary>
        /// 咨询商品
        /// </summary>
        public ActionResult ConsultProduct()
        {
            //不允许游客访问
            if (WorkContext.Uid < 1)
                return AjaxResult("nologin", "请先登录");

            //验证验证码
            if (CommonHelper.IsInArray(WorkContext.PageKey, WorkContext.MallConfig.VerifyPages))
            {
                string verifyCode = WebHelper.GetFormString("verifyCode");//验证码
                if (string.IsNullOrWhiteSpace(verifyCode))
                {
                    return AjaxResult("emptyverifycode", "验证码不能为空"); ;
                }
                else if (verifyCode.ToLower() != Sessions.GetValueString(WorkContext.Sid, "verifyCode"))
                {
                    return AjaxResult("wrongverifycode", "验证码错误"); ;
                }
            }

            int pid = WebHelper.GetFormInt("pid");
            int consultTypeId = WebHelper.GetFormInt("consultTypeId");
            string consultMessage = WebHelper.GetFormString("consultMessage");

            PartProductInfo partProductInfo = Products.GetPartProductById(pid);
            if (partProductInfo == null)
                return AjaxResult("noproduct", "请选择商品");

            StoreInfo storeInfo = Stores.GetStoreById(partProductInfo.StoreId);
            if (storeInfo.State != (int)StoreState.Open)
                return AjaxResult("noproduct", "请选择商品");

            if (consultTypeId < 1 || ProductConsults.GetProductConsultTypeById(consultTypeId) == null)
                return AjaxResult("noproductconsulttype", "请选择咨询类型"); ;

            if (string.IsNullOrWhiteSpace(consultMessage))
                return AjaxResult("noconsultmessage", "请填写咨询内容"); ;
            if (consultMessage.Length > 100)
                return AjaxResult("muchconsultmessage", "咨询内容内容太长"); ;
            if (!SecureHelper.IsSafeSqlString(consultMessage))
                return AjaxResult("dangerconsultmessage", "咨询内中包含非法字符"); ;

            ProductConsults.ConsultProduct(pid, consultTypeId, WorkContext.Uid, partProductInfo.StoreId, DateTime.Now, WebHelper.HtmlEncode(consultMessage), WorkContext.NickName, partProductInfo.Name, partProductInfo.ShowImg, WorkContext.IP);
            return AjaxResult("success", Url.Action("product", new RouteValueDictionary { { "pid", pid } })); ;
        }


        public ActionResult KeyWordSearch()
        {
            //搜索词
            string keyword = WebHelper.GetQueryString("keyword");
            WorkContext.SearchWord = WebHelper.HtmlEncode(keyword);
            if (keyword.Length == 0)
                return PromptView(WorkContext.UrlReferrer, "请输入搜索词");
            if (!SecureHelper.IsSafeSqlString(keyword))
                return PromptView(WorkContext.UrlReferrer, "您搜索的商品不存在");

            //异步保存搜索历史
            Asyn.UpdateSearchHistory(WorkContext.Uid, keyword);

            //获取当前搜索词匹配缓存结果
            ProductSearchKeyInfo keyInfo = BMACache.Get(keyword) as ProductSearchKeyInfo;
            if (keyInfo != null && string.IsNullOrEmpty(keyInfo.Name)) //无匹配
            {
                return PromptView(WorkContext.UrlReferrer, "您搜索的商品不存在");
            }

            if (keyInfo == null) //未匹配过
            {
                keyInfo = Searches.GetProductSearchKey(keyword);
                if (keyInfo == null || string.IsNullOrEmpty(keyInfo.Name))
                {
                    keyInfo = new ProductSearchKeyInfo()
                    {
                        Name = "",
                        keyType = -1,
                        ToId = -1
                    };
                }
                BMACache.Insert(keyword, keyInfo);
            }

            //再次判断搜索词匹配结果
            if (keyInfo != null && string.IsNullOrEmpty(keyInfo.Name)) //无匹配
            {
                return PromptView(WorkContext.UrlReferrer, "您搜索的商品不存在");
            }

            return Redirect(Url.Action("categorysearch", new RouteValueDictionary { { "keyword", keyword }, { "searchKeyType", keyInfo.keyType }, { "searchKeyId", keyInfo.ToId } }));
        }

        public ActionResult CategorySearch()
        {
            //搜索词
            string keyword = GetRouteString("keyword");
            if (string.IsNullOrEmpty(keyword))
                keyword = WebHelper.GetQueryString("keyword");
            //搜索关联匹配类型
            int searchKeyType = GetRouteInt("searchKeyType");
            if (searchKeyType == 0)
                searchKeyType = WebHelper.GetQueryInt("searchKeyType");
            //搜索关联匹配对对象id
            int searchKeyId = GetRouteInt("searchKeyId");
            if (searchKeyId == 0)
                searchKeyId = WebHelper.GetQueryInt("searchKeyId");

            //分类id
            int cateId = GetRouteInt("cateId");
            if (cateId == 0)
                cateId = WebHelper.GetQueryInt("cateId");
            //品牌id
            int brandId = GetRouteInt("brandId");
            if (brandId == 0)
                brandId = WebHelper.GetQueryInt("brandId");
            ////专场id
            //int specialId = GetRouteInt("specialId");
            //if (specialId == 0)
            //    specialId = WebHelper.GetQueryInt("specialId");
            //筛选属性
            string filterAttr = GetRouteString("filterAttr");
            if (filterAttr.Length == 0)
                filterAttr = WebHelper.GetQueryString("filterAttr");
            //是否只显示有货
            int onlyStock = GetRouteInt("onlyStock");
            if (onlyStock == 0)
                onlyStock = WebHelper.GetQueryInt("onlyStock");


            if (searchKeyType <= 0 || searchKeyId <= 0)
            {
                return PromptView(WorkContext.UrlReferrer, "您搜索的商品不存在");
            }


            //可选商品id列表
            List<int> productIdList = new List<int>();
            //可选分类列表
            List<CategoryInfo> categoryList = new List<CategoryInfo>();
            //选中的分类信息
            List<CategoryInfo> categoryInfo = new List<CategoryInfo>();
            //可选品牌列表
            List<BrandInfo> brandList = new List<BrandInfo>();
            //可选属性列表
            List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>> cateAAndVList = new List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>>();


            bool isValid = GetProductFliterList(ref productIdList, ref categoryList, ref categoryInfo, ref brandList, ref cateAAndVList,
                searchKeyType, searchKeyId, cateId, brandId, filterAttr, onlyStock);

            if (!isValid)
            {
                return PromptView(WorkContext.UrlReferrer, "您搜索的商品不存在");
            }
            if (categoryInfo == null || categoryInfo.Count == 0)
                return PromptView("/", "此分类不存在");

            //排序列
            int sortColumn = GetRouteInt("sortColumn");
            if (sortColumn == 0)
                sortColumn = WebHelper.GetQueryInt("sortColumn");
            //排序方向
            int sortDirection = GetRouteInt("sortDirection");
            if (sortDirection == 0)
                sortDirection = WebHelper.GetQueryInt("sortDirection");
            //当前页数
            int page = GetRouteInt("page");
            if (page == 0)
                page = WebHelper.GetQueryInt("page");



            ////筛选价格
            //int filterPrice = GetRouteInt("filterPrice");
            //if (filterPrice == 0)
            //    filterPrice = WebHelper.GetQueryInt("filterPrice");


            ////分类价格范围列表
            //string[] catePriceRangeList = StringHelper.SplitString(categoryInfo.PriceRange, "\r\n");


            //分页对象
            PageModel pageModel = new PageModel(20, page, Searches.GetSearchMallProductCount(productIdList));
            //视图对象
            MallSearchModel model = new MallSearchModel()
            {
                Word = (searchKeyType == (int)ProductKeyEnum.Category || searchKeyType == (int)ProductKeyEnum.Special) ? "" : keyword,
                SearchKeyType = searchKeyType,
                SearchKeyId = searchKeyId,
                CateId = cateId,
                BrandId = brandId,
                //FilterPrice = filterPrice,
                FilterAttr = filterAttr,
                OnlyStock = onlyStock,
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                CategoryList = categoryList,
                CategoryInfo = categoryInfo,
                BrandList = brandList,
                //CatePriceRangeList = catePriceRangeList,
                AAndVList = cateAAndVList,
                PageModel = pageModel,
                ProductList = Searches.SearchMallProducts(productIdList, pageModel.PageSize, pageModel.PageNumber, sortColumn, sortDirection)
            };

            return View("search", model);
        }

        public ActionResult FilterSearch()
        {
            return null;

        }

        /// <summary>
        /// 获取满足搜索条件的商品id列表
        /// </summary>
        /// <param name="searchKeyList">商品id列表</param>
        /// <param name="name">搜索词</param>
        /// <param name="keytype">搜索词类型</param>
        public bool GetProductFliterList(ref List<int> productIdList, ref List<CategoryInfo> categoryList, ref List<CategoryInfo> categoryInfo, ref List<BrandInfo> brandList, ref List<KeyValuePair<AttributeInfo, List<AttributeValueInfo>>> cateAAndVList,
            int searchKeyType, int searchKeyId, int cateId, int brandId, string filterAttr, int onlyStock)
        {
            //获取可选的商品分类品牌列表（1商品id,2分类id,3品牌id）
            var productCateBrandList = GetProductCateBrandList(searchKeyType, searchKeyId);
            if (productCateBrandList == null)
            {
                return false;
            }

            //获取可选的分类id列表
            var categoryIdList = productCateBrandList.Select(p => p.Item2).Distinct().ToList();
            var categoryAllList = Categories.GetCategoryList();

            //获取可选的分类信息列表
            categoryIdList.AddRange(categoryAllList.Where(p => categoryIdList.Contains(p.CateId)).Select(p => p.ParentId));
            categoryList = categoryAllList.FindAll(p => categoryIdList.Contains(p.CateId));
            //获取指定的树形分类信息
            if (cateId == 0)
            {
                cateId = categoryList.FirstOrDefault(p => p.HasChild == 0).CateId;
            }
            var _cateid = cateId;
            while (_cateid > 0)
            {
                var cateInfo = categoryAllList.FirstOrDefault(p => p.CateId == _cateid);
                if (cateInfo == null)
                {
                    break;
                }
                categoryInfo.Add(cateInfo);
                _cateid = cateInfo.ParentId;
            }
            categoryInfo.OrderBy(p => p.Layer);

            //获取可选的品牌列表
            var brandIdList = productCateBrandList.Where(p => p.Item2 == cateId).Select(p => p.Item3).Distinct().ToList();
            brandList = Brands.GetBrandListByIds(brandIdList);

            //获取可选的筛选属性及属性值列表
            var attrProductList = Searches.GetProductIdListByAttrId(0, 0, cateId, 0);
            var attrIdList = attrProductList.Select(p => p.AttrId).Distinct().ToList();
            var attrValueIdList = attrProductList.Select(p => p.AttrValueId).Distinct().ToList();
            var attrList = Categories.GetFilterAttributeListByIds(attrIdList);
            foreach (var item in attrList)
            {
                var attrValueList = Categories.GetAttributeValueListByIds(item.AttrId, attrValueIdList);
                cateAAndVList.Add(new KeyValuePair<AttributeInfo, List<AttributeValueInfo>>(item, attrValueList));
            }

            //获取筛选条件下的商品id列表
            var _productIdList = productCateBrandList.Where(p => p.Item2 == cateId && (brandId == 0 || p.Item3 == brandId)).Select(p => p.Item1).Distinct();
            if (string.IsNullOrEmpty(filterAttr))
            {
                string[] filterAttrValueIdList = StringHelper.SplitString(filterAttr, "-");
                if (filterAttrValueIdList.Length == cateAAndVList.Count)  //选定的属性应该可筛选属性数量一致
                {
                    List<int> pidforAttrList = attrProductList.Where(p => filterAttrValueIdList.Contains(p.AttrValueId.ToString())).Select(p => p.Pid).ToList();
                    _productIdList = _productIdList.Where(p => pidforAttrList.Contains(p));
                }

            }
            if (onlyStock == 1) //有货筛选
            {
                var stockProductIdList = Products.GetStockProductIdList();
                if (stockProductIdList != null && stockProductIdList.Count > 0)
                {
                    _productIdList = _productIdList.Where(p => stockProductIdList.Contains(p));
                }
            }
            productIdList = _productIdList.ToList();

            if (productIdList.Count==0)
            {
                return false;
            }

            #region //废弃
            //int cateSearchId = 0, brandSearchId = 0, specialSearchId = 0, keywordSearchId = 0, attrSearchId = 0, attrValueSearchId = 0; //分类id,品牌id,专场id,关键词id,属性id,属性值id
            //switch (searchKeyType)
            //{
            //    case (int)ProductKeyEnum.Category:
            //        {
            //            cateSearchId = searchKeyId;
            //            break;
            //        }
            //    case (int)ProductKeyEnum.Brand:
            //        {
            //            brandSearchId = searchKeyId;
            //            break;
            //        }
            //    case (int)ProductKeyEnum.Special:
            //        {
            //            specialSearchId = searchKeyId;
            //            break;
            //        }
            //    case (int)ProductKeyEnum.Attribute:
            //        {
            //            attrSearchId = searchKeyId;
            //            break;
            //        }
            //    case (int)ProductKeyEnum.AttributeValue:
            //        {
            //            attrValueSearchId = searchKeyId;
            //            break;
            //        }
            //    case (int)ProductKeyEnum.KeyWord:
            //        {
            //            keywordSearchId = searchKeyId;
            //            break;
            //        }
            //    default:
            //        {
            //            if (searchKeyId > 0)
            //            {
            //                return false;
            //            }
            //            break;
            //        }
            //}

            ////特殊情况：直接选择商品类型搜索,仅展示所选类型的类型结构树,提前限定,减少查询量
            //if (searchKeyType == 0 && cateId > 0)
            //{
            //    cateSearchId = cateId;
            //}

            ////获取商品关联信息
            //List<int> productSearchIdList = new List<int>();
            //var specialProductList = Searches.GetProductIdListBySpecialId(specialSearchId, cateSearchId, brandSearchId);
            //var keywordProductList = Searches.GetProductIdListByKeyId(keywordSearchId, cateSearchId, brandSearchId);
            //var attrProductList = Searches.GetProductIdListByAttrId(attrSearchId, attrValueSearchId, cateSearchId, brandSearchId);

            ////获取可选的商品id列表
            //if (specialProductList != null && specialProductList.Count > 0)
            //{
            //    productSearchIdList = productSearchIdList.Count == 0
            //        ? specialProductList.Select(p => p.Pid).ToList()
            //        : specialProductList.Select(p => p.Pid).Where(p => productSearchIdList.Contains(p)).ToList();
            //    if (productSearchIdList.Count == 0)
            //    {
            //        return false;
            //    }

            //    var ss = specialProductList.Where(p =>
            //                                        (p.SpecialId == specialId || specialId == 0) &&
            //                                        (p.CateId == cateId || cateId == 0) &&
            //                                        (p.BrandId == brandId || brandId == 0)
            //                                    ).Select(p => p.Pid).ToList();
            //    productIdList = productIdList.Count == 0 ? ss : productIdList.Where(p => ss.Contains(p)).ToList();
            //}
            //if (keywordProductList != null && keywordProductList.Count > 0)
            //{
            //    productSearchIdList = productSearchIdList.Count == 0
            //        ? keywordProductList.Select(p => p.Pid).ToList()
            //        : keywordProductList.Select(p => p.Pid).Where(p => productSearchIdList.Contains(p)).ToList();
            //    if (productSearchIdList.Count == 0)
            //    {
            //        return false;
            //    }

            //}
            //if (attrProductList != null && attrProductList.Count > 0)
            //{
            //    productSearchIdList = productSearchIdList.Count == 0
            //        ? attrProductList.Select(p => p.Pid).ToList()
            //        : attrProductList.Select(p => p.Pid).Where(p => productSearchIdList.Contains(p)).ToList();
            //    if (productSearchIdList.Count == 0)
            //    {
            //        return false;
            //    }

            //    string[] filterAttrValueIdList = StringHelper.SplitString(filterAttr, "-");
            //    var ss = attrProductList.Where(p =>
            //                                    (filterAttrValueIdList.Contains(p.AttrValueId.ToString()) || string.IsNullOrEmpty(filterAttr)) &&
            //                                    (p.CateId == cateId || cateId == 0) &&
            //                                    (p.BrandId == brandId || brandId == 0)
            //                                ).Select(p => p.Pid).ToList();
            //    productIdList = productIdList.Count == 0 ? ss : productIdList.Where(p => ss.Contains(p)).ToList();
            //}

            //productSearchIdList = productSearchIdList.Distinct().ToList();
            //productIdList = productIdList.Distinct().ToList();

            ////有货筛选
            //if (onlyStock == 1 && productIdList.Count > 0)
            //{
            //    var stockProductIdList = Products.GetStockProductIdList();
            //    if (stockProductIdList != null && stockProductIdList.Count > 0)
            //    {
            //        productIdList = productIdList.Where(p => stockProductIdList.Contains(p)).ToList();
            //    }
            //}

            ////获取可选的类型id列表
            //List<int> categoryIdList = new List<int>();

            //if (specialProductList != null && specialProductList.Count > 0)
            //{
            //    var productSelecttIds = specialProductList.Where(p => productSearchIdList.Contains(p.Pid));
            //    categoryIdList.AddRange(productSelecttIds.Select(p => p.CateId));
            //}
            //if (keywordProductList != null && keywordProductList.Count > 0)
            //{
            //    var productSelecttIds = keywordProductList.Where(p => productSearchIdList.Contains(p.Pid));
            //    categoryIdList.AddRange(productSelecttIds.Select(p => p.CateId));
            //}
            //if (specialProductList != null && specialProductList.Count > 0)
            //{
            //    var productSelecttIds = attrProductList.Where(p => productSearchIdList.Contains(p.Pid));
            //    categoryIdList.AddRange(productSelecttIds.Select(p => p.CateId));
            //}
            ////获取类型信息列表
            //if (categoryIdList.Count > 0)
            //{
            //    categoryIdList = categoryIdList.Distinct().ToList();
            //    categoryList = Categories.GetCategoryListByIds(categoryIdList);
            //    cateId = categoryList.FirstOrDefault(p => p.HasChild == 0).CateId;
            //}
            ////获取可选的类型id列表,品牌id列表,属性id列表,属性值id列表
            //List<int> brandIdList = new List<int>();
            //List<int> attrIdList = new List<int>();
            //List<int> attrValueIdList = new List<int>();
            //if (specialProductList != null && specialProductList.Count > 0)
            //{
            //    var productSelecttIds = specialProductList.Where(p => p.CateId == cateId && productSearchIdList.Contains(p.Pid));
            //    brandIdList.AddRange(productSelecttIds.Select(p => p.BrandId));
            //}
            //if (keywordProductList != null && keywordProductList.Count > 0)
            //{
            //    var productSelecttIds = keywordProductList.Where(p => p.CateId == cateId && productSearchIdList.Contains(p.Pid));
            //    brandIdList.AddRange(productSelecttIds.Select(p => p.BrandId));
            //}
            //if (specialProductList != null && specialProductList.Count > 0)
            //{
            //    var productSelecttIds = attrProductList.Where(p => p.CateId == cateId && productSearchIdList.Contains(p.Pid));
            //    brandIdList.AddRange(productSelecttIds.Select(p => p.BrandId));

            //    attrIdList.AddRange(productSelecttIds.Select(p => p.AttrId));
            //    attrValueIdList.AddRange(productSelecttIds.Select(p => p.AttrValueId));
            //}
            ////获取品牌信息列表，属性信息列表
            //if (brandIdList.Count > 0)
            //{
            //    brandIdList = brandIdList.Distinct().ToList();
            //    brandList = Brands.GetBrandListByIds(brandIdList);
            //}
            //if (attrIdList.Count > 0 && attrValueIdList.Count > 0)
            //{
            //    attrIdList = attrIdList.Distinct().ToList();
            //    attrValueIdList = attrValueIdList.Distinct().ToList();

            //    var attrList = Categories.GetFilterAttributeListByIds(attrIdList);

            //    foreach (var item in attrList)
            //    {
            //        var attrValueList = Categories.GetAttributeValueListByIds(item.AttrId, attrValueIdList);
            //        cateAAndVList.Add(new KeyValuePair<AttributeInfo, List<AttributeValueInfo>>(item, attrValueList));
            //    }
            //}

            #endregion

            return true;
        }

        public List<Tuple<int, int, int>> GetProductCateBrandList(int searchKeyType, int searchKeyId)
        {
            switch (searchKeyType)
            {
                case (int)ProductKeyEnum.Category:
                    {
                        var attrProductList = Searches.GetProductIdListByAttrId(0, 0, searchKeyId, 0);
                        if (attrProductList != null && attrProductList.Count > 0)
                        {
                            return attrProductList.Select(p => new Tuple<int, int, int>(p.Pid, p.CateId, p.BrandId)).ToList();
                        }
                        break;
                    }
                case (int)ProductKeyEnum.Brand:
                    {
                        var attrProductList = Searches.GetProductIdListByAttrId(0, 0, 0, searchKeyId);
                        if (attrProductList != null && attrProductList.Count > 0)
                        {
                            return attrProductList.Select(p => new Tuple<int, int, int>(p.Pid, p.CateId, p.BrandId)).ToList();
                        }
                        break;
                    }
                case (int)ProductKeyEnum.Special:
                    {
                        var specialProductList = Searches.GetProductIdListBySpecialId(searchKeyId, 0, 0);
                        if (specialProductList != null && specialProductList.Count > 0)
                        {
                            return specialProductList.Select(p => new Tuple<int, int, int>(p.Pid, p.CateId, p.BrandId)).ToList();
                        }
                        break;
                    }
                case (int)ProductKeyEnum.Attribute:
                    {
                        var attrProductList = Searches.GetProductIdListByAttrId(searchKeyId, 0, 0, 0);
                        if (attrProductList != null && attrProductList.Count > 0)
                        {
                            return attrProductList.Select(p => new Tuple<int, int, int>(p.Pid, p.CateId, p.BrandId)).ToList();
                        }
                        break;
                    }
                case (int)ProductKeyEnum.AttributeValue:
                    {
                        var attrProductList = Searches.GetProductIdListByAttrId(0, searchKeyId, 0, 0);
                        if (attrProductList != null && attrProductList.Count > 0)
                        {
                            return attrProductList.Select(p => new Tuple<int, int, int>(p.Pid, p.CateId, p.BrandId)).ToList();
                        }
                        break;
                    }
                case (int)ProductKeyEnum.KeyWord:
                    {
                        var keywordProductList = Searches.GetProductIdListByKeyId(searchKeyId, 0, 0);
                        if (keywordProductList != null && keywordProductList.Count > 0)
                        {
                            return keywordProductList.Select(p => new Tuple<int, int, int>(p.Pid, p.CateId, p.BrandId)).ToList();
                        }
                        break;
                    }
            }

            return null;

        }

    }
}
