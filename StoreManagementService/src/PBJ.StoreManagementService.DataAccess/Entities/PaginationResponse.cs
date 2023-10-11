using PBJ.StoreManagementService.DataAccess.Entities.Abstract;

namespace PBJ.StoreManagementService.DataAccess.Entities
{
    public class PaginationResponse<TEntity>
        where TEntity : BaseEntity
    {
        public IEnumerable<TEntity> Items { get; set; } = new List<TEntity>();

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 20;

        public int Total { get; set; }
    }
}
