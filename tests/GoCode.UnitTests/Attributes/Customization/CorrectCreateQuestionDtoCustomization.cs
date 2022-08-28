using GoCode.Application.Common.Dtos;

namespace GoCode.UnitTests.Attributes.Customization
{
    [ExcludeFromCodeCoverage]
    public class CorrectCreateQuestionDtoCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<CreateQuestionDto>(transform => transform
                .With(x => x.Answers, new List<CreateAnswearDto>
                {
                    new CreateAnswearDto
                    {
                        Content = "Answear1",
                        IsCorrect = true
                    },
                    new CreateAnswearDto
                    {
                        Content = "Answear2",
                        IsCorrect = false
                    },
                    new CreateAnswearDto
                    {
                        Content = "Answear3",
                        IsCorrect = false
                    },
                }));
        }
    }
}
