using GoCode.Application.Common.Dtos;

namespace GoCode.UnitTests.Attributes.Customization
{
    [ExcludeFromCodeCoverage]
    public class CorrectCreateCourseDtoCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<CreateCourseDto>(transform => transform
                .With(x => x.XP, 5)
                .With(x => x.PassPercentTreshold, 50)
                .With(x => x.Questions, new List<CreateQuestionDto>
                {
                    new CreateQuestionDto
                    {
                        Content = "Question1",
                        Answers = new List<CreateAnswearDto>
                        {
                            new CreateAnswearDto
                            {
                                Content = "Answear1",
                                IsCorrect = false
                            },
                            new CreateAnswearDto
                            {
                                Content = "Answear2",
                                IsCorrect = true
                            },
                        }
                    },
                    new CreateQuestionDto
                    {
                        Content = "Question2",
                        Answers = new List<CreateAnswearDto>
                        {
                            new CreateAnswearDto
                            {
                                Content = "Answear1",
                                IsCorrect = false
                            },
                            new CreateAnswearDto
                            {
                                Content = "Answear2",
                                IsCorrect = true
                            },
                        }
                    }
                }));
        }
    }
}
