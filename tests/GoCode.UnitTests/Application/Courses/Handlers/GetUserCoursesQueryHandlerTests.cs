using AutoMapper;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Common.Contracts.Identity;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Courses.Handlers;
using GoCode.Application.Courses.Queries;
using GoCode.Domain.Entities;
using GoCode.Domain.Interfaces;
using GoCode.Infrastructure.Identity.Entities;

namespace GoCode.UnitTests.Application.Courses.Handlers
{
    [ExcludeFromCodeCoverage]
    public class GetUserCoursesQueryHandlerTests
    {
        [Theory]
        [CustomUser]
        public async Task Handle_GivenGetUserCoursesQuery_WhenUserIsNotNull_ReturnsUserCourses(User user,
            IEnumerable<UserCourseDto> expected, GetUserCoursesQuery query)
        {
            //Arrange
            var currentUserMock = new Mock<ICurrentUserService>();
            currentUserMock.Setup(x => x.User)
                .Returns(user);

            var coursesRepositoryMock = new Mock<ICoursesRepository>();

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<UserCourseDto>>(It.IsAny<IEnumerable<Course>>()))
                .Returns(expected);

            var sut = new GetUserCoursesQueryHandler(currentUserMock.Object, coursesRepositoryMock.Object,
                mapperMock.Object);

            //Act
            var response = await sut.Handle(query, CancellationToken.None);

            //Assert
            response.Data.Courses.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [AutoData]
        public async Task Handle_GivenGetUserCoursesQuery_WhenUserIsNull_ReturnsAuthorizationFail(GetUserCoursesQuery query)
        {
            //Arrange
            var currentUserMock = new Mock<ICurrentUserService>();
            currentUserMock.Setup(x => x.User)
                .Returns((IUser?)null);

            var coursesRepositoryMock = new Mock<ICoursesRepository>();
            var mapperMock = new Mock<IMapper>();

            var sut = new GetUserCoursesQueryHandler(currentUserMock.Object, coursesRepositoryMock.Object,
                mapperMock.Object);

            //Act
            var response = await sut.Handle(query, CancellationToken.None);

            //Assert
            response.Succeeded.Should().BeFalse();
        }
    }
}
