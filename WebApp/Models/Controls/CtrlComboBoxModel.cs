using System.Collections.Generic;


namespace WebApp.Models.Controls
{
    public class CtrlComboBoxModel : CtrlBaseModel
    {
        public string Label { get; set; }
        public string ColumnDataName { get; set; }
        public string Readyonly { get; set; }
        public Dictionary<string, string> TextValue;
        public string ClassAttribute { get; set; }
        public string Multiple { get; set; }

        public CtrlComboBoxModel()
        {
            ViewName = "";
        }
    }
}