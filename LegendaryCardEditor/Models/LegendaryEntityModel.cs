using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LegendaryCardEditor.Models
{

    public partial class CustomSetsModel
    {
        [JsonProperty("CustomSets", Required = Required.Always)]
        public List<CustomSet> CustomSets { get; set; }
    }

    public partial class CustomSet
    {
        [JsonProperty("SetId", Required = Required.Always)]
        public long SetId { get; set; }

        [JsonProperty("SetName", Required = Required.Always)]
        public string SetName { get; set; }

        [JsonProperty("SetWorkPath", Required = Required.Always)]
        public string SetWorkPath { get; set; }

        [JsonProperty("SetDisplayName", Required = Required.Always)]
        public string SetDisplayName { get; set; }

        [JsonProperty("DateCreated", Required = Required.Always)]
        public DateTime DateCreated { get; set; }

        [JsonProperty("DateUpdated", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DateUpdated { get; set; }

        [JsonProperty("Decks", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<Deck> Decks { get; set; }
    }

    public partial class Deck
    {
        [JsonProperty("DeckId", Required = Required.Always)]
        public long DeckId { get; set; }

        [JsonProperty("DeckName", Required = Required.Always)]
        public string DeckName { get; set; }

        [JsonProperty("DeckDisplayName", Required = Required.Always)]
        public string DeckDisplayName { get; set; }

        [JsonProperty("TeamIconId", Required = Required.Always)]
        public long TeamIconId { get; set; }

        [JsonProperty("CustomSetSetId", Required = Required.Always)]
        public long CustomSetSetId { get; set; }

        [JsonProperty("DeckTypeId", Required = Required.Always)]
        public long DeckTypeId { get; set; }

        [JsonProperty("Cards", Required = Required.Always)]
        public List<Card> Cards { get; set; }
    }

    public partial class Card
    {
        [JsonProperty("CardId", Required = Required.Always)]
        public long CardId { get; set; }

        [JsonProperty("CardName", Required = Required.Always)]
        public string CardName { get; set; }

        [JsonProperty("CardDisplayName", Required = Required.Always)]
        public string CardDisplayName { get; set; }

        [JsonProperty("CardDisplayNameFont", Required = Required.Always)]
        public long CardDisplayNameFont { get; set; }

        [JsonProperty("CardDisplayNameSub", Required = Required.Always)]
        public string CardDisplayNameSub { get; set; }

        [JsonProperty("CardDisplayNameSubFont", Required = Required.Always)]
        public long CardDisplayNameSubFont { get; set; }

        [JsonProperty("CardTypeId", Required = Required.Always)]
        public long CardTypeId { get; set; }

        [JsonProperty("CardTemplateTemplateId", Required = Required.Always)]
        public long CardTemplateTemplateId { get; set; }

        [JsonProperty("PowerPrimary", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string PowerPrimary { get; set; }

        [JsonProperty("PowerSecondary", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string PowerSecondary { get; set; }

        [JsonProperty("AttributeCost", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AttributeCost { get; set; }

        [JsonProperty("AttributeAttack", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public long AttributeAttack { get; set; }

        [JsonProperty("AttributeRecruit", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string AttributeRecruit { get; set; }

        [JsonProperty("AttributePiercing", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string AttributePiercing { get; set; }

        [JsonProperty("AttributeVictoryPoints", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string AttributeVictoryPoints { get; set; }

        [JsonProperty("CardText", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string CardText { get; set; }

        [JsonProperty("CardTextFont", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long CardTextFont { get; set; }

        [JsonProperty("NumberInDeck", Required = Required.Always)]
        public long NumberInDeck { get; set; }

        [JsonProperty("ArtWorkFile", Required = Required.Always)]
        public string ArtWorkFile { get; set; }

        [JsonProperty("ExportedCardFile", Required = Required.Always)]
        public string ExportedCardFile { get; set; }

        [JsonProperty("DeckId", Required = Required.Always)]
        public long DeckId { get; set; }

        [JsonProperty("TeamIconId", Required = Required.Always)]
        public long TeamIconId { get; set; }

        [JsonProperty("PowerPrimaryIconId", Required = Required.Always)]
        public long PowerPrimaryIconId { get; set; }

        [JsonProperty("PowerSecondaryIconId", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        public long PowerSecondaryIconId { get; set; }
    }

    public partial class CustomSetsModel
    {
        public static CustomSetsModel FromJson(string json)
        {
            return JsonConvert.DeserializeObject<CustomSetsModel>(json, LegendaryCardEditor.Models.Converter.Settings);
        }
    }

    public static class Serialize
    {
        public static string ToJson(this CustomSetsModel self)
        {
            return JsonConvert.SerializeObject(self, LegendaryCardEditor.Models.Converter.Settings);
        }
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(long) || t == typeof(long?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
