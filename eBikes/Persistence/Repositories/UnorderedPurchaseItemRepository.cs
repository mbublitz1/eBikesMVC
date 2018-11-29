using eBikes.Core.Models;
using eBikes.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace eBikes.Persistence.Repositories
{

    public class UnorderedPurchaseItemRepository : IUnorderedPurchaseItemRepository
    {
        private readonly ApplicationDbContext _context;

        public UnorderedPurchaseItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UnorderedPurchaseItemCart> GetUnorderedPartsByPOId(int id)
        {
            return _context.UnorderedPurchaseItemCarts
                .Where(u => u.PurchaseOrderNumber.Equals(id)).ToList();
        }

        public UnorderedPurchaseItemCart GetUnorderedPartById(int cartId)
        {
            return _context.UnorderedPurchaseItemCarts.Single(u => u.CartID == cartId);
        }

        public void Remove(UnorderedPurchaseItemCart unorderedPart)
        {
            _context.UnorderedPurchaseItemCarts.Remove(unorderedPart);
        }
    }
}