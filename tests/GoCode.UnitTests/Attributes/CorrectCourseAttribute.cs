using GoCode.UnitTests.Attributes.Customization;

namespace GoCode.UnitTests.Attributes
{
    [ExcludeFromCodeCoverage]
    public class CorrectCourseAttribute : AutoDataAttribute
    {
        public CorrectCourseAttribute() : base(() =>
            new Fixture().Customize(new CorrectCourseCustomization()))
        {
        }
    }
}
