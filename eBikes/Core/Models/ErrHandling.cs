using System;
using System.Web.Mvc;

namespace eBikes.Core.Models
{
    public class ErrHandling : FilterAttribute, IExceptionFilter
    {
        //public override void OnException(ExceptionContext filterContext)
        //{
        //    Exception ex = filterContext.Exception;
        //    filterContext.ExceptionHandled = true;
        //    var controllerName = (string)filterContext.RouteData.Values["controller"];
        //    var actionName = (string)filterContext.RouteData.Values["action"];
        //    var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

        //    filterContext.Result = new ViewResult
        //    {
        //        ViewName = "Error",
        //        ViewData = new ViewDataDictionary(model)
        //    };

        //}
        public void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;
            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

            filterContext.Result = new PartialViewResult()
            {
                ViewName = "_Error",
                ViewData = new ViewDataDictionary(model)
            };
        }
    }
}

