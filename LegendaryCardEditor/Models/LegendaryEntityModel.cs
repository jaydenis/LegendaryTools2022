using Newtonsoft.Json;
using System.Collections.Generic;

namespace LegendaryCardEditor.Models
{

    public partial class DeckList
    {
        [JsonProperty("Decks", Required = Required.Always)]
        public List<Deck> Decks { get; set; }
    }

    public partial class Deck
    {
        [JsonProperty("DeckId", Required = Required.Always)]
        public int DeckId { get; set; }

        [JsonProperty("FolderName", Required = Required.Always)]
        public string FolderName { get; set; }

        [JsonProperty("DeckName", Required = Required.Always)]
        public string DeckName { get; set; }

        [JsonProperty("DeckDisplayName", Required = Required.Always)]
        public string DeckDisplayName { get; set; }

        [JsonProperty("Team", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public string Team { get; set; }

        [JsonProperty("CustomSetSetId", Required = Required.Always)]
        public int CustomSetSetId { get; set; }

        [JsonProperty("DeckTypeId", Required = Required.Always)]
        public int DeckTypeId { get; set; }

        [JsonProperty("Cards", Required = Required.Always)]
        public List<CardEntity> Cards { get; set; }
    }

}
