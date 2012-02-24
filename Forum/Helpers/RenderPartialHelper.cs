using System.IO;
using System.Web.Mvc;

namespace Forum.Helpers
{
    public static class RenderPartialHelper
    {
        // taken from http://craftycodeblog.com/2010/05/15/asp-net-mvc-render-partial-view-to-string/
        // and reprocessed to static method
        public static string RenderPartialViewToString(string viewName, object model, ControllerBase controller)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}