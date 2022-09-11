using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Contracts.DataAccess;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Common.Validators.Courses;
using GoCode.UnitTests.Attributes.Customization;

namespace GoCode.UnitTests.Application.Common.Validators.Courses
{
    [ExcludeFromCodeCoverage]
    public class CreateCourseDtoValidatorTests
    {
        private static IEnumerable<object[]> GetValidCourseDtos()
        {
            yield return new object[]
            {
                new CreateCourseDto
                {
                    Name = "Course 1",
                    Description = "Description",
                    XP = 5,
                    PassPercentTreshold = 50,
                    Questions = new List<CreateQuestionDto>
                    {
                        new CreateQuestionDto
                        {
                            Content = "Question1",
                        },
                        new CreateQuestionDto
                        {
                            Content = "Question2",
                        }
                    }
                }
            };
        }

        private readonly IValidator<CreateCourseDto> _sut;
        private readonly Mock<IValidator<CreateQuestionDto>> _questionValidatorMock;
        private readonly Mock<ICoursesRepository> _coursesRepositoryMock;

        public CreateCourseDtoValidatorTests()
        {
            _questionValidatorMock = new();
            _coursesRepositoryMock = new();
            _questionValidatorMock.Setup(x => x.Validate(It.IsAny<CreateQuestionDto>()))
                .Returns(new ValidationResult());
            _sut = new CreateCourseDtoValidator(_questionValidatorMock.Object, _coursesRepositoryMock.Object);
        }

        [Theory]
        [MemberData(nameof(GetValidCourseDtos))]
        public void Validate_GivenCreateCourseDto_ShouldNotReturnErrrors(CreateCourseDto createCourseCommand)
        {
            //Act
            var result = _sut.TestValidate(createCourseCommand);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1001)]
        [InlineData(2000)]
        public void Validate_GivenCreateCourseDto_WhenNameHasIncorrectLength_ShouldReturnErrrorForName(int charCount)
        {
            //Arrange
            var dto = GetCreateCourseDtoFixture();
            dto.Name = new string('a', charCount);

            //Act
            var result = _sut.TestValidate(dto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2001)]
        [InlineData(3000)]
        public void Validate_GivenCreateCourseDto_WhenDescriptionHasIncorrectLength_ShouldReturnErrrorForDescription(int charCount)
        {
            //Arrange
            var dto = GetCreateCourseDtoFixture();
            dto.Description = new string('a', charCount);

            //Act
            var result = _sut.TestValidate(dto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Description);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        [InlineData(-200)]
        public void Validate_GivenCreateCourseDto_WhenXPPointAreLessThanOrEqualToZero_ShouldReturnErrrorForXP(int XP)
        {
            //Arrange
            var dto = GetCreateCourseDtoFixture();
            dto.XP = XP;

            //Act
            var result = _sut.TestValidate(dto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.XP);
        }

        [Theory]
        [InlineData(-500)]
        [InlineData(-1)]
        [InlineData(101)]
        [InlineData(500)]
        public void Validate_GivenCreateCourseDto_WhenPassPercentTresholdIsNotBetweenZeroAndOneHundred_ShouldReturnErrrorForXP(int treshold)
        {
            //Arrange
            var dto = GetCreateCourseDtoFixture();
            dto.PassPercentTreshold = treshold;

            //Act
            var result = _sut.TestValidate(dto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.PassPercentTreshold);
        }

        [Theory]
        [CorrectCreateCourseDto]
        public void Validate_GivenCreateCourseCommand_WhenQuestionsAreDuplicated_ShouldReturnErrror(CreateCourseDto dto)
        {
            //Arrange
            dto.Questions = new List<CreateQuestionDto>
            {
                new CreateQuestionDto
                {
                    Content = "Question1",
                },
                new CreateQuestionDto
                {
                    Content = "Question1",
                }
            };

            //Act
            var result = _sut.TestValidate(dto);

            //Assert
            result.ShouldHaveAnyValidationError()
                .WithErrorMessage(ErrorMessages.Question.QuestionsMustBeUnique);
        }

        private CreateCourseDto GetCreateCourseDtoFixture()
        {
            var fixture = new Fixture().Customize(new CorrectCreateCourseDtoCustomization());
            return fixture.Create<CreateCourseDto>();
        }
    }
}
