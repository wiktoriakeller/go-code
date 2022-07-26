﻿using AutoFixture;
using GoCode.UnitTests.Attributes.Customization;

namespace GoCode.UnitTests.Attributes
{
    [ExcludeFromCodeCoverage]
    public class CustomUserAttribute : AutoDataAttribute
    {
        public CustomUserAttribute() : base(() =>
            new Fixture().Customize(new CustomUserCustomization()))
        { }
    }
}
