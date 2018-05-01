using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApp.Models.Controls;

namespace WebApp.Helpers
{
    public static class ControlExtensions
    {
        public static HtmlString CtrlTable(this HtmlHelper html, string viewName, string id, string title,
            string columnsTitle, string columnsDataName, string onSelectFunction)
        {
            var ctrl = new CtrlTableModel
            {
                ViewName = viewName,
                Id = id,
                Title = title,
                Columns = columnsTitle,
                ColumnsDataName = columnsDataName,
                FunctionName = onSelectFunction
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlBreadcrum(this HtmlHelper html, string id)
        {
            var ctrl = new CtrlBreadcrumModel
            {
                ViewName = "Dashboard",
                Id = id,
                Helper = html
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlInput(this HtmlHelper html, string id, string type, string label, string placeHolder = "", string columnDataName = "", bool onlyread = false)
        {
            var ctrl = new CtrlInputModel
            {
                Id = id,
                Type = type,
                Label = label,
                PlaceHolder = placeHolder,
                ColumnDataName = columnDataName,
                Readyonly = onlyread ? "readonly" : "",
                Class = onlyread ? "form-control-plaintext" : "form-control"
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlTextArea(this HtmlHelper html, string id, string type, string label, string placeHolder = "", string columnDataName = "", bool onlyread = false, int rows = 3, int maxlength = 100)
        {
            var ctrl = new CtrlTextAreaModel
            {
                Id = id,
                Type = type,
                Label = label,
                PlaceHolder = placeHolder,
                ColumnDataName = columnDataName,
                Readyonly = onlyread ? "readonly" : "",
                Class = onlyread ? "form-control-plaintext" : "form-control",
                Rows = rows,
                Maxlength = maxlength
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlDatetimePicker(this HtmlHelper html, string id, string format, string label, string columnDataName = "", bool onlyread = false)
        {
            var ctrl = new CtrlDatetimePickerModel
            {
                Id = id,
                Format = format,
                Label = label,
                ColumnDataName = columnDataName,
                Readyonly = onlyread ? "readonly" : ""
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlButton(this HtmlHelper html, string viewName, string id, string label, string onClickFunction = "", string buttonType = "primary")
        {
            var ctrl = new CtrlButtonModel
            {
                ViewName = viewName,
                Id = id,
                Label = label,
                FunctionName = onClickFunction,
                ButtonType = buttonType
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlDropDown(this HtmlHelper html, string id, string label, string listId)
        {
            var ctrl = new CtrlDropDownModel
            {
                Id = id,
                Label = label,
                ListId = listId
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlComboBox(this HtmlHelper html, string id, string label, string columnDataName = "", bool onlyread = false, string classAttribute = "", bool multiple = false)
        {
            var ctrl = new CtrlComboBoxModel
            {
                Id = id,
                Label = label,
                ColumnDataName = columnDataName,
                Readyonly = onlyread ? "disabled" : "",
                ClassAttribute = classAttribute,
                Multiple = multiple ? "multiple"  : ""
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlCheckBox(this HtmlHelper html, string id, string label, string columnDataName = "")
        {
            var ctrl = new CtrlCheckBox
            {
                Id = id,
                Label = label,
                ColumnDataName = columnDataName
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlParagraph(this HtmlHelper html, string id, string label, string columnDataName = "")
        {
            var ctrl = new CtrlCheckBox
            {
                Id = id,
                Label = label,
                ColumnDataName = columnDataName
            };

            return new HtmlString(ctrl.GetHtml());
        }

        public static HtmlString CtrlLabel(this HtmlHelper html, string id, string label)
        {
            var ctrl = new CtrlLabel
            {
                Id = id,
                Label = label
            };

            return new HtmlString(ctrl.GetHtml());
        }
    }
}