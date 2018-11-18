using System;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using NStorePublicAPI.Bll;


namespace NStorePublicAPI
{
    internal class AuthenticationFilter : IAuthenticationFilter
    {
        Task IAuthenticationFilter.AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return Task.Factory.StartNew(() =>
            {
                AuthenticationType authenticationType;
                if (Enum.TryParse(context.Request?.Headers?.Authorization?.Scheme, out authenticationType))
                {
                    context.Principal = UserPrincipal.Create(authenticationType, context.Request?.Headers?.Authorization?.Parameter);
                }
            }, cancellationToken);
        }


        Task IAuthenticationFilter.ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public bool AllowMultiple => true;
    }



    internal static class AuthenticationConfig
    {
        internal static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new AuthenticationFilter());
        }
    }
}