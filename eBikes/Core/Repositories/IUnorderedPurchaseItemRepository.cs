using System.Collections.Generic;
using eBikes.Core.Models;

namespace eBikes.Core.Repositories
{
    public interface IUnorderedPurchaseItemRepository
    {
        List<UnorderedPurchaseItemCart> GetUnorderedPartsByPOId(int id);
        UnorderedPurchaseItemCart GetUnorderedPartById(int cartId);
        void Remove(UnorderedPurchaseItemCart unorderedPart);
    }
}