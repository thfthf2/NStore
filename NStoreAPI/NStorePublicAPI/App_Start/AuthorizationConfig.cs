using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using NStorePublicAPI.Bll;


namespace NStorePublicAPI
{
    internal class AuthorizationFilter : IAuthorizationFilter
    {
        public bool AllowMultiple => false;

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken,
            Func<Task<HttpResponseMessage>> continuation)
        {
            //若允许匿名访问，则直接跳过权限检查，即直接允许
            var skip = SkipAuthorization(actionContext);
            if (!skip)
            {
                //若目标action未被注解授权相关特性，则直接跳过权限检查，即直接允许
                var attributes = ExtractAuthorizeExxAttributes(actionContext);
                if (attributes?.Any() == true)
                {
                    //解析各个注解
                    var annotations = Resolve(attributes);
                    //通过注解进行权限验证
                    Authorize(actionContext, annotations);

                    //若权限验证过程已设置响应结果，则将此结果直接作为最终的请求结果而返回
                    if (actionContext.Response != null)
                    {
                        return Task.FromResult(actionContext.Response);
                    }
                }
            }
            //未被拒绝权限，进行请求的剩余步骤，最终可能进入action
            return continuation();
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            return actionContext?.ActionDescriptor?.GetCustomAttributes<AllowAnonymousAttribute>().Any() == true
                   || actionContext?.ControllerContext?.ControllerDescriptor?.GetCustomAttributes<AllowAnonymousAttribute>().Any() == true;
        }

        private static ICollection<AuthorizeExAttribute> ExtractAuthorizeExxAttributes(HttpActionContext actionContext)
        {
            ICollection<AuthorizeExAttribute> AuthorizeExxAttributes = null;
            var methodAttributes = actionContext.ActionDescriptor.GetCustomAttributes<AuthorizeExAttribute>();
            if (methodAttributes?.Any() == true)
            {
                //验证方法级
                AuthorizeExxAttributes = methodAttributes;
            }
            else
            {
                var classAttributes = actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AuthorizeExAttribute>();
                if (classAttributes?.Any() == true)
                {
                    //验证类级
                    AuthorizeExxAttributes = classAttributes;
                }
            }
            return AuthorizeExxAttributes;
        }

        private static KeyValuePair<AuthenticationType, object[]>[] Resolve(IEnumerable<AuthorizeExAttribute> attributes)
        {
            return attributes
                ?.Select(a => new KeyValuePair<AuthenticationType, object[]>(a.AuthenticationType, a.Permissions))
                .ToArray();
        }

        private static void Authorize(HttpActionContext actionContext, KeyValuePair<AuthenticationType, object[]>[] pairs)
        {
            if (!pairs.Select(p => p.Key.ToString()).Contains(actionContext.Request?.Headers?.Authorization?.Scheme))
            {
                //返回授权错误
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "验证类型错误");
            }
            else if (actionContext.RequestContext?.Principal?.Identity?.IsAuthenticated != true)
            {
                //返回授权错误
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "用户未通过验证");
            }
            else
            {
                var principal = actionContext.RequestContext?.Principal as UserPrincipal;
                var Permissions = pairs.Where(p => p.Value != null).SelectMany(v => v.Value);
                if (Permissions.Any(p => principal?.HasPermission(p) != true))
                {
                    //返回授权错误
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "用户无权限");
                }
            }
        }
    }

    /// <summary>
    /// 同级允许多个特性
    /// 方法级覆盖类级的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    internal class AuthorizeExAttribute : Attribute
    {
        public AuthenticationType AuthenticationType { get; }
        public object[] Permissions { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationType">必须匹配此处指定的验证类型才可授权</param>
        public AuthorizeExAttribute(AuthenticationType authenticationType)
        {
            AuthenticationType = authenticationType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationType">必须匹配此处指定的验证类型才可授权</param>
        /// <param name="permissions">必须拥有此处指定的权限集才可授权</param>
        public AuthorizeExAttribute(AuthenticationType authenticationType, params object[] permissions)
            : this(authenticationType)
        {
            Permissions = permissions;
        }
    }



    internal static class AuthorizationConfig
    {
        internal static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new AuthorizationFilter());
        }
    }
}