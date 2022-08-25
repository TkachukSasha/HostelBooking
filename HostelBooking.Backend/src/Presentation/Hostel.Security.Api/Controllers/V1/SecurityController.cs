using Hostel.Security.Application.Commands.Login;
using Hostel.Security.Application.Commands.Refresh;
using Hostel.Security.Application.Commands.Register;
using Hostel.Security.Application.Common.Repositories;
using Hostel.Security.Application.Dto;
using Hostel.Security.Domain.Repositories;
using Hostel.Shared.Types;
using Hostel.Shared.Types.Const;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hostel.Security.Api.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {

        private readonly ICommandHandler<SignUp> _signUpHandler;
        private readonly ICommandHandler<SignIn> _signInHandler;
        private readonly ICommandHandler<Refresh> _refreshHandler;
        private readonly ITokenStorage _tokenStorage;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public SecurityController(
            ICommandHandler<SignUp> signUpHandler,
            ICommandHandler<SignIn> signInHandler,
            ICommandHandler<Refresh> refreshHandler,
            ITokenStorage tokenStorage,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _signUpHandler = signUpHandler;
            _signInHandler = signInHandler;
            _refreshHandler = refreshHandler;
            _tokenStorage = tokenStorage;
            _refreshTokenRepository = refreshTokenRepository;
        }

        [AllowAnonymous]
        [Route(Routes.Login)]
        [HttpPost]
        public async Task<ActionResult<JwtDto>> Login([FromBody] SignIn command, CancellationToken token)
        {
            await _signInHandler.HandleAsync(command, token);
            var jwt = _tokenStorage.Get();
            return jwt;
        }

        [AllowAnonymous]
        [Route(Routes.Register)]
        [HttpPost]
        public async Task<ActionResult<JwtDto>> Register([FromBody] SignUp command, CancellationToken token)
        {
            await _signUpHandler.HandleAsync(command, token);
            var jwt = _tokenStorage.Get();
            return jwt;
        }

        [Route(Routes.Refresh)]
        [HttpPost]
        public async Task<ActionResult<JwtDto>> RefreshToken([FromBody] Refresh command, CancellationToken token)
        {
            await _refreshHandler.HandleAsync(command, token);
            var jwt = _tokenStorage.Get();
            return jwt;
        }

        [Authorize]
        [Route(Routes.Logout)]
        [HttpDelete]
        public async Task<IActionResult> Logout()
        {
            string rawUserId = HttpContext.User.FindFirstValue("UserId");

            if (!int.TryParse(rawUserId, out int userId))
            {
                return Unauthorized();
            }

            await _refreshTokenRepository.DeleteAllAsync(userId);

            return NoContent();
        }
    }
}
