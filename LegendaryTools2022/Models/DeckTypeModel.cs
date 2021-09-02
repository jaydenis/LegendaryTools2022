using LegendaryTools2022.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTools2022.Models
{
    public partial class LegeditItems
    {
        [JsonProperty("deckTypes")]
        public List<DeckTypeModel> DeckTypes { get; set; }
    }

    public partial class DeckTypeModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("defaultStyle")]
        public string DefaultStyle { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }
    }


}
