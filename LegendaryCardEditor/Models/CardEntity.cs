using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryCardEditor
{
    public class CardEntity
    {

        [JsonProperty("CardId", Required = Required.Always)]
        public int CardId { get; set; }

        [JsonProperty("CardName", Required = Required.Always)]
        public string CardName { get; set; }

        [JsonProperty("CardDisplayName", Required = Required.Always)]
        public string CardDisplayName { get; set; }

        [JsonProperty("CardDisplayNameFont", Required = Required.Always)]
        public int CardDisplayNameFont { get; set; }

        [JsonProperty("CardDisplayNameSub", Required = Required.Always)]
        public string CardDisplayNameSub { get; set; }

        [JsonProperty("CardDisplayNameSubFont", Required = Required.Always)]
        public int CardDisplayNameSubFont { get; set; }

        [JsonProperty("TemplateId", Required = Required.Always)]
        public int TemplateId { get; set; }

        [JsonProperty("PowerPrimary", Required = Required.Always)]
        public string PowerPrimary { get; set; }

        [JsonProperty("PowerSecondary", Required = Required.Always)]
        public string PowerSecondary { get; set; }

        [JsonProperty("AttributeCost", Required = Required.Always)]
        public string AttributeCost { get; set; }

        [JsonProperty("AttributeAttack", Required = Required.Always)]
        public string AttributeAttack { get; set; }

        [JsonProperty("AttributeRecruit", Required = Required.Always)]
        public string AttributeRecruit { get; set; }

        [JsonProperty("AttributePiercing", Required = Required.Always)]
        public string AttributePiercing { get; set; }

        [JsonProperty("AttributeVictoryPoints", Required = Required.AllowNull)]
        public int AttributeVictoryPoints { get; set; }
        [JsonProperty("AttributeAttackDefense", Required = Required.Always)]
        public string AttributeAttackDefense { get; set; }


        [JsonProperty("CardText", Required = Required.Always)]
        public string CardText { get; set; }

        [JsonProperty("CardTextFont", Required = Required.Always)]
        public int CardTextFont { get; set; }

        [JsonProperty("NumberInDeck", Required = Required.Always)]
        public int NumberInDeck { get; set; }

        [JsonProperty("ArtWorkFile", Required = Required.Always)]
        public string ArtWorkFile { get; set; }

        [JsonProperty("ExportedCardFile", Required = Required.Always)]
        public string ExportedCardFile { get; set; }

        [JsonProperty("DeckId", Required = Required.Always)]
        public int DeckId { get; set; }

        [JsonProperty("TeamIconId", Required = Required.Always)]
        public int TeamIconId { get; set; }

        [JsonProperty("PowerPrimaryIconId", Required = Required.Always)]
        public int PowerPrimaryIconId { get; set; }

        [JsonProperty("PowerSecondaryIconId", Required = Required.Always)]
        public int PowerSecondaryIconId { get; set; }
    }
}
