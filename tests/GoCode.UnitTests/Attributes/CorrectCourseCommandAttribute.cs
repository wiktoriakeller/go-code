using GoCode.UnitTests.Attributes.Customization;

namespace GoCode.UnitTests.Attributes
{
    [ExcludeFromCodeCoverage]
    public class CorrectCourseCommandAttribute : AutoDataAttribute
    {
        public CorrectCourseCommandAttribute() : base(() =>
            new Fixture().Customize(new CorrectCourseCommandCustomization()))
        {
        }
    }
}
