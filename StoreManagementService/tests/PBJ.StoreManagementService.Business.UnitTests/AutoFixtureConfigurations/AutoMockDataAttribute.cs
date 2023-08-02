using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

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
