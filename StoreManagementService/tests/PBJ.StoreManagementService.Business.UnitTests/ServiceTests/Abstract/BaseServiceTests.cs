using AutoMapper;
using Moq;

namespace PBJ.StoreManagementService.Business.UnitTests.ServiceTests.Abstract
{
    public abstract class BaseServiceTests
    {
        protected readonly Mock<IMapper> _mockMapper;

        protected BaseServiceTests()
        {
            _mockMapper = new Mock<IMapper>();
        }
    }
}
