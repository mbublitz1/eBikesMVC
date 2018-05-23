﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eBike.DTO.POCO;
using eBike.System.BLL;
using eBikes.Models;
using eBikes.ViewModel;

namespace eBikes.Controllers
{
    public class ReceivingController : Controller
    {
        private ApplicationDbContext _context;
        // GET: Receiving
        public ReceivingController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //Index action list all outstanding orders
        public ActionResult Index()
        {
            var orderList = _context.PurchaseOrders.Where(od =>
                od.Closed == false && !String.IsNullOrEmpty(od.PurchaseOrderNumber.ToString()) && od.OrderDate != null).ToList();

            var viewModel = new ReceivngFormViewModel
            {
                OutstandingOrders = orderList,
                ReceivedOrderDetails = new List<ReceivedOrderDetail>(),
                UnorderedParts = new List<UnorderedPurchaseItemCart>()

            };

            return View(viewModel);
        }

        //Details action gets specific outstanding order
        public ActionResult Details(int id)
        {
            var purchaseOrder = _context.PurchaseOrders.Single(p => p.PurchaseOrderNumber == id);

            var viewModel = new ReceivngFormViewModel
            {
                PO = purchaseOrder.PurchaseOrderNumber,
                Vendor = purchaseOrder.Vendor.VendorName,
                Contact = purchaseOrder.Vendor.Phone,
                //OutstandingOrders = _context.PurchaseOrders.Where(od =>
                //    od.Closed == false && !String.IsNullOrEmpty(od.PurchaseOrderNumber.ToString()) &&
                //    od.OrderDate != null).ToList(),
                ReceivedOrderDetails = _context.PurchaseOrderDetails
                .Where(pod => pod.PurchaseOrderID == purchaseOrder.PurchaseOrderID && (pod.Quantity - pod.ReceiveOrderDetails
                .Sum(rod => rod.QuantityReceived)) != 0)
                .Select(pod => new ReceivedOrderDetail
                {
                    PurchaseOrderId = pod.PurchaseOrderID,
                    PurchaseOrderDetailId = pod.PurchaseOrderDetailID,
                    PartId = pod.PartID,
                    PartDescription = pod.Part.Description,
                    QtyOnOrder = pod.Quantity,
                    QtyOutstanding = pod.ReceiveOrderDetails.Select(rod => rod.QuantityReceived).Any() ?
                        pod.Quantity - pod.ReceiveOrderDetails.Sum(rod => rod.QuantityReceived) : pod.Quantity
                }).ToList()
            };
          
            return PartialView("_OrderDetails", viewModel);
        }
        [HttpPost]

        public ActionResult CreateUnorderedPart(ReceivngFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);

                var viewModel = new ReceivngFormViewModel
                {
                    UnorderedPart = new UnorderedPurchaseItemCart()
                };

                return PartialView("_UnorderedParts", viewModel);

            }
            else
            {
                _context.UnorderedPurchaseItemCarts.Add(model.UnorderedPart);
                _context.SaveChanges();
            }

            return RedirectToAction("GetUnorderedParts", new { id = model.UnorderedPart.PurchaseOrderNumber });
        }

        public ActionResult GetUnorderedParts(int id)
        {
            var viewModel = new ReceivngFormViewModel()
            {
                UnorderedParts = _context.UnorderedPurchaseItemCarts
                    .Where(u => u.PurchaseOrderNumber.Equals(id)).ToList(),
            };

            return PartialView("_UnorderedPartsDetail", viewModel);
        }
        [HttpPost]
        public ActionResult Delete(int cartId, int poNumber )
        {
            var unorderedPart = _context.UnorderedPurchaseItemCarts.Single(u => u.CartID == cartId);

            if (unorderedPart != null)
            {
                _context.UnorderedPurchaseItemCarts.Remove(unorderedPart);
                _context.SaveChanges();
            }

            var viewModel = new ReceivngFormViewModel
            {
                UnorderedParts = _context.UnorderedPurchaseItemCarts.Where(u => u.PurchaseOrderNumber == poNumber)
            };

            return PartialView("_UnorderedPartsDetail", viewModel);
        }
        [HttpPost]
        public ActionResult Receive(ReceivngFormViewModel model)
        {
            ReceivingOrderBLL sysmgr = new ReceivingOrderBLL();
            int poId = model.ReceivedOrderDetails[0].PurchaseOrderId;
            int poDetailId = model.ReceivedOrderDetails[0].PurchaseOrderDetailId;

            sysmgr.Add_ReceivedOrders(poId, poDetailId, model.ReceivedOrderDetails);

            return RedirectToAction("Details", new { id = model.PO });
        }
    }
}