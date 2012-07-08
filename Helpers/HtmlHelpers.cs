using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace HarderBetterFasterStronger.Helpers
{
    public static class HtmlHelpers
    {
        /// <summary>
        /// A helper based on the LinkExtensions.ActionLink helper.  If the current action/controller matches the action/controller used for
        /// the link only the link text is rendered.  This is for use in a menu where the current page shouldn't be a link.
        /// </summary>
        public static MvcHtmlString MenuActionLink(this HtmlHelper helper, string linkText, string actionName, string controllerName)
        {
            MvcHtmlString returnValue;

            if (helper.ViewContext.RouteData.Values["action"].ToString() == actionName && helper.ViewContext.RouteData.Values["controller"].ToString() == controllerName)
            {
                returnValue = new MvcHtmlString(linkText);
            }
            else
            {
                returnValue = LinkExtensions.ActionLink(helper, linkText, actionName, controllerName);
            }

            return returnValue;
        }
    }
}