using AutoFixture;
using PBJ.StoreManagementService.Api.IntegrationTests.FixtureCustomizations;
using PBJ.StoreManagementService.DataAccess.Context;
using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.Api.IntegrationTests.Managers
{
    public class TestDataManager
    {
        private readonly DatabaseContext _databaseContext;
        private readonly Fixture _fixture;

        public TestDataManager(DatabaseContext databaseContext, Fixture fixture)
        {
            _databaseContext = databaseContext;
            _fixture = fixture;

            _fixture.Customizations.Add(new MaxLengthStringCustomization(30));
            _fixture.Customize(new UserCustomization());
            _fixture.Customize(new PostCustomization());
            _fixture.Customize(new CommentCustomization());
            _fixture.Customize(new UserFollowersCustomization());

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        public async Task<Comment> CreateCommentAsync()
        {
            var post = await CreatePostAsync();

            var comment = _fixture.Create<Comment>();

            comment.UserId = post.UserId;
            comment.PostId = post.Id;

            _databaseContext.Comments.Add(comment);
            await _databaseContext.SaveChangesAsync();

            return comment;
        }

        public async Task<Post> CreatePostAsync()
        {
            var user = await CreateUserAsync();

            var post = _fixture.Create<Post>();
            post.UserId = user.Id;

            _databaseContext.Posts.Add(post);
            await _databaseContext.SaveChangesAsync();

            return post;
        }

        public async Task<User> CreateUserAsync()
        {
            var user = _fixture.Create<User>();

            _databaseContext.Users.Add(user);
            await _databaseContext.SaveChangesAsync();

            return user;
        }

        public async Task<UserFollowers> CreateUserFollowerAsync()
        {
            var user1 = await CreateUserAsync();
            var user2 = await CreateUserAsync();

            var userFollower = _fixture.Create<UserFollowers>();

            userFollower.UserId = user1.Id;
            userFollower.FollowerId = user2.Id;

            _databaseContext.UserFollowers.Add(userFollower);
            await _databaseContext.SaveChangesAsync();

            return userFollower;
        }
    }
}
