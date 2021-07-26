using Core.WebAPI.User.Extensions;
using Core.WebAPI.User.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebAPI.User
{
    public class User : IUser
    {

        private readonly IHttpContextAccessor _accessor;

        public string Name =>
                    _accessor.HttpContext.User.Identity.Name;

        public User(IHttpContextAccessor accessor)
        {
            this._accessor = accessor;
        }

        public IEnumerable<Claim> GetClaims()
                            => _accessor.HttpContext.User.Claims;

        public HttpContext GetHttpContext()
                            => _accessor.HttpContext;

        public Guid GetUserId()
                   => IsAuthenticated() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;

        public string GetUserEmail()
                   => IsAuthenticated() ? _accessor.HttpContext.User.GetUserEmail() : "";

        public string GetUserRefreshToken()
                    => IsAuthenticated() ? _accessor.HttpContext.User.GetUserRefreshToken() : "";

        public string GetUserToken()
                    => IsAuthenticated() ? _accessor.HttpContext.User.GetUserToken() : "";

        public bool HasRole(string role)
                    => _accessor.HttpContext.User.IsInRole(role);

        public bool IsAuthenticated()
                    => _accessor.HttpContext.User.Identity.IsAuthenticated;
    }
}
