using System;
using System.Collections.Generic;

using NStore.Core;

namespace NStore.Web.Models
{
    /// <summary>
    /// 登陆模型类
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// 返回地址
        /// </summary>
        public string ReturnUrl { get; set; }
        /// <summary>
        /// 影子账号名
        /// </summary>
        public string ShadowName { get; set; }
        /// <summary>
        /// 是否允许记住用户
        /// </summary>
        public bool IsRemember { get; set; }
        /// <summary>
        /// 是否启用验证码
        /// </summary>
        public bool IsVerifyCode { get; set; }
        /// <summary>
        /// 开放授权插件
        /// </summary>
        public List<PluginInfo> OAuthPluginList { get; set; }
    }

    /// <summary>
    /// 注册模型类
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// 返回地址
        /// </summary>
        public string ReturnUrl { get; set; }
        /// <summary>
        /// 影子账号名
        /// </summary>
        public string ShadowName { get; set; }
        ///// <summary>
        ///// 注册用户类型（0个人,1企业）
        ///// </summary>
        //public int IsEnterprise { get; set; }
        /// <summary>
        /// 是否启用验证码
        /// </summary>
        public bool IsVerifyCode { get; set; }

    }

    /// <summary>
    /// 认证模型类
    /// </summary>
    public class Authentication
    {
        /// <summary>
        /// 影子账号名
        /// </summary>
        public string ShadowName { get; set; }
        /// <summary>
        /// 返回地址
        /// </summary>
        public string ReturnUrl { get; set; }

        ///// <summary>
        ///// 手机号
        ///// </summary>
        //public string Mobile { get; set; }
        ///// <summary>
        ///// 邮箱
        ///// </summary>
        //public string Email { get; set; }
        ///// <summary>
        ///// 公司名称
        ///// </summary>
        //public string CompanyName { get; set; }
        ///// <summary>
        ///// 联系人
        ///// </summary>
        //public string LinkName { get; set; }
        ///// <summary>
        ///// 信用码
        ///// </summary>
        //public string CreditCode { get; set; }
        ///// <summary>
        ///// 营业执照
        ///// </summary>
        //public string BusinessLicense { get; set; }
        /// <summary>
        /// 注册用户类型（0个人,1企业）
        /// </summary>
        public int UserType { get; set; }
        /// <summary>
        /// 是否启用验证码
        /// </summary>
        public bool IsVerifyCode { get; set; }

    }

    /// <summary>
    /// 找回密码模型类
    /// </summary>
    public class FindPwdModel
    {
        /// <summary>
        /// 影子账号名
        /// </summary>
        public string ShadowName { get; set; }
        /// <summary>
        /// 是否启用验证码
        /// </summary>
        public bool IsVerifyCode { get; set; }
    }

    /// <summary>
    /// 选择找回密码方式模型类
    /// </summary>
    public class SelectFindPwdTypeModel
    {
        public PartUserInfo PartUserInfo { get; set; }
    }

    /// <summary>
    /// 重置密码模型类
    /// </summary>
    public class ResetPwdModel
    {
        public string V { get; set; }
    }
}