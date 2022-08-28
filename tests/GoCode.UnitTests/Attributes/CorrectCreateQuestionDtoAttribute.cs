using GoCode.UnitTests.Attributes.Customization;

namespace GoCode.UnitTests.Attributes
{
    [ExcludeFromCodeCoverage]
    public class CorrectCreateQuestionDtoAttribute : AutoDataAttribute
    {
        public CorrectCreateQuestionDtoAttribute() : base(() =>
            new Fixture().Customize(new CorrectCreateQuestionDtoCustomization()))
        {
        }
    }
}
