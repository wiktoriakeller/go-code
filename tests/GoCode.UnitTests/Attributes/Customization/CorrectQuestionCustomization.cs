using GoCode.Application.Common.Dtos;

namespace GoCode.UnitTests.Attributes.Customization
{
    [ExcludeFromCodeCoverage]
    public class CorrectQuestionCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<CreateQuestionDto>(transform => transform
                .With(x => x.XP, 5)
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
