using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using GoCode.Application.Contracts.DataAccess;
using GoCode.Application.Dto.BaseResponse;
using GoCode.Application.Dto.Identity;
using GoCode.Application.Identity.Commands;
using GoCode.Infrastructure.Constants;
using GoCode.Infrastructure.Identity;
using GoCode.Infrastructure.Identity.Entities;
using GoCode.Infrastructure.Interfaces;
using GoCode.UnitTests.Attributes;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace GoCode.UnitTests.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class IdentityServiceTests
    {
        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly Mock<IRepository<RefreshToken>> _refreshTokenRepository;

        public IdentityServiceTests()
        {
            _jwtOptions = Options.Create(new JwtOptions());
            _refreshTokenRepository = new Mock<IRepository<RefreshToken>>();
        }

        [Theory]
        [ApplicationUserWithoutTokenData]
        public async Task GetUserById_GivenUserId_ResultShouldBeOk(string userId, ApplicationUser user, UserDto userDto)
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var jwtService = new Mock<IJwtService>();
            var userManager = MockUserManager<ApplicationUser>();

            userManager.Setup(x => x.FindByIdAsync(userId))
                .Returns(Task.FromResult(user));

            mapper.Setup(x => x.Map<UserDto>(user))
                .Returns(userDto);

            var sut = new IdentityService(userManager.Object,
                jwtService.Object,
                _refreshTokenRepository.Object,
                mapper.Object,
                _jwtOptions);

            //Act
            var result = await sut.GetUserById(userId);

            //Assert
            result.Data.Should().BeEquivalentTo(userDto);
        }

        [Theory]
        [AutoData]
        public async Task GetUserById_GivenUserId_ResultShouldBeNotFound(string userId)
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var jwtService = new Mock<IJwtService>();
            var userManager = MockUserManager<ApplicationUser>();

            userManager.Setup(x => x.FindByIdAsync(userId))
                .Returns(Task.FromResult<ApplicationUser?>(null));

            var sut = new IdentityService(userManager.Object,
                jwtService.Object,
                _refreshTokenRepository.Object,
                mapper.Object,
                _jwtOptions);

            //Act
            var result = await sut.GetUserById(userId);

            //Assert
            result.ResponseError.Should().Be(ResponseError.NotFound);
        }

        [Theory]
        [ApplicationUserWithoutTokenData]
        public async Task CreateUserAsync_GivenCreateUserCommand_UsersCountShouldIncrease(CreateUserCommand command,
            List<ApplicationUser> users)
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var jwtService = new Mock<IJwtService>();
            var userManager = MockUserManager<ApplicationUser>();

            userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), command.Password))
                .ReturnsAsync(IdentityResult.Success).Callback<ApplicationUser, string>((x, y) => users.Add(x));

            var sut = new IdentityService(userManager.Object,
                jwtService.Object,
                _refreshTokenRepository.Object,
                mapper.Object,
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
            var userManager = MockUserManager<ApplicationUser>();

            userManager.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), command.Password))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Code = "Identity", Description = "Invalid user data" }));

            var sut = new IdentityService(userManager.Object,
                jwtService.Object,
                _refreshTokenRepository.Object,
                mapper.Object,
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
            var userManager = MockUserManager<ApplicationUser>();

            userManager.Setup(x => x.FindByEmailAsync(command.Email))
                .Returns(Task.FromResult<ApplicationUser?>(null));

            var sut = new IdentityService(userManager.Object,
                jwtService.Object,
                refreshTokenRepository.Object,
                mapper.Object,
                jwtOptions);

            //Act
            var result = await sut.AuthenticateUserAync(command);

            //Assert
            result.Errors.Should().Contain(ErrorMessages.Identity.IncorrectCredentials);
        }

        [Theory]
        [ApplicationUserWithoutTokenData]
        public async Task AuthenticateUserAync_GivenAuthenticateUserCommand_WhenPasswordIsInvalid_ResponseErrorsShouldNotBeEmpty(AuthenticateUserCommand command,
            ApplicationUser user)
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var jwtService = new Mock<IJwtService>();
            var userManager = MockUserManager<ApplicationUser>();

            userManager.Setup(x => x.FindByEmailAsync(command.Email))
                .Returns(Task.FromResult(user));

            userManager.Setup(x => x.CheckPasswordAsync(user, command.Password))
                .ReturnsAsync(false);

            var sut = new IdentityService(userManager.Object,
                jwtService.Object,
                _refreshTokenRepository.Object,
                mapper.Object,
                _jwtOptions);

            //Act
            var result = await sut.AuthenticateUserAync(command);

            //Assert
            result.Errors.Should().Contain(ErrorMessages.Identity.IncorrectCredentials);
        }

        [Theory]
        [ApplicationUserWithoutTokenData]
        public async Task AuthenticateUserAync_GivenAuthenticateUserCommand_WhenPasswordIsInvalid_ResponseResultShouldIndicateSuccess(
            AuthenticateUserCommand command,
            ApplicationUser user,
            string token,
            string jti)
        {
            //Arrange
            var mapper = new Mock<IMapper>();
            var jwtService = new Mock<IJwtService>();
            var userManager = MockUserManager<ApplicationUser>();

            userManager.Setup(x => x.FindByEmailAsync(command.Email))
                .Returns(Task.FromResult(user));

            userManager.Setup(x => x.CheckPasswordAsync(user, command.Password))
                .ReturnsAsync(true);

            userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>()))
                .Returns(Task.FromResult<ApplicationUser?>(null));

            jwtService.Setup(x => x.CreateJwtToken(user))
                .Returns((token, jti));

            var sut = new IdentityService(userManager.Object,
                jwtService.Object,
                _refreshTokenRepository.Object,
                mapper.Object,
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
