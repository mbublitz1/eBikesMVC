using eBikes.Core.DTO.Receiving;
using eBikes.Core.Models;
using eBikes.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace eBikes.Persistence.Repositories
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public PurchaseOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<PurchaseOrder> GetOpenOrdersList()
        {
            return _context.PurchaseOrders.Where(od =>
                od.Closed == false && !String.IsNullOrEmpty(od.PurchaseOrderNumber.ToString()) && od.OrderDate != null).ToList();
        }

        public PurchaseOrder GetPurchaseOrderWithDetails(int id)
        {
            return _context.PurchaseOrders.Include(c => c.PurchaseOrderDetails).Single(p => p.PurchaseOrderID == id);
        }

        public List<ReceivingOrderList> Get_OutstandingOrders()
        {
            var results = _context.PurchaseOrders
                .Where(od =>
                    od.Closed == false && !String.IsNullOrEmpty(od.PurchaseOrderNumber.ToString()) &&
                    od.OrderDate != null)
                .Select(o => new ReceivingOrderList
                {
                    PurchaseOrderId = o.PurchaseOrderID,
                    PurchaseOrderNumber = o.PurchaseOrderNumber,
                    OrderDate = o.OrderDate,
                    VendorName = o.Vendor.VendorName,
                    VendorPhone = o.Vendor.Phone
                }).ToList();

            return results;

        } //eom
    }
}