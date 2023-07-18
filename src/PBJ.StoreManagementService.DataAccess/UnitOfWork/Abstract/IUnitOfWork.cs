using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;

namespace PBJ.StoreManagementService.DataAccess.UnitOfWork.Abstract
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }

        IPostRepository PostRepository { get; }

        ICommentRepository CommentRepository { get; }

        ISubscriptionRepository SubscriptionRepository { get; }

        IFollowingRepository FollowingRepository { get; }

        IUserSubscriptionRepository UserSubscriptionRepository { get; }

        IUserFollowingRepository UserFollowingRepository { get; }

        Task SaveChangesAsync();
    }
}
