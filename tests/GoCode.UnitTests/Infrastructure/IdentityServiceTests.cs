using AutoMapper;
using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Common.Services;
using GoCode.Application.Identity.Commands;
using GoCode.Infrastructure.Identity;
using GoCode.Infrastructure.Identity.Dto;
using GoCode.Infrastructure.Identity.Entities;
using GoCode.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace GoCode.UnitTests.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class IdentityServiceTests
    {
        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly Mock<IRepository<RefreshToken>> _refreshTokenRepository;
        private readonly Mock<IDateTimeProvider> _dateTimeProvider;

        public IdentityServiceTests()
        {
            _jwtOptions = Options.Create(new JwtOptions());
            _refreshTokenRepository = new Mock<IRepository<RefreshToken>>();
            _dateTimeProvider = new Mock<IDateTimeProvider>();
            _dateTimeProvider.Setup(x => x.UtcNow).Returns(new DateTime(2022, 1, 1, 12, 0, 0, 0, 0));
        }

        [Theory]
        [CustomUser]
        public async Task GetUserByEmail_GivenEmail_ResponseShouldBeNotFound(string email)
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var jwtService = new Mock<IJwtService>();
            var userManager = MockUserManager<User>();

            var sut = new IdentityService(userManager.Object,
                jwtService.Object,
                _refreshTokenRepository.Object,
                mapper.Object,
                _dateTimeProvider.Object,
                _jwtOptions);

            //Act
            var response = await sut.GetUserByEmail(email);

            //Assert
            response.ResponseError.Should().Be(ResponseError.NotFound);
        }

        [Theory]
        [CustomUser]
        public async Task GetUserByEmail_GivenEmail_ResponseShouldBeOk(string email, User user)
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var jwtService = new Mock<IJwtService>();
            var userManager = MockUserManager<User>();

            userManager.Setup(x => x.FindByEmailAsync(email))
                .Returns(Task.FromResult(user));

            var sut = new IdentityService(userManager.Object,
                jwtService.Object,
                _refreshTokenRepository.Object,
                mapper.Object,
                _dateTimeProvider.Object,
                _jwtOptions);

            //Act
            var response = await sut.GetUserByEmail(email);

            //Assert
            response.Succeeded.Should().BeTrue();
        }

        [Theory]
        [CustomUser]
        public async Task CreateUserAsync_GivenCreateUserCommand_UsersCountShouldIncrease(CreateUserCommand command,
            List<User> users)
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var jwtService = new Mock<IJwtService>();
            var userManager = MockUserManager<User>();

            userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), command.Password))
                .ReturnsAsync(IdentityResult.Success).Callback<User, string>((x, y) => users.Add(x));

            var sut = new IdentityService(userManager.Object,
                jwtService.Object,
                _refreshTokenRepository.Object,
                mapper.Object,
                _dateTimeProvider.Object,
                _jwtOptions);

            //Act
            await sut.CreateUserAsync(command);

            //Assert
            users.Should().HaveCount(4);
        }

        [Theory]
        [AutoData]
        public async Task CreateUserAsync_GivenCreateUserCommand_WhenUserManagerFails_ResponseErrorsShouldNotBeEmpty(CreateUserCommand command)
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var jwtService = new Mock<IJwtService>();
            var userManager = MockUserManager<User>();

            userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), command.Password))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Code = "Identity", Description = "Invalid user data" }));

            var sut = new IdentityService(userManager.Object,
                jwtService.Object,
                _refreshTokenRepository.Object,
                mapper.Object,
                _dateTimeProvider.Object,
                _jwtOptions);

            //Act
            var result = await sut.CreateUserAsync(command);

            //Assert
            result.Errors.Should().NotBeEmpty();
        }

        [Theory]
        [AutoData]
        public async Task AuthenticateUserAync_GivenAuthenticateUserCommand_WhenUserIsNull_ResponseErrorsShouldNotBeEmpty(AuthenticateUserCommand command)
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var jwtService = new Mock<IJwtService>();
            var jwtOptions = Options.Create(new JwtOptions());
            var refreshTokenRepository = new Mock<IRepository<RefreshToken>>();
            var userManager = MockUserManager<User>();

            userManager.Setup(x => x.FindByEmailAsync(command.Email))
                .Returns(Task.FromResult<User?>(null));

            var sut = new IdentityService(userManager.Object,
                jwtService.Object,
                refreshTokenRepository.Object,
                mapper.Object,
                _dateTimeProvider.Object,
                jwtOptions);

            //Act
            var result = await sut.AuthenticateUserAync(command);

            //Assert
            result.Errors.Should().Contain(ErrorMessages.Identity.IncorrectCredentials);
        }

        [Theory]
        [CustomUser]
        public async Task AuthenticateUserAync_GivenAuthenticateUserCommand_WhenPasswordIsInvalid_ResponseErrorsShouldNotBeEmpty(AuthenticateUserCommand command,
            User user)
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var jwtService = new Mock<IJwtService>();
            var userManager = MockUserManager<User>();

            userManager.Setup(x => x.FindByEmailAsync(command.Email))
                .Returns(Task.FromResult(user));

            userManager.Setup(x => x.CheckPasswordAsync(user, command.Password))
                .ReturnsAsync(false);

            var sut = new IdentityService(userManager.Object,
                jwtService.Object,
                _refreshTokenRepository.Object,
                mapper.Object,
                _dateTimeProvider.Object,
                _jwtOptions);

            //Act
            var result = await sut.AuthenticateUserAync(command);

            //Assert
            result.Errors.Should().Contain(ErrorMessages.Identity.IncorrectCredentials);
        }

        [Theory]
        [CustomUser]
        public async Task AuthenticateUserAync_GivenAuthenticateUserCommand_WhenPasswordIsInvalid_ResponseResultShouldIndicateSuccess(
            AuthenticateUserCommand command,
            User user,
            string token,
            string jti)
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var jwtService = new Mock<IJwtService>();
            var userManager = MockUserManager<User>();

            userManager.Setup(x => x.FindByEmailAsync(command.Email))
                .Returns(Task.FromResult(user));

            userManager.Setup(x => x.CheckPasswordAsync(user, command.Password))
                .ReturnsAsync(true);

            userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<User?>(null));

            jwtService.Setup(x => x.CreateJwtToken(user))
                .ReturnsAsync(new JwtTokenInfoDto
                {
                    Jti = jti,
                    Token = token
                });

            var sut = new IdentityService(userManager.Object,
                jwtService.Object,
                _refreshTokenRepository.Object,
                mapper.Object,
                _dateTimeProvider.Object,
                _jwtOptions);

            //Act
            var result = await sut.AuthenticateUserAync(command);

            //Assert
            result.Succeeded.Should().BeTrue();
        }

        private static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var userManagerMock = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            userManagerMock.Object.UserValidators.Add(new UserValidator<TUser>());
            userManagerMock.Object.PasswordValidators.Add(new PasswordValidator<TUser>());
            return userManagerMock;
        }
    }
}
