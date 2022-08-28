using AutoMapper;
using GoCode.Application.Common.Contracts.DataAccess;
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
        public async Task Handle_GivenGetAllCoursesQuery_MustReturnAllCourses(GetAllCoursesQuery query,
            IEnumerable<CourseDto> expected)
        {
            //Arrange
            var repositoryMock = new Mock<ICoursesRepository>();
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<CourseDto>>(It.IsAny<IEnumerable<Course>>()))
                .Returns(expected);

            var sut = new GetAllCoursesQueryHandler(repositoryMock.Object, mapperMock.Object);

            //Act
            var response = await sut.Handle(query, CancellationToken.None);

            //Assert
            response.Data.Courses.Should().BeEquivalentTo(expected);
        }
    }
}
