using ReaAccountingSys.SharedKernel;
using ReaAccountingSys.SharedKernel.CommonValueObjects;

namespace ReaAccountingSys.Core.Shared
{
    public class EconomicResource : Entity<Guid>
    {
        protected EconomicResource() { }

        public EconomicResource(EntityGuidID id, ResourceTypeEnum resourceType)
            : this()
        {
            Id = id;
            ResourceType = resourceType;
        }

        public ResourceTypeEnum ResourceType { get; protected set; }
    }

    public enum ResourceTypeEnum : int
    {
        Cash = 1,
        Inventory = 2,
        Product = 3,
        Labor = 4,
    }
}