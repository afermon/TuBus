using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using WebApp.Helpers;

namespace WebApp.Models.Controls
{
    public class CtrlBreadcrumModel : CtrlBaseModel
    {   
        public HtmlHelper Helper { get; set; }

        public string DreadcrumbItems
        {
            get
            {
                var breadcrumb = new StringBuilder();

                breadcrumb.Append("<li class='breadcrumb-item'>");
                breadcrumb.Append(Helper.ActionLink(Helper.ViewContext.RouteData.Values["controller"].ToString().Titleize(),
                                  "Index", Helper.ViewContext.RouteData.Values["controller"].ToString()));

                breadcrumb.Append("</li>");

                if (Helper.ViewContext.RouteData.Values["action"].ToString() == "Index") return breadcrumb.ToString();

                breadcrumb.Append("<li class='breadcrumb-item active' id='breadcrumb-current'> ");
                breadcrumb.Append(Helper.ViewContext.RouteData.Values["action"].ToString().Titleize());
                breadcrumb.Append("</li>");

                return breadcrumb.ToString();
            }
        } 
    }
}