using ReaAccountingSys.SharedKernel;
using ReaAccountingSys.SharedKernel.CommonValueObjects;

namespace ReaAccountingSys.Core.Shared
{
    public class ExternalAgent : Entity<Guid>
    {
        protected ExternalAgent() { }

        public ExternalAgent(EntityGuidID id, AgentTypeEnum agentType)
            : this()
        {
            Id = id;
            AgentType = agentType;
        }

        public AgentTypeEnum AgentType { get; protected set; }
    }

    public enum AgentTypeEnum : int
    {
        Customer = 1,
        Creditor = 2,
        Stockholder = 3,
        Vendor = 4,
        Employee = 5,
        Financier = 6
    }
}