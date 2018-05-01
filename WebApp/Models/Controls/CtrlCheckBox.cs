using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Controls
{
    public class CtrlCheckBox : CtrlBaseModel
    {
        public string Label { get; set; }
        public string ColumnDataName { get; set; }

        public CtrlCheckBox()
        {
            ViewName = "";
        }
    }
}