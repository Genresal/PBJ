using AutoFixture;
using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.Api.IntegrationTests.FixtureCustomizations
{
    public class UserCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<User>(cfg =>
                cfg.Without(x => x.Id)
                    .Without(x => x.Posts)
                    .Without(x => x.Comments)
                    .Without(x => x.Followers)
                    .Without(x => x.Followings));
        }
    }
}