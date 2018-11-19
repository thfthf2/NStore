using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NStore.Core;
using NStore.Data;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Threading.Tasks;
using NStorePublicAPI.Models;
using System.Web.Http;

namespace NStorePublicAPI.Controllers
{
    [AuthorizeEx(Bll.AuthenticationType.Store)]
    [AuthorizeEx(Bll.AuthenticationType.MobileStore)]
    [AuthorizeEx(Bll.AuthenticationType.StoreAdmin)]
    [EnableCors("*", "*", "*")]
    public class AdvertsController : ApiController
    {
        /// <summary>
        /// 获得广告位置列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <returns></returns>
        [ResponseType(typeof(ResultInfo<List<AdvertPositionInfo>>))]
        [Route("api/GetAdvertPositionList/{pageSize}/{pageNumber}")]
        [HttpGet]
        [AllowAnonymous]
        public ResultInfo<List<AdvertPositionInfo>> GetAdvertPositionList(int pageSize, int pageNumber)
        {
            try
            {
                var result = NStore.Data.Adverts.GetAdvertPositionList(pageSize, pageNumber);
                return new ResultInfo<List<AdvertPositionInfo>>()
                {
                    Code = 0,
                    Msg = "",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResultInfo<List<AdvertPositionInfo>>
                {
                    Code = 400,
                    Msg = ex.Message,
                    Data = null
                };
            }
        }

        /// <summary>
        /// 获得广告位置数量
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(ResultInfo<int>))]
        [Route("api/GetAdvertPositionCount")]
        [HttpGet]
        [AllowAnonymous]
        public ResultInfo<int> GetAdvertPositionCount()
        {
            try
            {
                var count = NStore.Data.Adverts.GetAdvertPositionCount();
                return new ResultInfo<int>()
                {
                    Code = 0,
                    Msg = "",
                    Data = count
                };
            }
            catch (Exception ex)
            {
                return new ResultInfo<int>
                {
                    Code = 400,
                    Msg = ex.Message,
                    Data = 0
                };
            }
        }

        /// <summary>
        /// 获得全部广告位置
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(ResultInfo<List<AdvertPositionInfo>>))]
        [Route("api/GetAllAdvertPosition")]
        [HttpGet]
        [AllowAnonymous]
        public ResultInfo<List<AdvertPositionInfo>> GetAllAdvertPosition()
        {
            try
            {
                var result = NStore.Data.Adverts.GetAllAdvertPosition();
                return new ResultInfo<List<AdvertPositionInfo>>()
                {
                    Code = 0,
                    Msg = "",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResultInfo<List<AdvertPositionInfo>>
                {
                    Code = 400,
                    Msg = ex.Message,
                    Data = null
                };
            }
        }

        /// <summary>
        /// 获得广告位置
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        /// <returns></returns>
        [ResponseType(typeof(ResultInfo<AdvertPositionInfo>))]
        [Route("api/GetAdvertPositionById/{adPosId}")]
        [HttpGet]
        [AllowAnonymous]
        public ResultInfo<AdvertPositionInfo> GetAdvertPositionById(int adPosId)
        {
            try
            {
                AdvertPositionInfo result = null;
                if (adPosId > 0)
                    result = NStore.Data.Adverts.GetAdvertPositionById(adPosId);
                return new ResultInfo<AdvertPositionInfo>()
                {
                    Code = 0,
                    Msg = "",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResultInfo<AdvertPositionInfo>
                {
                    Code = 400,
                    Msg = ex.Message,
                    Data = null
                };
            }
        }

        /// <summary>
        /// 获得广告列表
        /// </summary>
        /// <param name="adPosId">广告位置id</param>
        /// <returns></returns>
        [ResponseType(typeof(ResultInfo<List<AdvertInfo>>))]
        [Route("api/GetAdvertList/{adPosId}")]
        [HttpGet]
        [AllowAnonymous]
        public ResultInfo<List<AdvertInfo>> GetAdvertList(int adPosId)
        {
            try
            {
                List<AdvertInfo> advertList = NStore.Data.Adverts.GetAdvertList(adPosId, DateTime.Now);
                return new ResultInfo<List<AdvertInfo>>()
                {
                    Code = 0,
                    Msg = "",
                    Data = advertList
                };
            }
            catch (Exception ex)
            {
                return new ResultInfo<List<AdvertInfo>>
                {
                    Code = 400,
                    Msg = ex.Message,
                    Data = null
                };
            }
        }
    }
}