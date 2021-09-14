using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LegendaryCardEditor.Models
{

    public partial class LegendaryTemplateModel
    {
        [JsonProperty("TemplateId", Required = Required.Always)]
        public long TemplateId { get; set; }

        [JsonProperty("TemplateName", Required = Required.Always)]
        public string TemplateName { get; set; }

        [JsonProperty("TemplateDisplayName", Required = Required.Always)]
        public string TemplateDisplayName { get; set; }

        [JsonProperty("RectXArray", Required = Required.Always)]
        public string RectXArray { get; set; }

        [JsonProperty("RectYArray", Required = Required.Always)]
        public string RectYArray { get; set; }

        [JsonProperty("CardWidth", Required = Required.Always)]
        public long CardWidth { get; set; }

        [JsonProperty("CardHeight", Required = Required.Always)]
        public long CardHeight { get; set; }

        [JsonProperty("FrameImage", Required = Required.Always)]
        public string FrameImage { get; set; }

        [JsonProperty("CostImage", Required = Required.Always)]
        public string CostImage { get; set; }

        [JsonProperty("TextImage", Required = Required.Always)]
        public string TextImage { get; set; }

        [JsonProperty("UnderlayImage", Required = Required.Always)]
        public string UnderlayImage { get; set; }

        [JsonProperty("FormShowTeam", Required = Required.Always)]
        public bool FormShowTeam { get; set; }

        [JsonProperty("FormShowPowerPrimary", Required = Required.Always)]
        public bool FormShowPowerPrimary { get; set; }

        [JsonProperty("FormShowPowerSecondary", Required = Required.Always)]
        public bool FormShowPowerSecondary { get; set; }

        [JsonProperty("FormShowAttributes", Required = Required.Always)]
        public bool FormShowAttributes { get; set; }

        [JsonProperty("FormShowAttributesCost", Required = Required.Always)]
        public bool FormShowAttributesCost { get; set; }

        [JsonProperty("FormShowAttributesRecruit", Required = Required.Always)]
        public bool FormShowAttributesRecruit { get; set; }

        [JsonProperty("FormShowAttributesAttack", Required = Required.Always)]
        public bool FormShowAttributesAttack { get; set; }

        [JsonProperty("FormShowAttributesPiercing", Required = Required.Always)]
        public bool FormShowAttributesPiercing { get; set; }

        [JsonProperty("FormShowVictoryPoints", Required = Required.Always)]
        public bool FormShowVictoryPoints { get; set; }

        [JsonProperty("FormShowAttackCost", Required = Required.Always)]
        public bool FormShowAttackCost { get; set; }
        [JsonProperty("NumberInDeck", Required = Required.Always)]
        public int NumberInDeck { get; set; }
    }

}

