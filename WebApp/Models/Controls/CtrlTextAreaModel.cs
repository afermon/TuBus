namespace WebApp.Models.Controls
{
    public class CtrlTextAreaModel : CtrlBaseModel
    {
        public string Type { get; set; }
        public string Label { get; set; }
        public string PlaceHolder { get; set; }
        public string ColumnDataName { get; set; }
        public string Readyonly { get; set; }
        public string Class { get; set; }
        public int Rows { get; set; }
        public int Maxlength { get; set; }

        public CtrlTextAreaModel()
        {
            ViewName = "";
        }
    }
}