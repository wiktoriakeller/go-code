using AutoFixture;
using GoCode.Infrastructure.Identity.Entities;

namespace GoCode.UnitTests.Attributes.Customization
{
    public class CustomUserCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<User>(transform => transform
                .Without(u => u.RefreshToken)
                .Without(u => u.UserCourses));
        }
    }
}
