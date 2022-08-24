using GoCode.Application.Common.BaseResponse;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Courses.Commands;
using GoCode.Application.Courses.Handlers;
using GoCode.Domain.Entities;

namespace GoCode.UnitTests.Application.Courses.Handlers
{
    [ExcludeFromCodeCoverage]
    public class DeleteCourseCommandHandlerTests
    {
        [Theory]
        [CustomCourse]
        public async Task Handle_GivenDeleteCourseCommand_ShouldReturnSuccess(DeleteCourseCommand command, Course course)
        {
            //Arrange
            var coursesRepositoryMock = new Mock<ICoursesRepository>();
            coursesRepositoryMock.Setup(x => x.GetByIdAsync(command.Id))
                .ReturnsAsync(course);

            var sut = new DeleteCourseCommandHandler(coursesRepositoryMock.Object);

            //Act
            var response = await sut.Handle(command, CancellationToken.None);

            //Assert
            response.Succeeded.Should().BeTrue();
        }
    }
}
