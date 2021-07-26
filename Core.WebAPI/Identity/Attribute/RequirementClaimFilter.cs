using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebAPI.Identity.Attribute
{
    public class RequirementClaimFilter:IAuthorizationFilter
    {
        private readonly Claim _claim;

        public RequirementClaimFilter(Claim claim)
        {
            this._claim = claim;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new StatusCodeResult(401);
                return;
            }

            if (CustomAuthorization.ValidateUserClaim(context.HttpContext,
                                                        _claim.Type,
                                                        _claim.Value))
                context.Result = new StatusCodeResult(403);
        }
    }
}
