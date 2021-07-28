using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebAPI.Identity.Attribute
{
    public class ClaimsAuthorizeAttribute:TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimType,string claimValue)
            :base(typeof(RequirementClaimFilter))
        {           
            Arguments = new object[] { new Claim(claimType, claimValue) };
        }

    }
}
