using AutoFixture;
using PBJ.StoreManagementService.DataAccess.Entities;
using PBJ.StoreManagementService.Models.User;
using PBJ.StoreManagementService.Models.UserFollowers;

namespace PBJ.StoreManagementService.Business.UnitTests.AutoFixtureConfigurations
{
    public class UserCustomizations : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<User>(cfg =>
                cfg.Without(x => x.Id)
                    .With(x => x.Followers, new List<UserFollowers>())
                    .With(x => x.Followings, new List<UserFollowers>()));

            fixture.Customize<UserDto>(cfg =>
                cfg.Without(x => x.Id)
                    .With(x => x.Followers, new List<UserFollowersDto>())
                    .With(x => x.Followings, new List<UserFollowersDto>()));
        }
    }
}
