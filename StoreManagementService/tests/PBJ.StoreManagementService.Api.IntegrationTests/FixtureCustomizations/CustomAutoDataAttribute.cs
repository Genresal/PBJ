using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using PBJ.StoreManagementService.Models.Pagination;

namespace PBJ.StoreManagementService.Api.IntegrationTests.FixtureCustomizations
{
    public class CustomAutoDataAttribute : AutoDataAttribute
    {
        public CustomAutoDataAttribute() : base(CreateFixture)
        { }

        private static IFixture CreateFixture()
        {
            var fixture = new Fixture();

            fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });

            fixture.Customize<PaginationRequestModel>(cfg =>
                cfg.With(x => x.Page, 1));

            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            return fixture;
        }
    }
}
