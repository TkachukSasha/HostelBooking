using Hostel.Security.Application.Common.ExceptionCodes;
using Hostel.Security.Application.Common.Repositories;
using Hostel.Security.Application.Common.Security.TokenGenerators;
using Hostel.Security.Application.Dto;
using Hostel.Security.Domain.Entities;
using Hostel.Security.Domain.Repositories;
using Hostel.Security.Domain.ValueObjects;
using Hostel.Shared.Types;
using Hostel.Shared.Types.Exceptions;

namespace Hostel.Security.Application.Commands.Register
{
    public class SignUp : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }

    public class SignUpHandler : ICommandHandler<SignUp>
    {

        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordManager _passwordManager;
        private readonly IClock _clock;
        private readonly AccessTokenGenerator _accessTokenGenerator;
        private readonly RefreshTokenGenerator _refreshTokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenStorage _tokenStorage;

        public SignUpHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IPasswordManager passwordManager,
            IClock clock,
            AccessTokenGenerator accessTokenGenerator,
            RefreshTokenGenerator refreshTokenGenerator,
            IRefreshTokenRepository refreshTokenRepository,
            ITokenStorage tokenStorage)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordManager = passwordManager;
            _clock = clock;
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenStorage = tokenStorage;
        }

        public async Task HandleAsync(SignUp command, CancellationToken cancellationToken)
        {
            var email = new Email(command.Email);
            var password = new Password(command.Password);
            var role = string.IsNullOrWhiteSpace(command.Role) ? Role.User() : new Role(command.Role);

            var existingEmail = await _userRepository.GetByEmailAsync(command.Email);

            if (existingEmail is null)
                throw new HostelException(Codes.EmailIsNotFound,
                    $"Email: {command.Email} is not found!");

            var securePassword = _passwordManager.Secure(password);

            var user = new User(email, securePassword, role, _clock.Current(), false);

            await _userRepository.AddAsync(user);

            AccessToken accessToken = _accessTokenGenerator.GenerateToken(user);

            string refreshToken = _refreshTokenGenerator.GenerateToken();

            RefreshToken refreshTokenDTO = new RefreshToken(refreshToken, user.UserId);

            await _refreshTokenRepository.CreateAsync(refreshTokenDTO);

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
