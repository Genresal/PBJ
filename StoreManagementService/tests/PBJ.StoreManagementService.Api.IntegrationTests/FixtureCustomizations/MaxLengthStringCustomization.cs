using System.Reflection;
using AutoFixture.Kernel;

namespace PBJ.StoreManagementService.Api.IntegrationTests.FixtureCustomizations
{
    public class MaxLengthStringCustomization : ISpecimenBuilder
    {
        private readonly int _maxLength;

        public MaxLengthStringCustomization(int maxLength)
        {
            _maxLength = maxLength;
        }

        public object Create(object request, ISpecimenContext context)
        {
            if (request is PropertyInfo propertyInfo && propertyInfo.PropertyType == typeof(string))
            {
                var stringValue = (string)context.Resolve(propertyInfo.PropertyType);

                return stringValue?.Substring(0, Math.Min(stringValue.Length, _maxLength));
            }

            return new NoSpecimen();
        }
    }
}
