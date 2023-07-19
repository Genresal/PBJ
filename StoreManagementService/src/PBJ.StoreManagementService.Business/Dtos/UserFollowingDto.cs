namespace PBJ.StoreManagementService.Business.Dtos
{
    public class UserFollowingDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public UserDto? User { get; set; }

        public int FollowingId { get; set; }

        public FollowingDto? Following { get; set; }
    }
}
