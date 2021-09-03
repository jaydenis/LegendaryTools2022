using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTools2022.Models
{
    public partial class CustomSetsModel
    {
        [JsonProperty("custom_sets")]
        public List<CustomSetModel> CustomSets { get; set; }
    }

    public class CustomSetModel
    {
        [JsonProperty("set_id")]
        public long SetId { get; set; }

        [JsonProperty("set_name")]
        public string SetName { get; set; }

        [JsonProperty("base_work_path")]
        public string BaseWorkPath { get; set; }

        [JsonProperty("json_file")]
        public string DataFile { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("date_created")]
        public DateTime DateCreated { get; set; }

        [JsonProperty("date_updated")]
        public DateTime? DateUpdated { get; set; }
    }
}
