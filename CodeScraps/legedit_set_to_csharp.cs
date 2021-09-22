namespace LegendarySetEditor.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class CustomSetsModel
    {
        [JsonProperty("legedititems", Required = Required.Always)]
        public List<Legedititem> Legedititems { get; set; }
    }

    public partial class Legedititem
    {
        [JsonProperty("template", Required = Required.Always)]
        public string Template { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("attributes", Required = Required.Always)]
        public List<Attribute> Attributes { get; set; }

        [JsonProperty("cards", Required = Required.Always)]
        public CardsUnion Cards { get; set; }
    }

    public partial class Attribute
    {
        [JsonProperty("name", Required = Required.Always)]
        public AttributeName Name { get; set; }

        [JsonProperty("value", Required = Required.Always)]
        public ValueUnion Value { get; set; }
    }

    public partial class CardElement
    {
        [JsonProperty("template", Required = Required.Always)]
        public List<TentacledTemplate> Template { get; set; }

        [JsonProperty("style", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public StyleEnum? Style { get; set; }
    }

    public partial class PurpleTemplate
    {
        [JsonProperty("bgimage", Required = Required.Always)]
        public BgimageElement Bgimage { get; set; }

        [JsonProperty("iconbg", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<Attribute> Iconbg { get; set; }

        [JsonProperty("image", Required = Required.Always)]
        public ImageUnion Image { get; set; }

        [JsonProperty("text", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public TextUnion? Text { get; set; }

        [JsonProperty("elementgroup", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<Elementgroup> Elementgroup { get; set; }

        [JsonProperty("textarea", Required = Required.Always)]
        public TextareaElement Textarea { get; set; }

        [JsonProperty("scrollingtextarea", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public Scrollingtextarea Scrollingtextarea { get; set; }

        [JsonProperty("property", Required = Required.Always)]
        public Attribute Property { get; set; }

        [JsonProperty("styles", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<PurpleStyle> Styles { get; set; }

        [JsonProperty("icon", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<Attribute> Icon { get; set; }

        [JsonProperty("cardname", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public TemplateCardname Cardname { get; set; }
    }

    public partial class BgimageElement
    {
        [JsonProperty("name", Required = Required.Always)]
        public BgimageName Name { get; set; }

        [JsonProperty("path", Required = Required.Always)]
        public string Path { get; set; }

        [JsonProperty("zoom", Required = Required.Always)]
        public string Zoom { get; set; }

        [JsonProperty("imageOffsetX", Required = Required.Always)]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long ImageOffsetX { get; set; }

        [JsonProperty("imageOffsetY", Required = Required.Always)]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long ImageOffsetY { get; set; }
    }

    public partial class TemplateCardname
    {
        [JsonProperty("name", Required = Required.Always)]
        public CardnameName Name { get; set; }

        [JsonProperty("value", Required = Required.Always)]
        public string Value { get; set; }

        [JsonProperty("textsize", Required = Required.Always)]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long Textsize { get; set; }

        [JsonProperty("subnameValue", Required = Required.Always)]
        public PurpleSubnameValue SubnameValue { get; set; }

        [JsonProperty("subnamesize", Required = Required.Always)]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long Subnamesize { get; set; }
    }

    public partial class Elementgroup
    {
        [JsonProperty("name", Required = Required.Always)]
        public ElementgroupName Name { get; set; }

        [JsonProperty("visible", Required = Required.Always)]
        [JsonConverter(typeof(FluffyParseStringConverter))]
        public bool Visible { get; set; }

        [JsonProperty("icon", Required = Required.Always)]
        public Attribute Icon { get; set; }

        [JsonProperty("text", Required = Required.Always)]
        public TextareaElement Text { get; set; }
    }

    public partial class TextareaElement
    {
        [JsonProperty("name", Required = Required.Always)]
        public TextareaName Name { get; set; }

        [JsonProperty("value", Required = Required.Always)]
        public string Value { get; set; }

        [JsonProperty("textsize", Required = Required.Always)]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long Textsize { get; set; }

        [JsonProperty("textsizebold", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long? Textsizebold { get; set; }
    }

    public partial class Scrollingtextarea
    {
        [JsonProperty("name", Required = Required.Always)]
        public ScrollingtextareaName Name { get; set; }

        [JsonProperty("value", Required = Required.Always)]
        public ScrollingtextareaValue Value { get; set; }

        [JsonProperty("textsize", Required = Required.Always)]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long Textsize { get; set; }

        [JsonProperty("textsizebold", Required = Required.Always)]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long Textsizebold { get; set; }

        [JsonProperty("fontname", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Fontname { get; set; }

        [JsonProperty("fontstyle", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long? Fontstyle { get; set; }
    }

    public partial class PurpleStyle
    {
        [JsonProperty("name", Required = Required.Always)]
        public StyleEnum Name { get; set; }

        [JsonProperty("cardname", Required = Required.Always)]
        public PurpleCardname Cardname { get; set; }

        [JsonProperty("icon", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<Attribute> Icon { get; set; }
    }

    public partial class PurpleCardname
    {
        [JsonProperty("name", Required = Required.Always)]
        public CardnameName Name { get; set; }

        [JsonProperty("value", Required = Required.Always)]
        public string Value { get; set; }

        [JsonProperty("textsize", Required = Required.Always)]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long Textsize { get; set; }

        [JsonProperty("subnameValue", Required = Required.Always)]
        public FluffySubnameValue SubnameValue { get; set; }

        [JsonProperty("subnamesize", Required = Required.Always)]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long Subnamesize { get; set; }

        [JsonProperty("fontname", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Fontname { get; set; }

        [JsonProperty("fontstyle", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(PurpleParseStringConverter))]
        public long? Fontstyle { get; set; }
    }

    public partial class CardsClass
    {
        [JsonProperty("card", Required = Required.Always)]
        public PurpleCard Card { get; set; }
    }

    public partial class PurpleCard
    {
        [JsonProperty("template", Required = Required.Always)]
        public List<StickyTemplate> Template { get; set; }
    }

    public partial class FluffyTemplate
    {
        [JsonProperty("bgimage", Required = Required.Always)]
        public BgimageElement Bgimage { get; set; }

        [JsonProperty("image", Required = Required.Always)]
        public BgimageElement Image { get; set; }

        [JsonProperty("icon", Required = Required.Always)]
        public List<Attribute> Icon { get; set; }

        [JsonProperty("text", Required = Required.Always)]
        public List<TextareaElement> Text { get; set; }

        [JsonProperty("textarea", Required = Required.Always)]
        public TextareaElement Textarea { get; set; }

        [JsonProperty("property", Required = Required.Always)]
        public Attribute Property { get; set; }

        [JsonProperty("styles", Required = Required.Always)]
        public List<FluffyStyle> Styles { get; set; }
    }

    public partial class FluffyStyle
    {
        [JsonProperty("name", Required = Required.Always)]
        public StyleEnum Name { get; set; }

        [JsonProperty("cardname", Required = Required.Always)]
        public TemplateCardname Cardname { get; set; }
    }

    public enum AttributeName { Attack, AttackIcon, CostIcon, Defattack, NumberInDeck, PiercingIcon, Power, PowerOverlay, RecruitIcon, SecondPower, SecondPowerOverlay, Team, Victory, VillainTeam, Vp };

    public enum ValueValue { Attack, Covert, Instinct, Lom, None, Piercing, Range, Recruit, Strength, Tech, The8, Victory };

    public enum StyleEnum { BaseSet, Villains };

    public enum BgimageName { BackCost, BackText, Image, TextOverlay };

    public enum CardnameName { CardName };

    public enum PurpleSubnameValue { HenchmanVillain, Mastermind, MastermindTacticDeckname };

    public enum ElementgroupName { Attack, Cost, Piercing, Recruit };

    public enum TextareaName { AttackValue, CardText, Cost, CostText, PiercingValue, RecruitValue, VictoryValue };

    public enum ScrollingtextareaName { FlavorText };

    public enum ScrollingtextareaValue { Empty, LtGGtLtGGtYesIAm };

    public enum FluffySubnameValue { Deckname };

    public partial struct ValueUnion
    {
        public ValueValue? Enum;
        public long? Integer;

        public static implicit operator ValueUnion(ValueValue Enum)
        {
            return new ValueUnion { Enum = Enum };
        }
        public static implicit operator ValueUnion(long Integer)
        {
            return new ValueUnion { Integer = Integer };
        }
    }

    public partial struct ImageUnion
    {
        public BgimageElement BgimageElement;
        public List<BgimageElement> BgimageElementArray;

        public static implicit operator ImageUnion(BgimageElement BgimageElement)
        {
            return new ImageUnion { BgimageElement = BgimageElement };
        }
        public static implicit operator ImageUnion(List<BgimageElement> BgimageElementArray)
        {
            return new ImageUnion { BgimageElementArray = BgimageElementArray };
        }
    }

    public partial struct TextUnion
    {
        public TextareaElement TextareaElement;
        public List<TextareaElement> TextareaElementArray;

        public static implicit operator TextUnion(TextareaElement TextareaElement)
        {
            return new TextUnion { TextareaElement = TextareaElement };
        }
        public static implicit operator TextUnion(List<TextareaElement> TextareaElementArray)
        {
            return new TextUnion { TextareaElementArray = TextareaElementArray };
        }
    }

    public partial struct TentacledTemplate
    {
        public PurpleTemplate PurpleTemplate;
        public string String;

        public static implicit operator TentacledTemplate(PurpleTemplate PurpleTemplate)
        {
            return new TentacledTemplate { PurpleTemplate = PurpleTemplate };
        }
        public static implicit operator TentacledTemplate(string String)
        {
            return new TentacledTemplate { String = String };
        }
    }

    public partial struct StickyTemplate
    {
        public FluffyTemplate FluffyTemplate;
        public string String;

        public static implicit operator StickyTemplate(FluffyTemplate FluffyTemplate)
        {
            return new StickyTemplate { FluffyTemplate = FluffyTemplate };
        }
        public static implicit operator StickyTemplate(string String)
        {
            return new StickyTemplate { String = String };
        }
    }

    public partial struct CardsUnion
    {
        public List<CardElement> CardElementArray;
        public CardsClass CardsClass;

        public static implicit operator CardsUnion(List<CardElement> CardElementArray)
        {
            return new CardsUnion { CardElementArray = CardElementArray };
        }
        public static implicit operator CardsUnion(CardsClass CardsClass)
        {
            return new CardsUnion { CardsClass = CardsClass };
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
                AttributeNameConverter.Singleton,
                ValueUnionConverter.Singleton,
                ValueValueConverter.Singleton,
                CardsUnionConverter.Singleton,
                StyleEnumConverter.Singleton,
                TentacledTemplateConverter.Singleton,
                BgimageNameConverter.Singleton,
                CardnameNameConverter.Singleton,
                PurpleSubnameValueConverter.Singleton,
                ElementgroupNameConverter.Singleton,
                TextareaNameConverter.Singleton,
                ImageUnionConverter.Singleton,
                ScrollingtextareaNameConverter.Singleton,
                ScrollingtextareaValueConverter.Singleton,
                FluffySubnameValueConverter.Singleton,
                TextUnionConverter.Singleton,
                StickyTemplateConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class AttributeNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(AttributeName) || t == typeof(AttributeName?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Attack":
                    return AttributeName.Attack;
                case "Attack Icon":
                    return AttributeName.AttackIcon;
                case "Cost Icon":
                    return AttributeName.CostIcon;
                case "DEFATTACK":
                    return AttributeName.Defattack;
                case "Number in Deck":
                    return AttributeName.NumberInDeck;
                case "Piercing Icon":
                    return AttributeName.PiercingIcon;
                case "Power":
                    return AttributeName.Power;
                case "Power Overlay":
                    return AttributeName.PowerOverlay;
                case "Recruit Icon":
                    return AttributeName.RecruitIcon;
                case "Second Power":
                    return AttributeName.SecondPower;
                case "Second Power Overlay":
                    return AttributeName.SecondPowerOverlay;
                case "Team":
                    return AttributeName.Team;
                case "VP":
                    return AttributeName.Vp;
                case "Victory":
                    return AttributeName.Victory;
                case "Villain Team":
                    return AttributeName.VillainTeam;
            }
            throw new Exception("Cannot unmarshal type AttributeName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (AttributeName)untypedValue;
            switch (value)
            {
                case AttributeName.Attack:
                    serializer.Serialize(writer, "Attack");
                    return;
                case AttributeName.AttackIcon:
                    serializer.Serialize(writer, "Attack Icon");
                    return;
                case AttributeName.CostIcon:
                    serializer.Serialize(writer, "Cost Icon");
                    return;
                case AttributeName.Defattack:
                    serializer.Serialize(writer, "DEFATTACK");
                    return;
                case AttributeName.NumberInDeck:
                    serializer.Serialize(writer, "Number in Deck");
                    return;
                case AttributeName.PiercingIcon:
                    serializer.Serialize(writer, "Piercing Icon");
                    return;
                case AttributeName.Power:
                    serializer.Serialize(writer, "Power");
                    return;
                case AttributeName.PowerOverlay:
                    serializer.Serialize(writer, "Power Overlay");
                    return;
                case AttributeName.RecruitIcon:
                    serializer.Serialize(writer, "Recruit Icon");
                    return;
                case AttributeName.SecondPower:
                    serializer.Serialize(writer, "Second Power");
                    return;
                case AttributeName.SecondPowerOverlay:
                    serializer.Serialize(writer, "Second Power Overlay");
                    return;
                case AttributeName.Team:
                    serializer.Serialize(writer, "Team");
                    return;
                case AttributeName.Vp:
                    serializer.Serialize(writer, "VP");
                    return;
                case AttributeName.Victory:
                    serializer.Serialize(writer, "Victory");
                    return;
                case AttributeName.VillainTeam:
                    serializer.Serialize(writer, "Villain Team");
                    return;
            }
            throw new Exception("Cannot marshal type AttributeName");
        }

        public static readonly AttributeNameConverter Singleton = new AttributeNameConverter();
    }

    internal class ValueUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(ValueUnion) || t == typeof(ValueUnion?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    switch (stringValue)
                    {
                        case "8+":
                            return new ValueUnion { Enum = ValueValue.The8 };
                        case "ATTACK":
                            return new ValueUnion { Enum = ValueValue.Attack };
                        case "COVERT":
                            return new ValueUnion { Enum = ValueValue.Covert };
                        case "INSTINCT":
                            return new ValueUnion { Enum = ValueValue.Instinct };
                        case "LOM":
                            return new ValueUnion { Enum = ValueValue.Lom };
                        case "NONE":
                            return new ValueUnion { Enum = ValueValue.None };
                        case "PIERCING":
                            return new ValueUnion { Enum = ValueValue.Piercing };
                        case "RANGE":
                            return new ValueUnion { Enum = ValueValue.Range };
                        case "RECRUIT":
                            return new ValueUnion { Enum = ValueValue.Recruit };
                        case "STRENGTH":
                            return new ValueUnion { Enum = ValueValue.Strength };
                        case "TECH":
                            return new ValueUnion { Enum = ValueValue.Tech };
                        case "VICTORY":
                            return new ValueUnion { Enum = ValueValue.Victory };
                    }
                    long l;
                    if (Int64.TryParse(stringValue, out l))
                    {
                        return new ValueUnion { Integer = l };
                    }
                    break;
            }
            throw new Exception("Cannot unmarshal type ValueUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ValueUnion)untypedValue;
            if (value.Enum != null)
            {
                switch (value.Enum)
                {
                    case ValueValue.The8:
                        serializer.Serialize(writer, "8+");
                        return;
                    case ValueValue.Attack:
                        serializer.Serialize(writer, "ATTACK");
                        return;
                    case ValueValue.Covert:
                        serializer.Serialize(writer, "COVERT");
                        return;
                    case ValueValue.Instinct:
                        serializer.Serialize(writer, "INSTINCT");
                        return;
                    case ValueValue.Lom:
                        serializer.Serialize(writer, "LOM");
                        return;
                    case ValueValue.None:
                        serializer.Serialize(writer, "NONE");
                        return;
                    case ValueValue.Piercing:
                        serializer.Serialize(writer, "PIERCING");
                        return;
                    case ValueValue.Range:
                        serializer.Serialize(writer, "RANGE");
                        return;
                    case ValueValue.Recruit:
                        serializer.Serialize(writer, "RECRUIT");
                        return;
                    case ValueValue.Strength:
                        serializer.Serialize(writer, "STRENGTH");
                        return;
                    case ValueValue.Tech:
                        serializer.Serialize(writer, "TECH");
                        return;
                    case ValueValue.Victory:
                        serializer.Serialize(writer, "VICTORY");
                        return;
                }
            }
            if (value.Integer != null)
            {
                serializer.Serialize(writer, value.Integer.Value.ToString());
                return;
            }
            throw new Exception("Cannot marshal type ValueUnion");
        }

        public static readonly ValueUnionConverter Singleton = new ValueUnionConverter();
    }

    internal class ValueValueConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(ValueValue) || t == typeof(ValueValue?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "8+":
                    return ValueValue.The8;
                case "ATTACK":
                    return ValueValue.Attack;
                case "COVERT":
                    return ValueValue.Covert;
                case "INSTINCT":
                    return ValueValue.Instinct;
                case "LOM":
                    return ValueValue.Lom;
                case "NONE":
                    return ValueValue.None;
                case "PIERCING":
                    return ValueValue.Piercing;
                case "RANGE":
                    return ValueValue.Range;
                case "RECRUIT":
                    return ValueValue.Recruit;
                case "STRENGTH":
                    return ValueValue.Strength;
                case "TECH":
                    return ValueValue.Tech;
                case "VICTORY":
                    return ValueValue.Victory;
            }
            throw new Exception("Cannot unmarshal type ValueValue");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ValueValue)untypedValue;
            switch (value)
            {
                case ValueValue.The8:
                    serializer.Serialize(writer, "8+");
                    return;
                case ValueValue.Attack:
                    serializer.Serialize(writer, "ATTACK");
                    return;
                case ValueValue.Covert:
                    serializer.Serialize(writer, "COVERT");
                    return;
                case ValueValue.Instinct:
                    serializer.Serialize(writer, "INSTINCT");
                    return;
                case ValueValue.Lom:
                    serializer.Serialize(writer, "LOM");
                    return;
                case ValueValue.None:
                    serializer.Serialize(writer, "NONE");
                    return;
                case ValueValue.Piercing:
                    serializer.Serialize(writer, "PIERCING");
                    return;
                case ValueValue.Range:
                    serializer.Serialize(writer, "RANGE");
                    return;
                case ValueValue.Recruit:
                    serializer.Serialize(writer, "RECRUIT");
                    return;
                case ValueValue.Strength:
                    serializer.Serialize(writer, "STRENGTH");
                    return;
                case ValueValue.Tech:
                    serializer.Serialize(writer, "TECH");
                    return;
                case ValueValue.Victory:
                    serializer.Serialize(writer, "VICTORY");
                    return;
            }
            throw new Exception("Cannot marshal type ValueValue");
        }

        public static readonly ValueValueConverter Singleton = new ValueValueConverter();
    }

    internal class CardsUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(CardsUnion) || t == typeof(CardsUnion?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<CardsClass>(reader);
                    return new CardsUnion { CardsClass = objectValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<List<CardElement>>(reader);
                    return new CardsUnion { CardElementArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type CardsUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (CardsUnion)untypedValue;
            if (value.CardElementArray != null)
            {
                serializer.Serialize(writer, value.CardElementArray);
                return;
            }
            if (value.CardsClass != null)
            {
                serializer.Serialize(writer, value.CardsClass);
                return;
            }
            throw new Exception("Cannot marshal type CardsUnion");
        }

        public static readonly CardsUnionConverter Singleton = new CardsUnionConverter();
    }

    internal class StyleEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(StyleEnum) || t == typeof(StyleEnum?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Base Set":
                    return StyleEnum.BaseSet;
                case "Villains":
                    return StyleEnum.Villains;
            }
            throw new Exception("Cannot unmarshal type StyleEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (StyleEnum)untypedValue;
            switch (value)
            {
                case StyleEnum.BaseSet:
                    serializer.Serialize(writer, "Base Set");
                    return;
                case StyleEnum.Villains:
                    serializer.Serialize(writer, "Villains");
                    return;
            }
            throw new Exception("Cannot marshal type StyleEnum");
        }

        public static readonly StyleEnumConverter Singleton = new StyleEnumConverter();
    }

    internal class TentacledTemplateConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(TentacledTemplate) || t == typeof(TentacledTemplate?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new TentacledTemplate { String = stringValue };
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<PurpleTemplate>(reader);
                    return new TentacledTemplate { PurpleTemplate = objectValue };
            }
            throw new Exception("Cannot unmarshal type TentacledTemplate");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (TentacledTemplate)untypedValue;
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.PurpleTemplate != null)
            {
                serializer.Serialize(writer, value.PurpleTemplate);
                return;
            }
            throw new Exception("Cannot marshal type TentacledTemplate");
        }

        public static readonly TentacledTemplateConverter Singleton = new TentacledTemplateConverter();
    }

    internal class PurpleParseStringConverter : JsonConverter
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

        public static readonly PurpleParseStringConverter Singleton = new PurpleParseStringConverter();
    }

    internal class BgimageNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(BgimageName) || t == typeof(BgimageName?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Image":
                    return BgimageName.Image;
                case "back_cost":
                    return BgimageName.BackCost;
                case "back_text":
                    return BgimageName.BackText;
                case "text_overlay":
                    return BgimageName.TextOverlay;
            }
            throw new Exception("Cannot unmarshal type BgimageName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (BgimageName)untypedValue;
            switch (value)
            {
                case BgimageName.Image:
                    serializer.Serialize(writer, "Image");
                    return;
                case BgimageName.BackCost:
                    serializer.Serialize(writer, "back_cost");
                    return;
                case BgimageName.BackText:
                    serializer.Serialize(writer, "back_text");
                    return;
                case BgimageName.TextOverlay:
                    serializer.Serialize(writer, "text_overlay");
                    return;
            }
            throw new Exception("Cannot marshal type BgimageName");
        }

        public static readonly BgimageNameConverter Singleton = new BgimageNameConverter();
    }

    internal class CardnameNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(CardnameName) || t == typeof(CardnameName?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Card Name")
            {
                return CardnameName.CardName;
            }
            throw new Exception("Cannot unmarshal type CardnameName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (CardnameName)untypedValue;
            if (value == CardnameName.CardName)
            {
                serializer.Serialize(writer, "Card Name");
                return;
            }
            throw new Exception("Cannot marshal type CardnameName");
        }

        public static readonly CardnameNameConverter Singleton = new CardnameNameConverter();
    }

    internal class PurpleSubnameValueConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(PurpleSubnameValue) || t == typeof(PurpleSubnameValue?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Henchman Villain":
                    return PurpleSubnameValue.HenchmanVillain;
                case "Mastermind":
                    return PurpleSubnameValue.Mastermind;
                case "Mastermind Tactic - %DECKNAME%":
                    return PurpleSubnameValue.MastermindTacticDeckname;
            }
            throw new Exception("Cannot unmarshal type PurpleSubnameValue");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PurpleSubnameValue)untypedValue;
            switch (value)
            {
                case PurpleSubnameValue.HenchmanVillain:
                    serializer.Serialize(writer, "Henchman Villain");
                    return;
                case PurpleSubnameValue.Mastermind:
                    serializer.Serialize(writer, "Mastermind");
                    return;
                case PurpleSubnameValue.MastermindTacticDeckname:
                    serializer.Serialize(writer, "Mastermind Tactic - %DECKNAME%");
                    return;
            }
            throw new Exception("Cannot marshal type PurpleSubnameValue");
        }

        public static readonly PurpleSubnameValueConverter Singleton = new PurpleSubnameValueConverter();
    }

    internal class ElementgroupNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(ElementgroupName) || t == typeof(ElementgroupName?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Attack":
                    return ElementgroupName.Attack;
                case "Cost":
                    return ElementgroupName.Cost;
                case "Piercing":
                    return ElementgroupName.Piercing;
                case "Recruit":
                    return ElementgroupName.Recruit;
            }
            throw new Exception("Cannot unmarshal type ElementgroupName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ElementgroupName)untypedValue;
            switch (value)
            {
                case ElementgroupName.Attack:
                    serializer.Serialize(writer, "Attack");
                    return;
                case ElementgroupName.Cost:
                    serializer.Serialize(writer, "Cost");
                    return;
                case ElementgroupName.Piercing:
                    serializer.Serialize(writer, "Piercing");
                    return;
                case ElementgroupName.Recruit:
                    serializer.Serialize(writer, "Recruit");
                    return;
            }
            throw new Exception("Cannot marshal type ElementgroupName");
        }

        public static readonly ElementgroupNameConverter Singleton = new ElementgroupNameConverter();
    }

    internal class TextareaNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(TextareaName) || t == typeof(TextareaName?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Attack Value":
                    return TextareaName.AttackValue;
                case "Card Text":
                    return TextareaName.CardText;
                case "Cost":
                    return TextareaName.Cost;
                case "Cost Text":
                    return TextareaName.CostText;
                case "Piercing Value":
                    return TextareaName.PiercingValue;
                case "Recruit Value":
                    return TextareaName.RecruitValue;
                case "Victory Value":
                    return TextareaName.VictoryValue;
            }
            throw new Exception("Cannot unmarshal type TextareaName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TextareaName)untypedValue;
            switch (value)
            {
                case TextareaName.AttackValue:
                    serializer.Serialize(writer, "Attack Value");
                    return;
                case TextareaName.CardText:
                    serializer.Serialize(writer, "Card Text");
                    return;
                case TextareaName.Cost:
                    serializer.Serialize(writer, "Cost");
                    return;
                case TextareaName.CostText:
                    serializer.Serialize(writer, "Cost Text");
                    return;
                case TextareaName.PiercingValue:
                    serializer.Serialize(writer, "Piercing Value");
                    return;
                case TextareaName.RecruitValue:
                    serializer.Serialize(writer, "Recruit Value");
                    return;
                case TextareaName.VictoryValue:
                    serializer.Serialize(writer, "Victory Value");
                    return;
            }
            throw new Exception("Cannot marshal type TextareaName");
        }

        public static readonly TextareaNameConverter Singleton = new TextareaNameConverter();
    }

    internal class FluffyParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(bool) || t == typeof(bool?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            bool b;
            if (Boolean.TryParse(value, out b))
            {
                return b;
            }
            throw new Exception("Cannot unmarshal type bool");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (bool)untypedValue;
            var boolString = value ? "true" : "false";
            serializer.Serialize(writer, boolString);
            return;
        }

        public static readonly FluffyParseStringConverter Singleton = new FluffyParseStringConverter();
    }

    internal class ImageUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(ImageUnion) || t == typeof(ImageUnion?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<BgimageElement>(reader);
                    return new ImageUnion { BgimageElement = objectValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<List<BgimageElement>>(reader);
                    return new ImageUnion { BgimageElementArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type ImageUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (ImageUnion)untypedValue;
            if (value.BgimageElementArray != null)
            {
                serializer.Serialize(writer, value.BgimageElementArray);
                return;
            }
            if (value.BgimageElement != null)
            {
                serializer.Serialize(writer, value.BgimageElement);
                return;
            }
            throw new Exception("Cannot marshal type ImageUnion");
        }

        public static readonly ImageUnionConverter Singleton = new ImageUnionConverter();
    }

    internal class ScrollingtextareaNameConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(ScrollingtextareaName) || t == typeof(ScrollingtextareaName?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Flavor Text")
            {
                return ScrollingtextareaName.FlavorText;
            }
            throw new Exception("Cannot unmarshal type ScrollingtextareaName");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ScrollingtextareaName)untypedValue;
            if (value == ScrollingtextareaName.FlavorText)
            {
                serializer.Serialize(writer, "Flavor Text");
                return;
            }
            throw new Exception("Cannot marshal type ScrollingtextareaName");
        }

        public static readonly ScrollingtextareaNameConverter Singleton = new ScrollingtextareaNameConverter();
    }

    internal class ScrollingtextareaValueConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(ScrollingtextareaValue) || t == typeof(ScrollingtextareaValue?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "":
                    return ScrollingtextareaValue.Empty;
                case "&lt;g&gt;  &lt;g&gt; Yes I Am.":
                    return ScrollingtextareaValue.LtGGtLtGGtYesIAm;
            }
            throw new Exception("Cannot unmarshal type ScrollingtextareaValue");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ScrollingtextareaValue)untypedValue;
            switch (value)
            {
                case ScrollingtextareaValue.Empty:
                    serializer.Serialize(writer, "");
                    return;
                case ScrollingtextareaValue.LtGGtLtGGtYesIAm:
                    serializer.Serialize(writer, "&lt;g&gt;  &lt;g&gt; Yes I Am.");
                    return;
            }
            throw new Exception("Cannot marshal type ScrollingtextareaValue");
        }

        public static readonly ScrollingtextareaValueConverter Singleton = new ScrollingtextareaValueConverter();
    }

    internal class FluffySubnameValueConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(FluffySubnameValue) || t == typeof(FluffySubnameValue?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "%DECKNAME%")
            {
                return FluffySubnameValue.Deckname;
            }
            throw new Exception("Cannot unmarshal type FluffySubnameValue");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (FluffySubnameValue)untypedValue;
            if (value == FluffySubnameValue.Deckname)
            {
                serializer.Serialize(writer, "%DECKNAME%");
                return;
            }
            throw new Exception("Cannot marshal type FluffySubnameValue");
        }

        public static readonly FluffySubnameValueConverter Singleton = new FluffySubnameValueConverter();
    }

    internal class TextUnionConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(TextUnion) || t == typeof(TextUnion?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<TextareaElement>(reader);
                    return new TextUnion { TextareaElement = objectValue };
                case JsonToken.StartArray:
                    var arrayValue = serializer.Deserialize<List<TextareaElement>>(reader);
                    return new TextUnion { TextareaElementArray = arrayValue };
            }
            throw new Exception("Cannot unmarshal type TextUnion");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (TextUnion)untypedValue;
            if (value.TextareaElementArray != null)
            {
                serializer.Serialize(writer, value.TextareaElementArray);
                return;
            }
            if (value.TextareaElement != null)
            {
                serializer.Serialize(writer, value.TextareaElement);
                return;
            }
            throw new Exception("Cannot marshal type TextUnion");
        }

        public static readonly TextUnionConverter Singleton = new TextUnionConverter();
    }

    internal class StickyTemplateConverter : JsonConverter
    {
        public override bool CanConvert(Type t)
        {
            return t == typeof(StickyTemplate) || t == typeof(StickyTemplate?);
        }

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new StickyTemplate { String = stringValue };
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<FluffyTemplate>(reader);
                    return new StickyTemplate { FluffyTemplate = objectValue };
            }
            throw new Exception("Cannot unmarshal type StickyTemplate");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (StickyTemplate)untypedValue;
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.FluffyTemplate != null)
            {
                serializer.Serialize(writer, value.FluffyTemplate);
                return;
            }
            throw new Exception("Cannot marshal type StickyTemplate");
        }

        public static readonly StickyTemplateConverter Singleton = new StickyTemplateConverter();
    }
}
