using Newtonsoft.Json;

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

    public partial class TemplateTypeModel
    {
        [JsonProperty("templateId", Required = Required.Always)]
        public int TemplateId { get; set; }

        [JsonProperty("templateType", Required = Required.Always)]
        public string TemplateType { get; set; }
    }


    public partial class CardTypeModel
    {
        [JsonProperty("CardTypeId", Required = Required.Always)]
        public long CardTypeId { get; set; }

        [JsonProperty("CardTypeName", Required = Required.Always)]
        public string CardTypeName { get; set; }

        [JsonProperty("CardTypeDisplayName", Required = Required.Always)]
        public string CardTypeDisplayName { get; set; }

        [JsonProperty("CardGroup", Required = Required.Always)]
        public string CardGroup { get; set; }
    }

    public partial class LegendaryKeyword
    {
        [JsonProperty("Id", Required = Required.Always)]
        public long Id { get; set; }

        [JsonProperty("KeywordName", Required = Required.Always)]
        public string KeywordName { get; set; }

        [JsonProperty("KeywordDescription", Required = Required.Always)]
        public string KeywordDescription { get; set; }
    }

    public partial class Templates
    {
        [JsonProperty("templateId", Required = Required.Always)]
        public int TemplateId { get; set; }

        [JsonProperty("templateName", Required = Required.Always)]
        public string TemplateName { get; set; }
        [JsonProperty("templateDisplayName", Required = Required.Always)]
        public string TemplateDisplayName { get; set; }

        [JsonProperty("templateType", Required = Required.Always)]
        public string TemplateType { get; set; }
    }
}
