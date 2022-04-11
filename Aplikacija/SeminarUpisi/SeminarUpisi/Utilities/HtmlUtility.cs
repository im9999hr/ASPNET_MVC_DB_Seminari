using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SeminarUpisi.Utilities
{
    public static class HtmlUtility
    {

        public static string IsActive(this HtmlHelper html, string control, string action)
        {
            var routeData = html.ViewContext.RouteData;

            string routeAction = (string)routeData.Values["action"];
            string routeControl = (string)routeData.Values["controller"];

            if (routeAction == "DodajPredbiljezbu")
            {
                routeAction = "Index";
            }
            else if (routeAction == "UnesiSeminar" || routeAction == "UrediSeminar" || routeAction == "IzbrisiSeminar")
            {
                routeAction = "Seminari";
            }
            else if (routeAction == "ObradiPredbiljezbu")
            {
                routeAction = "Predbiljezbe";
            }


            bool returnActive = control == routeControl && action == routeAction;

            return returnActive ? "active" : "";
        }
    }
}