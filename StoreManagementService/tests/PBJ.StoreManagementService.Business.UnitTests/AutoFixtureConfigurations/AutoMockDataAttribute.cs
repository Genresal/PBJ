﻿using AutoFixture.AutoMoq;
using AutoFixture;
using AutoFixture.Xunit2;
using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.Business.UnitTests.AutoFixtureConfigurations
{
    public class AutoMockDataAttribute : AutoDataAttribute
    {
        public AutoMockDataAttribute() : base(CreateFixture)
        { }

        private static IFixture CreateFixture()
        {
            var fixture = new Fixture();

            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });
            fixture.Customizations.Add(new IReadOnlyCollectionsCustomization());

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }
    }
}
