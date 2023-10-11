using AutoFixture.Kernel;
using Bogus;

namespace PBJ.StoreManagementService.Api.IntegrationTests.FixtureCustomizations
{
    public class RandonEmailGeneratorCustomization : ISpecimenBuilder
    {
        public object Create(object request, ISpecimenContext context)
        {
            var type = request as Type;
            if (type != null && type == typeof(string)) return new Faker().Internet.Email();

            return new NoSpecimen();
        }
    }
}
