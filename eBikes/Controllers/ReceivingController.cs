using eBikes.Core;
using eBikes.Core.DTO;
using eBikes.Core.DTO.Receiving;
using eBikes.Core.Models;
using eBikes.Core.ViewModel;
using eBikes.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace eBikes.Controllers
{
    public class ReceivingController : Controller
    {
        private ApplicationDbContext _context;

        private readonly IUnitOfWork _unitOfWork;
        // GET: Receiving
        public ReceivingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Index action list all outstanding orders
        public ActionResult Index()
        {

            ModelState.Clear();
            var orderList = _unitOfWork.PurchaseOrders.GetOpenOrdersList();

            var viewModel = new ReceivngFormViewModel
            {
                OutstandingOrders = orderList,
                PurchaseOrderDetails = new List<PurchaseOrderDetailDto>(),
                ReceivedOrderDetails = new List<ReceiveOrderDetailDto>(),
                UnorderedParts = new List<UnorderedPurchaseItemCart>()

            };

            return View(viewModel);
        }


        //Details action gets specific outstanding order
        public ActionResult Details(int id)
        {
            //To utilize includes in this manner file needs to be using System.Data.Entity
            var purchaseOrder = _unitOfWork.PurchaseOrders.GetPurchaseOrderWithDetails(id);


            var viewModel = new ReceivngFormViewModel
            {
                PO = purchaseOrder.PurchaseOrderNumber,
                Vendor = purchaseOrder.Vendor.VendorName,
                Contact = purchaseOrder.Vendor.Phone,
                PurchaseOrderDetails = purchaseOrder.PurchaseOrderDetails
                .Where(pod => pod.PurchaseOrderID == purchaseOrder.PurchaseOrderID && (pod.Quantity - pod.ReceiveOrderDetails
                .Sum(rod => rod.QuantityReceived)) != 0)
                .Select(pod => new PurchaseOrderDetailDto
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
                UnorderedParts = _unitOfWork.UnorderedPurchaseItems.GetUnorderedPartsByPOId(id)
            };

            //Make sure variable name matches the variable used in the success function of Ajax
            return Json(new { data = viewModel.UnorderedParts }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public void Delete(int cartId, int poNumber)
        {
            var unorderedPart = _unitOfWork.UnorderedPurchaseItems.GetUnorderedPartById(cartId);

            try
            {
                if (unorderedPart != null)
                {
                    _unitOfWork.UnorderedPurchaseItems.Remove(unorderedPart);
                    _unitOfWork.Complete();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        [HttpPost]
        public ActionResult Receive(ReceivngFormViewModel model)
        {
            try
            {
                int poId = model.ReceivedOrderDetails[0].PurchaseOrderId;
                int poDetailId = model.ReceivedOrderDetails[0].PurchaseOrderDetailId;

                var receivedOrder = _unitOfWork.Receiving.GetReceiveOrderWithDetails(poId);

                if (receivedOrder == null)
                    return HttpNotFound();

                //receivedOrder.ReceiveOrderDetails = model.ReceivedOrderDetails;

                _unitOfWork.Receiving.Add_ReceivedOrders(poId, poDetailId, model.ReceivedOrderDetails);
                TempData["success"] = "Order has been updated successfully.";
                return RedirectToAction("Details", new { id = model.PO });
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return PartialView("_OrderDetails", model);
            }
        }

        [HttpPost]
        public ActionResult ForceCloser(ReceivngFormViewModel viewModel)
        {
            try
            {
                PurchaseOrder purchaseOrder = new PurchaseOrder
                {
                    PurchaseOrderNumber = viewModel.PO,
                    Closed = true,
                    Notes = viewModel.CloserReason

                };
                _unitOfWork.Receiving.Update_ClosePO(purchaseOrder, viewModel.ReceivedOrderDetails);

                return Json(JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return PartialView("_OrderDetails", viewModel);
            }
        }
    }

}