using PBJ.StoreManagementService.DataAccess.Entities;

namespace PBJ.StoreManagementService.DataAccess.Repositories.Abstract
{
    public interface ICommentRepository : IRepository<Comment>
    { }
    //remove referencing data deletion from service, add cascade 
}
