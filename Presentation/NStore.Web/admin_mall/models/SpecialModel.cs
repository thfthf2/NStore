using NStore.Web.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace NStore.Web.MallAdmin.models
{

    /// <summary>
    /// 专场列表模型类
    /// </summary>
    public class SpecialListModel
    {
        public PageModel PageModel { get; set; }
        public DataTable SpecialList { get; set; }
        public string SortColumn { get; set; }
        public string SortDirection { get; set; }
        /// <summary>
        /// 专场名称
        /// </summary>
        public string SpecialName { get; set; }
    }

    public class SpecialModel
    {
        /// <summary>
        /// 专场名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空")]
        [StringLength(10, ErrorMessage = "名称长度不能大于8")]
        public string SpecialName { get; set; }
        /// <summary>
        /// 专场排序
        /// </summary>
        [DisplayName("排序")]
        public int DisplayOrder { get; set; }
        /// <summary>
        /// 专场状态
        /// </summary>
        [DisplayName("状态")]
        public int State { get; set; }
    }
}