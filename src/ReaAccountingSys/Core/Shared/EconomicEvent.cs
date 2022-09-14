using ReaAccountingSys.SharedKernel;
using ReaAccountingSys.SharedKernel.CommonValueObjects;

namespace ReaAccountingSys.Core.Shared
{
    public class EconomicEvent : Entity<Guid>
    {
        protected EconomicEvent() { }

        public EconomicEvent(EntityGuidID id, EventTypeEnum eventType)
            : this()
        {
            Id = id;
            EventType = eventType;
        }

        public EventTypeEnum EventType { get; protected set; }
    }

    public enum EventTypeEnum : int
    {
        SalesInvoice = 1,
        LoanAgreement = 2,
        StockSubscription = 3,
        LoanInstallment = 4,
        DividentDeclaration = 5,
        LaborAcquisition = 6,
        InventoryReceived = 7,
        CashTransfered = 8
    }
}