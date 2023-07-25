using AutoFixture;
using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.Api.IntegrationTests.FixtureCustomizations
{
    public class CommentCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<Comment>(cfg =>
                cfg.Without(x => x.Id)
                    .Without(x => x.Post)
                    .Without(x => x.User));
        }
    }
}
