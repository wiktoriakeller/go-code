using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Common.Validators.Courses;
using GoCode.UnitTests.Attributes;
using GoCode.UnitTests.Attributes.Customization;
using System.Diagnostics.CodeAnalysis;

namespace GoCode.UnitTests.Application.Common.Validators
{
    [ExcludeFromCodeCoverage]
    public class CreateQuestionDtoValidatorTests
    {
        private static IEnumerable<object[]> GetValidQuestions()
        {
            yield return new object[]
            {
                new CreateQuestionDto
                {
                    Content = "Question1",
                    XP = 2,
                    Answers = new List<CreateAnswearDto>
                    {
                        new CreateAnswearDto {IsCorrect = true, Content = "Yes" },
                        new CreateAnswearDto {IsCorrect = false, Content = "No" },
                    }
                }
            };
            yield return new object[]
            {
                new CreateQuestionDto
                {
                    Content = "Question2",
                    XP = 3,
                    Answers = new List<CreateAnswearDto>
                    {
                        new CreateAnswearDto {IsCorrect = true, Content = "No" },
                        new CreateAnswearDto {IsCorrect = false, Content = "Yes" },
                    }
                }
            };
        }

        private readonly IValidator<CreateQuestionDto> _sut;
        private readonly Mock<IValidator<CreateAnswearDto>> _answearValidator;

        public CreateQuestionDtoValidatorTests()
        {
            _answearValidator = new();
            _answearValidator.Setup(x => x.Validate(It.IsAny<CreateAnswearDto>()))
                .Returns(new ValidationResult());
            _sut = new CreateQuestionDtoValidator(_answearValidator.Object);
        }

        [Theory]
        [MemberData(nameof(GetValidQuestions))]
        public void Validate_GivenCorrectQuestion_ShouldNotReturnErrrors(CreateQuestionDto createQuestionDto)
        {
            //Act
            var result = _sut.TestValidate(createQuestionDto);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2001)]
        [InlineData(2500)]
        public void Validate_GivenQuestion_WhenContentIsInvalid_ShouldReturnErrror(int charCount)
        {
            //Arrange
            var fixture = new Fixture();
            fixture.Customize(new CorrectQuestionCustomization());
            var createQuestionDto = fixture.Create<CreateQuestionDto>();
            createQuestionDto.Content = new string('a', charCount);

            //Act
            var result = _sut.TestValidate(createQuestionDto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Content);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        [InlineData(-20)]
        public void Validate_GivenQuestion_WhenXPPointAreLessThanOrEqualToZero_ShouldReturnErrror(int XP)
        {
            //Arrange
            var fixture = new Fixture();
            var customizedFixture = fixture.Customize(new CorrectQuestionCustomization());
            var createQuestionDto = customizedFixture.Create<CreateQuestionDto>();
            createQuestionDto.XP = XP;

            //Act
            var result = _sut.TestValidate(createQuestionDto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.XP);
        }

        [Theory]
        [CorrectQuestion]
        public void Validate_GivenQuestion_WhenAnswersAreEmpty_ShouldReturnError(CreateQuestionDto createQuestionDto)
        {
            //Arrange
            createQuestionDto.Answers = new List<CreateAnswearDto>();

            //Act
            var result = _sut.TestValidate(createQuestionDto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Answers)
                .WithErrorMessage(ErrorMessages.Answear.AnswersMinimumCount);
        }

        [Theory]
        [CorrectQuestion]
        public void Validate_GivenQuestion_WhenAnswersAreDuplicated_ShouldReturnError(CreateQuestionDto createQuestionDto)
        {
            //Arrange
            createQuestionDto.Answers = new List<CreateAnswearDto>
            {
                new CreateAnswearDto
                {
                    IsCorrect = false,
                    Content = "Answear"
                },
                new CreateAnswearDto
                {
                    IsCorrect = true,
                    Content = "Answear"
                }
            };

            //Act
            var result = _sut.TestValidate(createQuestionDto);

            //Assert
            result.ShouldHaveAnyValidationError()
                .WithErrorMessage(ErrorMessages.Answear.AnswersMustBeUnique);
        }

        [Theory]
        [CorrectQuestion]
        public void Validate_GivenQuestion_WhenThereIsNoCorrectAnswer_ShouldReturnError(CreateQuestionDto createQuestionDto)
        {
            //Arrange
            createQuestionDto.Answers = new List<CreateAnswearDto>
            {
                new CreateAnswearDto
                {
                    IsCorrect = false,
                    Content = "Answear1"
                },
                new CreateAnswearDto
                {
                    IsCorrect = false,
                    Content = "Answear2"
                }
            };

            //Act
            var result = _sut.TestValidate(createQuestionDto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Answers)
                .WithErrorMessage(ErrorMessages.Answear.OnlyOneAnswearCanBeCorrect);
        }

        [Theory]
        [CorrectQuestion]
        public void Validate_GivenQuestion_WhenThereIsAreMultipleCorrectAnswers_ShouldReturnError(CreateQuestionDto createQuestionDto)
        {
            //Arrange
            createQuestionDto.Answers = new List<CreateAnswearDto>
            {
                new CreateAnswearDto
                {
                    IsCorrect = true,
                    Content = "Answear1"
                },
                new CreateAnswearDto
                {
                    IsCorrect = true,
                    Content = "Answear2"
                }
            };

            //Act
            var result = _sut.TestValidate(createQuestionDto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Answers)
                .WithErrorMessage(ErrorMessages.Answear.OnlyOneAnswearCanBeCorrect);
        }
    }
}
