using Moq;
using PBJ.StoreManagementService.Business.Producers.Abstract;

namespace PBJ.StoreManagementService.Api.IntegrationTests.MockDependencies
{
    public class MockMessageProducer : IMessageProducer
    {
        private readonly Mock<IMessageProducer> _mockProducer;

        public MockMessageProducer()
        {
            _mockProducer = new Mock<IMessageProducer>();
        }

        public async Task PublicCommentMessageAsync(string email, string message)
        {
            await _mockProducer.Object.PublicCommentMessageAsync(email, message);
        }
    }
}
