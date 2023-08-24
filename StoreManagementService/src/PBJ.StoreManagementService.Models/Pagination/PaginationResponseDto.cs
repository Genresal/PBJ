namespace PBJ.StoreManagementService.Models.Pagination
{
    public class PaginationResponseDto<TDto>
    {
        public IEnumerable<TDto> Items { get; set; } = new List<TDto>();

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 20;

        public int Total { get; set; }
    }
}
