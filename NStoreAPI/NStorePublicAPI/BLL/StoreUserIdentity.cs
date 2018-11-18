using System;
using System.Security.Claims;

namespace NStorePublicAPI.Bll
{
    public class StoreUserIdentity : ClaimsIdentity
    {
        public string Token { get; }

        public override bool IsAuthenticated => Token != null;

        public StoreUserIdentity(string tokenId)
        {
            Token = tokenId;
        }
    }
}