using ReaAccountingSys.SharedKernel;
using ReaAccountingSys.SharedKernel.CommonValueObjects;

namespace ReaAccountingSys.Core.Shared
{
    public class DomainUser : Entity<Guid>
    {
        protected DomainUser() { }

        public DomainUser(ExternalAgent agent, string userName, EmailAddress email)
        {
            Id = (agent is not null) ? agent.Id : throw new ArgumentNullException("An ExternalAgent is required.");
            UserName = userName ?? throw new ArgumentNullException("The user name is required.");
            Email = email ?? throw new ArgumentNullException("The domain user email is required.");
        }

        public string? UserName { get; private set; }
        public EmailAddress? Email { get; private set; }
    }
}