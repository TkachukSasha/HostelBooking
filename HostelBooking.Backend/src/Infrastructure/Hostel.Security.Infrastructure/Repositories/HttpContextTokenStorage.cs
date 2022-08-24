using Hostel.Security.Application.Common.Repositories;
using Hostel.Security.Application.Dto;
using Microsoft.AspNetCore.Http;

namespace Hostel.Security.Infrastructure.Repositories
{
    public class HttpContextTokenStorage : ITokenStorage
    {
        private const string TokenKey = "jwt";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextTokenStorage(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public JwtDto Get()
        {
            if (_httpContextAccessor.HttpContext is null)
            {
                return null;
            }

            if (_httpContextAccessor.HttpContext.Items.TryGetValue(TokenKey, out var jwt))
            {
                return jwt as JwtDto;
            }

            return null;
        }

        public void Set(JwtDto jwt) => _httpContextAccessor.HttpContext.Items?.TryAdd(TokenKey, jwt);
    }
}
