using eBikes.Core;
using eBikes.Core.Repositories;
using eBikes.Persistence.Repositories;

namespace eBikes.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IReceivingRepository Receiving { get; }
        public IPurchaseOrderRepository PurchaseOrders { get; }

        public IUnorderedPurchaseItemRepository UnorderedPurchaseItems { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            Receiving = new ReceivingRepository(context);
            PurchaseOrders = new PurchaseOrderRepository(context);
            UnorderedPurchaseItems = new UnorderedPurchaseItemRepository(context);
            _context = context;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}