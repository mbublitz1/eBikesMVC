using System;
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


        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    Exception ex = filterContext.Exception;
        //    filterContext.ExceptionHandled = true;
        //    var controllerName = (string)filterContext.RouteData.Values["controller"];
        //    var actionName = (string)filterContext.RouteData.Values["action"];
        //    var exp = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

        //    ReceivngFormViewModel viewModel = new ReceivngFormViewModel
        //    {
        //        ErrorMessage = exp.Exception.Message.ToString()
        //    };
        //    filterContext.Result = new PartialViewResult()
        //    {
        //        ViewName = "_Error",
        //        ViewData = new ViewDataDictionary(viewModel)
        //    };

        //}

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

            //Make sure variable name matches the variable used in the success function of Ajax
            return Json(new { data = viewModel.UnorderedParts }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Delete(int cartId, int poNumber)
        {
            var unorderedPart = _context.UnorderedPurchaseItemCarts.Single(u => u.CartID == cartId);

            if (unorderedPart != null)
            {
                _context.UnorderedPurchaseItemCarts.Remove(unorderedPart);
                _context.SaveChanges();
            }
        }
        [HttpPost]
        public ActionResult Receive(ReceivngFormViewModel model)
        {

            try
            {
                ReceivingOrderBLL sysmgr = new ReceivingOrderBLL();
                int poId = model.ReceivedOrderDetails[0].PurchaseOrderId;
                int poDetailId = model.ReceivedOrderDetails[0].PurchaseOrderDetailId;

                sysmgr.Add_ReceivedOrders(poId, poDetailId, model.ReceivedOrderDetails);
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
                ReceivingOrderBLL sysmgr = new ReceivingOrderBLL();
                PurchaseOrder purchaseOrder = new PurchaseOrder
                {
                    PurchaseOrderNumber = viewModel.PO,
                    Closed = true,
                    Notes = viewModel.CloserReason

                };
                sysmgr.Update_ClosePO(purchaseOrder, viewModel.ReceivedOrderDetails);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return PartialView("_OrderDetails", viewModel);
            }
        }
    }

}