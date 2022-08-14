using AutoFixture;
using GoCode.Infrastructure.Identity.Entities;

namespace GoCode.UnitTests.Attributes.Customization
{
    public class ApplicationUserWithoutTokenCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<ApplicationUser>(transform => transform
                .Without(u => u.RefreshToken));
        }
    }
}
