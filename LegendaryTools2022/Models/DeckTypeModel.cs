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

        [JsonProperty("nameEditable")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool NameEditable { get; set; }

        //[JsonProperty("deckType_attributes")]
        //public List<DeckTypeAttributeModel> DeckTypeAttributes { get; set; }
    }

    public partial class DeckTypeAttributeModel
    {
        [JsonProperty("attribute_name")]
        public string AttributeName { get; set; }

        [JsonProperty("displayName", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }

        [JsonProperty("attribute_type")]
        public string AttributeType { get; set; }

        [JsonProperty("attribute_value", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(BooleanConverter))]
        public long? AttributeValue { get; set; }

        [JsonProperty("isEditable", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool? IsEditable { get; set; }

        [JsonProperty("iconType", NullValueHandling = NullValueHandling.Ignore)]
        public string IconType { get; set; }
    }
}
