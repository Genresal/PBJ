using AutoFixture;
using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.Api.IntegrationTests.FixtureCustomizations
{
    public class PostCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Post>(cfg =>
                cfg.Without(x => x.Id)
                    .Without(x => x.Comments)
                    .Without(x => x.User));
        }
    }
}
