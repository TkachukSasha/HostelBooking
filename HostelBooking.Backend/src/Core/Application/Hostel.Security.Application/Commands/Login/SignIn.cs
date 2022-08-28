using Hostel.Security.Application.Common.ExceptionCodes;
using Hostel.Security.Application.Common.Repositories;
using Hostel.Security.Application.Common.Security.TokenGenerators;
using Hostel.Security.Application.Dto;
using Hostel.Security.Domain.Entities;
using Hostel.Security.Domain.Repositories;
using Hostel.Shared.Types;
using Hostel.Shared.Types.Exceptions;

namespace Hostel.Security.Application.Commands.Login
{
    public class SignIn : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class SignInHandler : ICommandHandler<SignIn>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IPasswordManager _passwordManager;
        private readonly ITokenStorage _tokenStorage;

        public SignInHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            AccessTokenGenerator accessTokenGenerator,
            RefreshTokenGenerator refreshTokenGenerator,
            IRefreshTokenRepository refreshTokenRepository,
            IPasswordManager passwordManager,
            ITokenStorage tokenStorage)
        {
            _userRepository = userRepository;
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
            _passwordManager = passwordManager;
            _tokenStorage = tokenStorage;
        }

        public async Task HandleAsync(SignIn command, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(command.Email);

            if (existingUser is null)
                throw new HostelException(Codes.EmailIsNotFound,
                    $"Email: {command.Email} is not found!");

            bool isCorrectPassword = _passwordManager.Validate(command.Password, existingUser.Password);

            if (!isCorrectPassword)
                throw new HostelException(Codes.PasswordIsNotCorrect,
                    $"Password: {command.Password} is not correct!");

            AccessToken accessToken = _accessTokenGenerator.GenerateToken(existingUser);

            string refreshToken = _refreshTokenGenerator.GenerateToken();

            RefreshToken refreshTokenDTO = new RefreshToken(refreshToken, existingUser.UserId);

            await _refreshTokenRepository.CreateAsync(refreshTokenDTO);

            var result = new JwtDto()
            {
                AccessToken = accessToken.Value,
                RefreshToken = refreshToken
            };

            _tokenStorage.Set(result);
        }
    }
}
