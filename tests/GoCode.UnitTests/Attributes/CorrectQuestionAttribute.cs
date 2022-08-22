using GoCode.UnitTests.Attributes.Customization;

namespace GoCode.UnitTests.Attributes
{
    [ExcludeFromCodeCoverage]
    public class CorrectQuestionAttribute : AutoDataAttribute
    {
        public CorrectQuestionAttribute() : base(() =>
            new Fixture().Customize(new CorrectQuestionCustomization()))
        {
        }
    }
}
