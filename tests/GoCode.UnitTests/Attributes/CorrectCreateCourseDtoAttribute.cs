using GoCode.UnitTests.Attributes.Customization;

namespace GoCode.UnitTests.Attributes
{
    [ExcludeFromCodeCoverage]
    public class CorrectCreateCourseDtoAttribute : AutoDataAttribute
    {
        public CorrectCreateCourseDtoAttribute() : base(() =>
            new Fixture().Customize(new CorrectCreateCourseDtoCustomization()))
        {
        }
    }
}
