using NStore.Core;
using NStorePublicAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace NStorePublicAPI.Controllers
{
    [AuthorizeEx(Bll.AuthenticationType.Store)]
    [AuthorizeEx(Bll.AuthenticationType.MobileStore)]
    [AuthorizeEx(Bll.AuthenticationType.StoreAdmin)]
    [EnableCors("*", "*", "*")]
    public class BannedIPsController : ApiController
    {
        /// <summary>
        /// 获得禁止的ip列表
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(ResultInfo<HashSet<string>>))]
        [Route("api/GetBannedIPList")]
        [HttpGet]
        public ResultInfo<HashSet<string>> GetBannedIPList()
        {
            try
            {
                var ipList = NStore.Data.BannedIPs.GetBannedIPList();
                return new ResultInfo<HashSet<string>>
                {
                    Code = 0,
                    Msg = "",
                    Data = ipList
                };
            }
            catch (Exception ex)
            {
                return new ResultInfo<HashSet<string>>
                {
                    Code = 400,
                    Msg = ex.Message,
                    Data = null
                };
            }
        }

        /// <summary>
        /// 获得禁止的ip
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [ResponseType(typeof(ResultInfo<BannedIPInfo>))]
        [Route("api/GetBannedIPById/{id}")]
        [HttpGet]
        public ResultInfo<BannedIPInfo> GetBannedIPById(int id)
        {
            try
            {
                var result = NStore.Data.BannedIPs.GetBannedIPById(id);
                return new ResultInfo<BannedIPInfo>
                {
                    Code = 0,
                    Msg = "",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResultInfo<BannedIPInfo>
                {
                    Code = 400,
                    Msg = ex.Message,
                    Data = null
                };
            }
        }

        /// <summary>
        /// 获得禁止IP的id
        /// </summary>
        /// <param name="ip">ip</param>
        /// <returns></returns>
        [ResponseType(typeof(ResultInfo<int>))]
        [Route("api/GetBannedIPIdByIP/{ip}")]
        [HttpGet]
        public ResultInfo<int> GetBannedIPIdByIP(string ip)
        {
            try
            {
                var result = NStore.Data.BannedIPs.GetBannedIPIdByIP(ip);
                return new ResultInfo<int>
                {
                    Code = 0,
                    Msg = "",
                    Data = result
                };
            }
            catch(Exception ex)
            {
                return new ResultInfo<int>
                {
                    Code = 400,
                    Msg = ex.Message,
                    Data = 0
                };
            }
        }
    }
}