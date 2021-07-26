using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebAPI.Identity.Attribute
{
    public class CustomAuthorization
    {

        public static bool ValidateUserClaim(HttpContext context,
                                                string claimName,
                                                string claimValue)
        => context.User.Identity.IsAuthenticated &&
           context.User.Claims.Any(claim => claim.Type == claimName &&
                                         claim.Value.Contains(claimValue));
    }
}
