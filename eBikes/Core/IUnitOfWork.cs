using eBikes.Core.Repositories;

namespace eBikes.Core
{
    public interface IUnitOfWork
    {
        IReceivingRepository Receiving { get; }
        IPurchaseOrderRepository PurchaseOrders { get; }
        IUnorderedPurchaseItemRepository UnorderedPurchaseItems { get; }

        void Complete();
    }
}