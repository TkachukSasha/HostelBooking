using Hostel.Security.Application.Common.ExceptionCodes;
using Hostel.Security.Application.Common.Repositories;
using Hostel.Security.Application.Common.Security.TokenGenerators;
using Hostel.Security.Application.Common.Security.TokenValidators;
using Hostel.Security.Application.Dto;
using Hostel.Security.Domain.Entities;
using Hostel.Security.Domain.Repositories;
using Hostel.Shared.Types;
using Hostel.Shared.Types.Exceptions;

namespace Hostel.Security.Application.Commands.Refresh
{
    public class Refresh : ICommand
    {
        public string Token { get; set; }
    }

    public class RefreshHandler : ICommandHandler<Refresh>
    {

        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly IPasswordManager _passwordManager;
        private readonly ITokenStorage _tokenStorage;

        public RefreshHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            AccessTokenGenerator accessTokenGenerator,
            RefreshTokenGenerator refreshTokenGenerator,
            IRefreshTokenRepository refreshTokenRepository,
            RefreshTokenValidator refreshTokenValidator,
            IPasswordManager passwordManager,
            ITokenStorage tokenStorage)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
            _refreshTokenValidator = refreshTokenValidator;
            _passwordManager = passwordManager;
            _tokenStorage = tokenStorage;
        }

        public async Task HandleAsync(Refresh command, CancellationToken cancellationToken)
        {
            bool isValidRefreshToken = _refreshTokenValidator.Validate(command.Token);

            if (!isValidRefreshToken)
                throw new HostelException(Codes.TokenIsNotCorrect,
                    $"Token: {command.Token} is not correct!");

            RefreshToken refreshTokenDTO = await _refreshTokenRepository.GetByTokenAsync(command.Token);

            if (refreshTokenDTO is null)
                throw new HostelException(Codes.TokenIsNotCorrect,
                    $"Token: {command.Token} is not correct!");

            await _refreshTokenRepository.DeleteAsync(refreshTokenDTO.RefreshTokenId);
            User user = await _userRepository.GetByIdAsync(refreshTokenDTO.UserId);

            if (user is null)
                throw new HostelException(Codes.UserNotFound,
                   $"User: {user.UserId} is not found!");

            var existingUser = await _userRepository.GetByEmailAsync(user.Email);

            if (existingUser == null)
                throw new HostelException(Codes.EmailIsNotFound,
                   $"Email: {existingUser.Email} is not found!");

            AccessToken accessToken = _accessTokenGenerator.GenerateToken(existingUser);

            string refreshToken = _refreshTokenGenerator.GenerateToken();

            RefreshToken newRefreshTokenDTO = new RefreshToken(refreshToken, existingUser.UserId);

            await _refreshTokenRepository.CreateAsync(newRefreshTokenDTO);

            var result = new JwtDto()
            {
                AccessToken = accessToken.Value,
                RefreshToken = refreshToken
            };

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _tokenStorage.Set(result);
        }
    }
}
