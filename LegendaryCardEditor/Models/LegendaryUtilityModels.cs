using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryCardEditor.Models
{
    public partial class DeckTypeModel
    {
        [JsonProperty("DeckTypeId", Required = Required.Always)]
        public long DeckTypeId { get; set; }

        [JsonProperty("DeckTypeName", Required = Required.Always)]
        public string DeckTypeName { get; set; }
        [JsonProperty("NumberOfCards", Required = Required.Always)]
        public int NumberOfCards { get; set; }
    }

    public partial class CardTypeModel
    {
        [JsonProperty("CardTypeId", Required = Required.Always)]
        public long CardTypeId { get; set; }

        [JsonProperty("CardTypeName", Required = Required.Always)]
        public string CardTypeName { get; set; }

        [JsonProperty("CardTypeDisplayName", Required = Required.Always)]
        public string CardTypeDisplayName { get; set; }
    }
}
