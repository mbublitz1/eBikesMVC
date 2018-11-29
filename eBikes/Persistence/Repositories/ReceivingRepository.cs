using eBikes.Core.DTO.Receiving;
using eBikes.Core.Models;
using eBikes.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace eBikes.Persistence.Repositories
{
    public class ReceivingRepository : IReceivingRepository
    {
        private readonly ApplicationDbContext _context;

        public ReceivingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ReceiveOrder GetReceiveOrderWithDetails(int id)
        {
            return _context.ReceiveOrders.Include(c => c.ReceiveOrderDetails).Single(p => p.PurchaseOrderID == id);
        }

        public void Add_ReceivedOrders(int purchaseOrderId, int poNumber, List<ReceiveOrderDetailDto> newOrders)
        {

            int idValue = 0;
            ReceiveOrder receiveNewOrder = null;
            ReceiveOrderDetail receiveNewOrderDetail = null;
            ReturnedOrderDetail returnedOrder = null;

            //Add new received order
            receiveNewOrder = new ReceiveOrder();
            receiveNewOrder.PurchaseOrderID = purchaseOrderId;
            receiveNewOrder.ReceiveDate = DateTime.Now;
            receiveNewOrder = _context.ReceiveOrders.Add(receiveNewOrder);
            idValue = receiveNewOrder.ReceiveOrderDetails.Count() + 1;

            //Loop through the details of order
            foreach (ReceiveOrderDetailDto item in newOrders)
            {
                if (item.Received != 0)
                {
                    if (item.Received <= item.QtyOutstanding)
                    {
                        receiveNewOrderDetail = new ReceiveOrderDetail();
                        receiveNewOrderDetail.PurchaseOrderDetailID = item.PurchaseOrderDetailId;
                        receiveNewOrderDetail.ReceiveOrderID = idValue;
                        receiveNewOrderDetail.QuantityReceived = item.Received;

                        receiveNewOrder.ReceiveOrderDetails.Add(receiveNewOrderDetail);

                        //Update quantities in parts table
                        var partExists = _context.Parts.Where(p => p.PartID == item.PartId).SingleOrDefault();

                        if (partExists != null)
                        {
                            if (partExists.QuantityOnOrder >= item.QtyOutstanding)
                            {
                                _context.Parts.Attach(partExists);
                                partExists.QuantityOnHand += item.Received;
                                partExists.QuantityOnOrder -= item.Received;
                                _context.Entry(partExists).State = EntityState.Modified;
                            }
                            else
                            {
                                throw new Exception("There is an issue with Part Number " + partExists.PartID +
                                                    " - " + partExists.Description +
                                                    " the quanity on order  (" + partExists.QuantityOnOrder + ") is less than that outsanding.");
                            }
                        }
                        else
                        {
                            throw new Exception("Part does not exist in database or there is no quantity on order");
                        }
                    }
                    else
                    {
                        throw new Exception("Receive Quantity can not be more than Outstanding quantity");
                    }
                }
                //Process returned items
                if (!String.IsNullOrEmpty(item.Returning.ToString()) && !String.IsNullOrEmpty(item.Reason))
                {
                    returnedOrder = new ReturnedOrderDetail();

                    returnedOrder.ReceiveOrderID = idValue;
                    returnedOrder.PurchaseOrderDetailID = item.PurchaseOrderDetailId;
                    returnedOrder.ItemDescription = item.PartDescription;
                    returnedOrder.Quantity = item.Returning;
                    returnedOrder.Reason = item.Reason;
                    returnedOrder.VendorPartNumber = item.PartId.ToString();

                    receiveNewOrder.ReturnedOrderDetails.Add(returnedOrder);
                }
            }

            //Process items in unorder cart
            var unordered =
                    _context.UnorderedPurchaseItemCarts.Where(up => up.PurchaseOrderNumber == 0);

            if (unordered.Count() > 0)
            {
                foreach (var unorderedItem in unordered)
                {
                    ReturnedOrderDetail unorderedReturn = new ReturnedOrderDetail();

                    unorderedReturn.ReceiveOrderID = idValue;
                    unorderedReturn.Quantity = unorderedItem.Quantity;
                    unorderedReturn.Reason = unorderedItem.Description;
                    unorderedReturn.VendorPartNumber = unorderedItem.VendorPartNumber;

                    receiveNewOrder.ReturnedOrderDetails.Add(unorderedReturn);
                    _context.UnorderedPurchaseItemCarts.Remove(unorderedItem);
                }
            }

            //Get count of outstanding items
            int outstandingSum = newOrders.Sum(item => item.QtyOutstanding);
            int receivedSum = newOrders.Sum(rs => rs.Received);

            if ((outstandingSum - receivedSum) == 0)
            {
                PurchaseOrder po = _context.PurchaseOrders.Find(purchaseOrderId);

                if (po != null)
                {
                    _context.PurchaseOrders.Attach(po);
                    po.Closed = true;
                    _context.Entry(po).State = EntityState.Modified;
                }
            }

            _context.SaveChanges();
        }

        public void Update_ClosePO(PurchaseOrder purchaseOrder, List<ReceiveOrderDetailDto> orderDetails)
        {

            PurchaseOrder poExists =
                _context.PurchaseOrders.Where(po => po.PurchaseOrderNumber == purchaseOrder.PurchaseOrderNumber).FirstOrDefault();

            if (poExists == null)
            {
                throw new Exception("Purchase Order does not exist in the database");
            }
            else
            {
                _context.PurchaseOrders.Attach(poExists);
                poExists.Closed = purchaseOrder.Closed;
                poExists.Notes = purchaseOrder.Notes;

                _context.Entry(poExists).State = EntityState.Modified;
                foreach (ReceiveOrderDetailDto item in orderDetails)
                {
                    Part partExists = _context.Parts.Where(p => p.PartID == item.PartId).SingleOrDefault();

                    if (partExists != null)
                    {
                        if (partExists.QuantityOnOrder >= item.QtyOutstanding)
                        {
                            _context.Parts.Attach(partExists);
                            partExists.QuantityOnOrder -= item.QtyOutstanding;
                            _context.Entry(partExists).State = EntityState.Modified;
                        }
                        else
                        {
                            throw new Exception("There is an issue with Part Number " + partExists.PartID +
                                                    " - " + partExists.Description +
                                                    " the quanity on order (" + partExists.QuantityOnOrder + ") is less than that outsanding.");
                        }
                    }
                    else
                    {
                        throw new Exception("Part does not exist");
                    }
                }
                _context.SaveChanges();
            }

        } //eom
    }
}
