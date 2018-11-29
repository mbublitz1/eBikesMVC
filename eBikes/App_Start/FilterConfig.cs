using System.Web;
using System.Web.Mvc;
using eBikes.Core.Models;

namespace eBikes
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ErrHandling());
        }
    }
}
