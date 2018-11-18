using NStore.Core;
using NStorePublicAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace NStorePublicAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class BannersController : ApiController
    {
        /// <summary>
        /// 获得首页banner列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        [ResponseType(typeof(ResultInfo<BannerInfo[]>))]
        [Route("api/GetHomeBannerList/{type}")]
        [HttpGet]
        public ResultInfo<BannerInfo[]> GetHomeBannerList(int type)
        {
            try
            {
                var bannerList = NStore.Data.Banners.GetHomeBannerList(type, DateTime.Now);
                return new ResultInfo<BannerInfo[]>
                {
                    Code = 0,
                    Msg = string.Empty,
                    Data = bannerList
                };
            }
            catch(Exception ex)
            {
                return new ResultInfo<BannerInfo[]>
                {
                    Code = 400,
                    Msg = ex.Message,
                    Data = null
                };
            }
        }
    }
}