using FluentValidation;
using FluentValidation.TestHelper;
using GoCode.Application.Common.Dtos;
using GoCode.Application.Common.Validators.Answears;

namespace GoCode.UnitTests.Application.Common.Validators
{
    [ExcludeFromCodeCoverage]
    public class CreateAnswearDtoValidatorTests
    {
        private static IEnumerable<object[]> GetValidAnswers()
        {
            yield return new object[]
            {
                new CreateAnswearDto
                {
                    IsCorrect = true,
                    Content = "Yes"
                }
            };
            yield return new object[]
            {
                new CreateAnswearDto
                {
                    IsCorrect = false,
                    Content = "No"
                }
            };
        }

        private static IEnumerable<object[]> GetInvalidAnswers()
        {
            yield return new object[]
            {
                new CreateAnswearDto
                {
                    IsCorrect = true,
                    Content = new string('a', 2001)
                }
            };
            yield return new object[]
            {
                new CreateAnswearDto
                {
                    IsCorrect = true,
                    Content = ""
                }
            };
        }

        private readonly IValidator<CreateAnswearDto> _sut;

        public CreateAnswearDtoValidatorTests()
        {
            _sut = new CreateAnswearDtoValidator();
        }

        [Theory]
        [MemberData(nameof(GetValidAnswers))]
        public void Validate_GivenCorrectAnswear_ShouldNotReturnErrrors(CreateAnswearDto createAnswearDto)
        {
            //Act
            var result = _sut.TestValidate(createAnswearDto);

            //Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Theory]
        [MemberData(nameof(GetInvalidAnswers))]
        public void Validate_GivenInCorrectAnswear_ShouldReturnErrrors(CreateAnswearDto createAnswearDto)
        {
            //Act
            var result = _sut.TestValidate(createAnswearDto);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Content);
        }
    }
}
