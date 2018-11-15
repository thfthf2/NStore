using System;
using System.Text;
using System.Drawing;
using System.Web.Mvc;
using System.Collections.Generic;

using NStore.Core;
using NStore.Services;
using NStore.Web.Framework;

namespace NStore.Web.Controllers
{
    /// <summary>
    /// 工具控制器类
    /// </summary>
    public partial class ToolController : Controller
    {
        /// <summary>
        /// 验证图片
        /// </summary>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <returns></returns>
        public ImageResult VerifyImage(int width = 56, int height = 20)
        {
            //获得用户唯一标示符sid
            string sid = MallUtils.GetSidCookie();
            //当sid为空时
            if (sid == null)
            {
                //生成sid
                sid = Sessions.GenerateSid();
                //将sid保存到cookie中
                MallUtils.SetSidCookie(sid);
            }

            //生成验证值
            string verifyValue = Randoms.CreateRandomValue(4, false).ToLower();
            //生成验证图片
            RandomImage verifyImage = Randoms.CreateRandomImage(verifyValue, width, height, Color.White, Color.Blue, Color.DarkRed);
            //将验证值保存到session中
            Sessions.SetItem(sid, "verifyCode", verifyValue);

            //输出验证图片
            return new ImageResult(verifyImage.Image, verifyImage.ContentType);
        }

        /// <summary>
        /// 省列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ProvinceList()
        {
            List<RegionInfo> regionList = Regions.GetProvinceList();

            StringBuilder sb = new StringBuilder();

            sb.Append("[");

            foreach (RegionInfo info in regionList)
            {
                sb.AppendFormat("{0}\"id\":\"{1}\",\"name\":\"{2}\"{3},", "{", info.RegionId, info.Name, "}");
            }

            if (regionList.Count > 0)
                sb.Remove(sb.Length - 1, 1);

            sb.Append("]");

            return AjaxResult("success", sb.ToString(), true);
        }

        /// <summary>
        /// 市列表
        /// </summary>
        /// <param name="provinceId">省id</param>
        /// <returns></returns>
        public ActionResult CityList(int provinceId = -1)
        {
            List<RegionInfo> regionList = Regions.GetCityList(provinceId);

            StringBuilder sb = new StringBuilder();

            sb.Append("[");

            foreach (RegionInfo info in regionList)
            {
                sb.AppendFormat("{0}\"id\":\"{1}\",\"name\":\"{2}\"{3},", "{", info.RegionId, info.Name, "}");
            }

            if (regionList.Count > 0)
                sb.Remove(sb.Length - 1, 1);

            sb.Append("]");

            return AjaxResult("success", sb.ToString(), true);
        }

        /// <summary>
        /// 县或区列表
        /// </summary>
        /// <param name="cityId">市id</param>
        /// <returns></returns>
        public ActionResult CountyList(int cityId = -1)
        {
            List<RegionInfo> regionList = Regions.GetCountyList(cityId);

            StringBuilder sb = new StringBuilder();

            sb.Append("[");

            foreach (RegionInfo info in regionList)
            {
                sb.AppendFormat("{0}\"id\":\"{1}\",\"name\":\"{2}\"{3},", "{", info.RegionId, info.Name, "}");
            }

            if (regionList.Count > 0)
                sb.Remove(sb.Length - 1, 1);

            sb.Append("]");

            return AjaxResult("success", sb.ToString(), true);
        }

        /// <summary>
        /// ajax请求结果
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        protected ActionResult AjaxResult(string state, string content)
        {
            return AjaxResult(state, content, false);
        }

        /// <summary>
        /// ajax请求结果
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="content">内容</param>
        /// <param name="isObject">是否为对象</param>
        /// <returns></returns>
        protected ActionResult AjaxResult(string state, string content, bool isObject)
        {
            return Content(string.Format("{0}\"state\":\"{1}\",\"content\":{2}{3}{4}{5}", "{", state, isObject ? "" : "\"", content, isObject ? "" : "\"", "}"));
        }

        public ActionResult AjaxUpload()
        {
            string Path = WebHelper.GetQueryString("path"); //原图片地址
            string NewPath = WebHelper.GetQueryString("newpath"); //替换的新图片存储路径,eg: "/images/active/";

            if (string.IsNullOrEmpty(NewPath))
            {
                return AjaxResult("fail", "文件上传错误", false);
            }
            string imgPathStr = "";
            try
            {
                string dirpath = System.Web.HttpContext.Current.Server.MapPath(NewPath);
                if (!System.IO.Directory.Exists(dirpath))
                {
                    System.IO.Directory.CreateDirectory(dirpath);
                }
                imgPathStr = UploadImage(NewPath);
                string[] imgPathArr = imgPathStr.Split(',');
                if (!string.IsNullOrEmpty(imgPathStr) && imgPathArr.Length > 0)
                {
                    for (int i = 0; i < imgPathArr.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(Path))
                        {
                            Path = System.Web.HttpContext.Current.Server.MapPath(Path);
                            if (System.IO.File.Exists(Path))
                            {
                                System.IO.File.Delete(Path);
                            }
                        }
                    }

                }
            }
            catch (Exception)
            {
                return AjaxResult("fail", "文件上传失败", false);
            }

            return AjaxResult("success", imgPathStr, false);
        }

        /// <summary> 
        /// 上传图片 
        /// </summary> 
        /// <returns>图片保存路径，多个之间,隔开</returns> 
        public static string UploadImage(string pathFolder)
        {
            string res = "";
            try
            {
                Random rand = new Random();

                foreach (string f in System.Web.HttpContext.Current.Request.Files.AllKeys)
                {
                    System.Web.HttpPostedFile pfile = System.Web.HttpContext.Current.Request.Files[f];
                    if (pfile.ContentLength > 0)
                    {
                        //上传图片
                        string houzhui = ".jpg";
                        string ctype = pfile.ContentType;
                        if (ctype == "image/jpeg" || ctype == "image/jpg")
                        {
                            houzhui = ".jpg";
                        }
                        else if (ctype == "image/png")
                        {
                            houzhui = ".png";
                        }
                        else if (ctype == "image/gif")
                        {
                            houzhui = ".gif";
                        }
                        else if (ctype == "image/bmp")
                        {
                            houzhui = ".bmp";
                        }
                        //获取文件名
                        string fileName = Convert.ToString(new System.IO.FileInfo(pfile.FileName));
                        // 取文件后缀名
                        //string houzui = new FileInfo(pfile.FileName).Extension;
                        //获取.位置
                        int dian = fileName.LastIndexOf(".");//获取文件点的索引位置
                        //创建新的名字，防止重名
                        string newname = "";
                        if (fileName.Contains("."))
                        {
                            newname = GetPicNO(rand) + fileName.Substring(dian, fileName.Length - dian);
                        }
                        else
                        {
                            newname = GetPicNO(rand) + houzhui;
                        }
                        //获取uploas.ashx所在文件相对路径
                        string path = pathFolder + newname;
                        //string spath = "/Upload/head/s" + newname;

                        // 保存文件到根目录下的upload目录中
                        string savePath = System.Web.HttpContext.Current.Server.MapPath(path);
                        pfile.SaveAs(savePath);
                        ////转换成缩略图
                        //string originalImagePath = Server.MapPath(path);
                        //string thumbnailPath = Server.MapPath(spath);
                        //System.Drawing.Image originalImage = System.Drawing.Image.FromFile(System.Web.HttpContext.Current.Server.MapPath(path));
                        //int ow = originalImage.Width;
                        //int oh = originalImage.Height;
                        //int width, height;
                        //if (ow > oh)
                        //{
                        //    width = oh;
                        //    height = oh;
                        //}
                        //else
                        //{
                        //    width = ow;
                        //    height = ow;
                        //}

                        //ImageHelper.MakeThumbnail(originalImagePath, thumbnailPath, width, height, "Cut");
                        res += path;
                    }

                    res += ",";
                }

            }
            catch
            {
                //Response.Write("Error");
            }
            res = res.Trim(',');
            return res;

        }

        /// <summary>
        /// 得到图片名称编号
        /// </summary>
        /// <returns></returns>
        private static string GetPicNO(Random rand)
        {
            return DateTime.Now.ToString("yyMMdd") + DateTime.Now.ToString("HHmmss") + new Random(Guid.NewGuid().GetHashCode()).Next(100, 999);
        }
    }
}
