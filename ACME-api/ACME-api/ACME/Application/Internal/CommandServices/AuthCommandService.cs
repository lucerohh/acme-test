using ACME_api.ACME.Domain.Model.Aggregates;
using ACME_api.ACME.Domain.Model.Commands.User;
using ACME_api.ACME.Domain.Repositories;
using ACME_api.ACME.Domain.Services.Auth;
using ACME_api.ACME.Domain.Services.JWT;
using ACME_api.ACME.Interfaces.Rest.Resources.Auth;
using ACME_api.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;


namespace ACME_api.ACME.Application.Internal.CommandServices
{
    public class AuthCommandService : IAuthCommandService
    {
        private readonly IJwtService _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthCommandService(IJwtService jwtService, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SignInResponseResource> SignIn(SignInCommand command) {
            var user = await _userRepository.FindByEmailAsync(command.Email);

            if(user == null)
            {
                throw new ApplicationException("Invalid credentials");
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(command.Password, user.Password);

            if (!isValidPassword)
                throw new ApplicationException("Invalid credentials");

            string accessToken = _jwtService.GenerateToken(user);

            return new SignInResponseResource(accessToken);
        }
        public async Task<SignUpResponseResource> SignUp(SignUpCommand command) {
            var existingUser = await _userRepository.FindByEmailAsync(command.Email);

            if (existingUser != null)
                throw new ApplicationException("User already exists");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(command.Password);

            var user = new User(
                command.FirstName,
                command.LastName,
                command.Email,
                hashedPassword
             );

            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            var accessToken = _jwtService.GenerateToken(user);

            return new SignUpResponseResource(accessToken, user.FirstName, user.LastName);
        }
    }
}
