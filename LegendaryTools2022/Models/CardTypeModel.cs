using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTools2022.Models
{
    public partial class CardTypeModel
    {
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayname")]
        public string Displayname { get; set; }

        [JsonProperty("deck")]
        public string Deck { get; set; }
    }
}
