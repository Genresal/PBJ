using AutoFixture;
using Bogus;
using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.Api.IntegrationTests.FixtureCustomizations
{
    public class UserCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<User>(cfg =>
                cfg.With(x => x.Email, new Faker().Internet.Email())
                    .Without(x => x.Id)
                    .Without(x => x.Posts)
                    .Without(x => x.Comments)
                    .Without(x => x.Followers)
                    .Without(x => x.Followings));
        }
    }
}
