using System;
using System.Collections.Generic;

using NStore.Core;
using NStore.Services;
using NStore.Web.Framework;

namespace NStore.Web.Models
{
    /// <summary>
    /// 问题模型类
    /// </summary>
    public class QuestionModel
    {
        public HelpInfo HelpInfo { get; set; }
        public List<HelpInfo> HelpList { get; set; }
    }
}