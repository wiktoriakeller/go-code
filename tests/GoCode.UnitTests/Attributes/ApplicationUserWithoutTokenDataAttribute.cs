using AutoFixture;
using AutoFixture.Xunit2;
using GoCode.UnitTests.Attributes.Customization;

namespace GoCode.UnitTests.Attributes
{
    public class ApplicationUserWithoutTokenDataAttribute : AutoDataAttribute
    {
        public ApplicationUserWithoutTokenDataAttribute() : base(() =>
            new Fixture().Customize(new ApplicationUserWithoutTokenCustomization()))
        { }
    }
}
