using Microsoft.AspNetCore.Mvc;

namespace GaripSozluk.Common.Extensions
{
    public static class ControllerExtensions
    {
        public static string Action<T>(this Controller controller, string actionName, object routeValues = null) where T : Controller
        {
            var name = typeof(T).Name;

            string controllerName = name.EndsWith("Controller") ? name.Substring(0, name.Length - 10) : name;

            return controller.Url.Action(actionName, controllerName, routeValues);
        }
    }
}
