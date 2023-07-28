using AutoFixture;
using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.Api.IntegrationTests.FixtureCustomizations
{
    public class UserFollowersCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<UserFollowers>(cfg => 
                cfg.Without(x => x.Id)
                    .Without(x => x.User)
                    .Without(x => x.Follower));
        }
    }
}
