using PBJ.StoreManagementService.DataAccess.Context;
using PBJ.StoreManagementService.DataAccess.Repositories;
using PBJ.StoreManagementService.DataAccess.Repositories.Abstract;
using PBJ.StoreManagementService.DataAccess.UnitOfWork.Abstract;

namespace PBJ.StoreManagementService.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DatabaseContext _databaseContext;
        private IUserRepository _userRepository;
        private IPostRepository _postRepository;
        private ICommentRepository _commentRepository;
        private IFollowingRepository _followingRepository;
        private ISubscriptionRepository _subscriptionRepository;
        private IUserSubscriptionRepository _userSubscriptionRepository;
        private IUserFollowingRepository _userFollowingRepository;

        private bool disposed = false;

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_databaseContext);
                }

                return _userRepository;
            }
        }

        public IPostRepository PostRepository
        {
            get
            {
                if (_postRepository == null)
                {
                    _postRepository = new PostRepository(_databaseContext);
                }

                return _postRepository;
            }
        }

        public ICommentRepository CommentRepository
        {
            get
            {
                if (_commentRepository == null)
                {
                    _commentRepository = new CommentRepository(_databaseContext);
                }

                return _commentRepository;
            }
        }

        public ISubscriptionRepository SubscriptionRepository
        {
            get
            {
                if (_subscriptionRepository == null)
                {
                    _subscriptionRepository = new SubscriptionRepository(_databaseContext);
                }

                return _subscriptionRepository;
            }
        }

        public IFollowingRepository FollowingRepository
        {
            get
            {
                if (_followingRepository == null)
                {
                    _followingRepository = new FollowingRepository(_databaseContext);
                }

                return _followingRepository;
            }
        }

        public IUserSubscriptionRepository UserSubscriptionRepository
        {
            get
            {
                if (_userSubscriptionRepository == null)
                {
                    _userSubscriptionRepository = new UserSubscriptionRepository(_databaseContext);
                }

                return _userSubscriptionRepository;
            }
        }

        public IUserFollowingRepository UserFollowingRepository
        {
            get
            {
                if (_userFollowingRepository == null)
                {
                    _userFollowingRepository = new UserFollowingRepository(_databaseContext);
                }

                return  _userFollowingRepository;
            }
        }

        public UnitOfWork(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _databaseContext.Dispose();
                }
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
