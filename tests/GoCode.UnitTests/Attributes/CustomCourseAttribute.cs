using GoCode.UnitTests.Attributes.Customization;

namespace GoCode.UnitTests.Attributes
{
    public class CustomCourseAttribute : AutoDataAttribute
    {
        public CustomCourseAttribute() : base(() => new Fixture().Customize(new CustomCourseCustomization()))
        {
        }
    }
}
