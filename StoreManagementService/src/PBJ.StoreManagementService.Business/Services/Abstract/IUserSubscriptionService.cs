using PBJ.StoreManagementService.Business.Dtos;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface IUserSubscriptionService
    {
        Task<List<UserSubscriptionDto>> GetAmountAsync(int amount);

        Task<UserSubscriptionDto> GetAsync(int id);

        Task<bool> CreateAsync(UserSubscriptionDto userSubscriptionDto);

        Task<bool> UpdateAsync(int id, UserSubscriptionDto userSubscriptionDto);

        Task<bool> DeleteAsync(int id);
    }
}
