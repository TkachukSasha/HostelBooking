using Hostel.Security.Application.Dto;

namespace Hostel.Security.Application.Common.Repositories
{
    public interface ITokenStorage
    {
        void Set(JwtDto jwt);
        JwtDto Get();
    }
}
