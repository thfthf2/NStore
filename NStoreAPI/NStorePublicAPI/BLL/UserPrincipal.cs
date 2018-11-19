using System.Security.Claims;


namespace NStorePublicAPI.Bll
{
    public class UserPrincipal : ClaimsPrincipal
    {
        public static UserPrincipal Create(AuthenticationType authenticationType, string parameter)
        {
            switch (authenticationType)
            {
                case AuthenticationType.Store:
                case AuthenticationType.MobileStore:
                    {
                        if (!string.IsNullOrEmpty(parameter))
                        {
                            var identity = new StoreUserIdentity(parameter);
                            var principal = new UserPrincipal(identity);

                            return principal;
                        }
                        break;
                    }
                case AuthenticationType.StoreAdmin:
                    {
                        var identity = new AdminUserIdentity();
                        var principal = new UserPrincipal(identity);
                        return principal;
                    }
            }
            return null;
        }

        private UserPrincipal(ClaimsIdentity identity) : base(new[] { identity })
        {
        }

        public bool HasPermission(object permission)
        {
            if (Identity != null)
            {
                switch (Identity.GetType().Name)
                {
                    case nameof(StoreUserIdentity):
                        {
                            return true;
                        }
                    case nameof(AdminUserIdentity):
                        {
                            return true;
                        }
                }
            }
            return false;
        }
    }
}
