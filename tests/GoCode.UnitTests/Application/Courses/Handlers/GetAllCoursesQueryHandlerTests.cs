using AutoMapper;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Common.Contracts.Identity;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Courses.Handlers;
using GoCode.Application.Courses.Queries;
using GoCode.Domain.Entities;

namespace GoCode.UnitTests.Application.Courses.Handlers
{
    [ExcludeFromCodeCoverage]
    public class GetAllCoursesQueryHandlerTests
    {
        [Theory]
        [AutoData]
        public async Task Handle_GivenGetAllCoursesQuery_MustReturnAllCourses(GetAllCoursesInfoQuery query,
            IEnumerable<CourseInfoDto> expected)
        {
            //Arrange
            var repositoryMock = new Mock<ICoursesRepository>();
            var mapperMock = new Mock<IMapper>();
            var currentUserServiceMock = new Mock<ICurrentUserService>();
            mapperMock.Setup(x => x.Map<IEnumerable<CourseInfoDto>>(It.IsAny<IEnumerable<Course>>()))
                .Returns(expected);

            var sut = new GetAllCoursesInfoQueryHandler(repositoryMock.Object, currentUserServiceMock.Object, mapperMock.Object);

            //Act
            var response = await sut.Handle(query, CancellationToken.None);

            //Assert
            response.Data.Courses.Should().BeEquivalentTo(expected);
        }
    }
}
