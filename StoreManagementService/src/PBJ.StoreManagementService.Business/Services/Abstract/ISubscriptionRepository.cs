using PBJ.StoreManagementService.Business.Dtos;

namespace PBJ.StoreManagementService.Business.Services.Abstract
{
    public interface ISubscriptionRepository
    {
        Task<List<SubscriptionDto>> GetAmountAsync(int amount);

        Task<List<SubscriptionDto>> GetUserSubscriptionsAsync(int userId, int amount);

        Task<SubscriptionDto> GetAsync(int id);

        Task<bool> CreateAsync(SubscriptionDto subscriptionDto);

        Task<bool> UpdateAsync(int id, SubscriptionDto subscriptionDto);

        Task<bool> DeleteAsync(int id);
    }
}
