using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using Entities;

namespace WebApp.Models.Controls
{
    public class CtrlDropDownModel : CtrlBaseModel
    {
        public string Label { get; set; }
        public string ListId { get; set; }

        private string URL_API_LISTs = "http://localhost:57056/api/List/";

        public string ListOptions
        {
            get
            {
                var htmlOptions = "";
                var lst = GetOptionsFromApi();

                foreach (var option in lst)
                {
                    htmlOptions += "<option value='" + option.Value + "'>" + option.Description + "</option>";
                }
                return htmlOptions;
            }
        }

        private List<ListItem> GetOptionsFromApi()
        {
            var client = new WebClient();
            var response = client.DownloadString(URL_API_LISTs + ListId);
            var options = JsonConvert.DeserializeObject<List<ListItem>>(response);
            return options;
        }

        public CtrlDropDownModel()
        {
            ViewName = "";
        }
    }
}