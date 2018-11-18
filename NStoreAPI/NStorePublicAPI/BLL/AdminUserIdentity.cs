using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NStorePublicAPI.Bll
{
    public class AdminUserIdentity : ClaimsIdentity
    {
        public override bool IsAuthenticated => true;
    }
}
