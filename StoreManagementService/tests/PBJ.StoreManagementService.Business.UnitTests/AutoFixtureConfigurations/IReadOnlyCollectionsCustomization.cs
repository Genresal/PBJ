using System.Collections;
using AutoFixture.Kernel;
using System.Reflection;

namespace PBJ.StoreManagementService.Business.UnitTests.AutoFixtureConfigurations
{
    public class IReadOnlyCollectionsCustomization : ISpecimenBuilder
    {
        public object? Create(object request, ISpecimenContext context)
        {
            if (request is PropertyInfo propertyInfo)
            {
                if (IsReadOnlyCollection(propertyInfo.PropertyType))
                {
                    return null;
                }
            }

            return new NoSpecimen();
        }

        private static bool IsReadOnlyCollection(Type type)
        {
            return type.IsGenericType &&
                   type.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>);
        }
    }
}
