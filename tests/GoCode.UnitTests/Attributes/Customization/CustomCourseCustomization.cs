using GoCode.Domain.Entities;

namespace GoCode.UnitTests.Attributes.Customization
{
    [ExcludeFromCodeCoverage]
    public class CustomCourseCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Course>(transform => transform
                .Without(x => x.Questions)
                .Without(x => x.UserCourses));
        }
    }
}
