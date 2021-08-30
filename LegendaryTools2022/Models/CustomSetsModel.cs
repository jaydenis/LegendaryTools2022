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
        [JsonProperty("custom-sets")]
        public List<CustomSetModel> CustomSets { get; set; }
    }

    public class CustomSetModel
    {
        [JsonProperty("set_id")]
        public long SetId { get; set; }

        [JsonProperty("set_name")]
        public string SetName { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("date_created")]
        public DateTimeOffset DateCreated { get; set; }

        [JsonProperty("date_updated")]
        public DateTimeOffset DateUpdated { get; set; }
    }
}
