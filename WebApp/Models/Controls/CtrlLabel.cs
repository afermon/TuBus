using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Controls
{
    public class CtrlLabel : CtrlBaseModel
    {

        public string Label { get; set; }
        public string Id { get; set; }

        public CtrlLabel()
        {
            ViewName = "";
        }
    }
}