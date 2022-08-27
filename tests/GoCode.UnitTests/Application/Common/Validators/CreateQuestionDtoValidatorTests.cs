using FluentValidation;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using GoCode.Application.Common.Constants;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Common.Validators.Questions;
using GoCode.UnitTests.Attributes.Customization;

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
                    Answers = new List<CreateAnswearDto>
                    {
                        new CreateAnswearDto {IsCorrect = true, Content = "No" },
                        new CreateAnswearDto {IsCorrect = false, Content = "Yes" },
                    }
                }
            };
        }

        private readonly IValidator<CreateQuestionDto> _sut;
        private readonly Mock<IValidator<CreateAnswearDto>> _answearValidatorMock;

        public CreateQuestionDtoValidatorTests()
        {
            _answearValidatorMock = new();
            _answearValidatorMock.Setup(x => x.Validate(It.IsAny<CreateAnswearDto>()))
                .Returns(new ValidationResult());
            _sut = new CreateQuestionDtoValidator(_answearValidatorMock.Object);
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
        public void Validate_GivenCreateQuestionDto_WhenContentIsInvalid_ShouldReturnErrrorForContent(int charCount)
        {
            //Arrange
            var createQuestionDto = GetCreateQuestionDtoFixture();
            createQuestionDto.Content = new string('a', charCount);

            //Act
            var result = _sut.TestValidate(createQuestionDto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Content);
        }

        [Theory]
        [CorrectQuestion]
        public void Validate_GivenCreateQuestionDto_WhenAnswersAreEmpty_ShouldReturnErrorForAnswers(CreateQuestionDto createQuestionDto)
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
        public void Validate_GivenCreateQuestionDto_WhenAnswersContentsAreDuplicated_ShouldReturnError(CreateQuestionDto createQuestionDto)
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
        public void Validate_GivenCreateQuestionDto_WhenThereIsNoCorrectAnswer_ShouldReturnErrorForAnswers(CreateQuestionDto createQuestionDto)
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
        public void Validate_GivenCreateQuestionDto_WhenThereIsAreMultipleCorrectAnswers_ShouldReturnErrorForAnswers(CreateQuestionDto createQuestionDto)
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

        private CreateQuestionDto GetCreateQuestionDtoFixture()
        {
            var fixture = new Fixture().Customize(new CorrectQuestionCustomization());
            return fixture.Create<CreateQuestionDto>();
        }
    }
}
