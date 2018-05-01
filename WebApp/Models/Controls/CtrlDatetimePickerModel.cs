namespace WebApp.Models.Controls
{
    public class CtrlDatetimePickerModel : CtrlBaseModel
    {
        public string Format { get; set; } /* L -> date, LT -> Time, L LT -> datetime*/
        public string Label { get; set; }
        public string ColumnDataName { get; set; }
        public string Readyonly { get; set; }

        public CtrlDatetimePickerModel()
        {
            ViewName = "";
        }
    }
}