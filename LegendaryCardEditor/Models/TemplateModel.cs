namespace LegendarySetEditor.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel;

    public partial class TemplateModel
    {
        [JsonProperty("template", Required = Required.Always)]
        public Template Template { get; set; }

        [JsonProperty("cardsize", Required = Required.Always)]
        public Cardsize Cardsize { get; set; }

        [JsonProperty("bgimage", Required = Required.Always)]
        public Image Bgimage { get; set; }

        [JsonProperty("iconbg", Required = Required.Always)]
        public List<Iconbg> Iconbg { get; set; }

        [JsonProperty("image", Required = Required.Always)]
        public Image Image { get; set; }

        [JsonProperty("styles", Required = Required.Always)]
        public List<Style> Styles { get; set; }

        [JsonProperty("elementgroup", Required = Required.Always)]
        public List<Elementgroup> Elementgroup { get; set; }

        [JsonProperty("textarea", Required = Required.Always)]
        public Textarea Textarea { get; set; }

        [JsonProperty("property", Required = Required.Always)]
        public Property Property { get; set; }
    }

    public partial class Image
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("fullsize", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Fullsize { get; set; }

        [JsonProperty("x", Required = Required.Always)]
        public int X { get; set; }

        [JsonProperty("y", Required = Required.Always)]
        public int Y { get; set; }

        [JsonProperty("maxwidth", Required = Required.Always)]
        public int Maxwidth { get; set; }

        [JsonProperty("maxheight", Required = Required.Always)]
        public int Maxheight { get; set; }

        [JsonProperty("allowchange", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Allowchange { get; set; }

        [JsonProperty("zoomable", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Zoomable { get; set; }

        [JsonProperty("templatefile", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool? Templatefile { get; set; }

        [JsonProperty("path", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Path { get; set; }
    }

    public partial class Cardsize
    {
        [JsonProperty("cardwidth", Required = Required.Always)]
        public int Cardwidth { get; set; }

        [JsonProperty("cardheight", Required = Required.Always)]
        public int Cardheight { get; set; }

        [JsonProperty("dpi", Required = Required.Always)]
        public int Dpi { get; set; }
    }

    public partial class Elementgroup
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("icon", Required = Required.Always)]
        public Icon Icon { get; set; }

        [JsonProperty("text", Required = Required.Always)]
        public Text Text { get; set; }

        [JsonProperty("visible", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool? Visible { get; set; }
    }

    public partial class Icon
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("defaultvalue", Required = Required.Always)]
        public string Defaultvalue { get; set; }

        [JsonProperty("allowchange", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Allowchange { get; set; }

        [JsonProperty("optional", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool? Optional { get; set; }

        [JsonProperty("x", Required = Required.Always)]
        public int X { get; set; }

        [JsonProperty("y", Required = Required.Always)]
        public int Y { get; set; }

        [JsonProperty("maxwidth", Required = Required.Always)]
        public int Maxwidth { get; set; }

        [JsonProperty("maxheight", Required = Required.Always)]
        public int Maxheight { get; set; }

        [JsonProperty("drawunderlay", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Drawunderlay { get; set; }

        [JsonProperty("blurradius", Required = Required.Always)]
        public int Blurradius { get; set; }

        [JsonProperty("bluedouble", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Bluedouble { get; set; }

        [JsonProperty("blurexpand", Required = Required.Always)]
        public int Blurexpand { get; set; }

        [JsonProperty("blurcolour", Required = Required.Always)]
        public string Blurcolour { get; set; }

        [JsonProperty("visible", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool? Visible { get; set; }

        [JsonProperty("icontype", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Icontype { get; set; }

        [JsonProperty("valuefrom", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Valuefrom { get; set; }
    }

    public partial class Text
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("alignment", Required = Required.Always)]
        public string Alignment { get; set; }

        [JsonProperty("allowchange", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Allowchange { get; set; }

        [JsonProperty("defaultvalue", Required = Required.Always)]
        public int Defaultvalue { get; set; }

        [JsonProperty("x", Required = Required.Always)]
        public int X { get; set; }

        [JsonProperty("y", Required = Required.Always)]
        public int Y { get; set; }

        [JsonProperty("colour", Required = Required.Always)]
        public string Colour { get; set; }

        [JsonProperty("textsize", Required = Required.Always)]
        public int Textsize { get; set; }

        [JsonProperty("uppercase", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Uppercase { get; set; }

        [JsonProperty("drawunderlay", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Drawunderlay { get; set; }

        [JsonProperty("blurradius", Required = Required.Always)]
        public int Blurradius { get; set; }

        [JsonProperty("bluedouble", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Bluedouble { get; set; }

        [JsonProperty("blurexpand", Required = Required.Always)]
        public int Blurexpand { get; set; }

        [JsonProperty("blurcolour", Required = Required.Always)]
        public string Blurcolour { get; set; }

        [JsonProperty("linkedelement", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Linkedelement { get; set; }

        [JsonProperty("visible", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool? Visible { get; set; }
    }

    public partial class Iconbg
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("defaultvalue", Required = Required.Always)]
        public string Defaultvalue { get; set; }

        [JsonProperty("allowchange", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Allowchange { get; set; }

        [JsonProperty("icontype", Required = Required.Always)]
        public string Icontype { get; set; }

        [JsonProperty("x", Required = Required.Always)]
        public int X { get; set; }

        [JsonProperty("y", Required = Required.Always)]
        public int Y { get; set; }

        [JsonProperty("maxwidth", Required = Required.Always)]
        public int Maxwidth { get; set; }

        [JsonProperty("maxheight", Required = Required.Always)]
        public int Maxheight { get; set; }

        [JsonProperty("drawunderlay", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Drawunderlay { get; set; }

        [JsonProperty("blurradius", Required = Required.Always)]
        public int Blurradius { get; set; }

        [JsonProperty("bluedouble", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Bluedouble { get; set; }

        [JsonProperty("blurexpand", Required = Required.Always)]
        public int Blurexpand { get; set; }

        [JsonProperty("blurcolour", Required = Required.Always)]
        public string Blurcolour { get; set; }

        [JsonProperty("imagex", Required = Required.Always)]
        public int Imagex { get; set; }

        [JsonProperty("imagey", Required = Required.Always)]
        public int Imagey { get; set; }

        [JsonProperty("imagemaxwidth", Required = Required.Always)]
        public int Imagemaxwidth { get; set; }

        [JsonProperty("imagemaxheight", Required = Required.Always)]
        public int Imagemaxheight { get; set; }

        [JsonProperty("imageprefix", Required = Required.Always)]
        public string Imageprefix { get; set; }

        [JsonProperty("imageextension", Required = Required.Always)]
        public string Imageextension { get; set; }

        [JsonProperty("imagefilter", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Imagefilter { get; set; }
    }

    public partial class Property
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("property", Required = Required.Always)]
        public string PropertyProperty { get; set; }

        [JsonProperty("defaultvalue", Required = Required.Always)]
        public int Defaultvalue { get; set; }
    }

    public partial class Style
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("cardname", Required = Required.Always)]
        public Cardname Cardname { get; set; }

        [JsonProperty("icon", Required = Required.Always)]
        public List<Icon> Icon { get; set; }
    }

    public partial class Cardname
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("defaultvalue", Required = Required.Always)]
        public string Defaultvalue { get; set; }

        [JsonProperty("allowchange", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Allowchange { get; set; }

        [JsonProperty("includesubname", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Includesubname { get; set; }

        [JsonProperty("highlight", Required = Required.Always)]
        public string Highlight { get; set; }

        [JsonProperty("highlightcolour", Required = Required.Always)]
        public string Highlightcolour { get; set; }

        [JsonProperty("uppercase", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Uppercase { get; set; }

        [JsonProperty("alignment", Required = Required.Always)]
        public string Alignment { get; set; }

        [JsonProperty("x", Required = Required.Always)]
        public int X { get; set; }

        [JsonProperty("y", Required = Required.Always)]
        public int Y { get; set; }

        [JsonProperty("colour", Required = Required.Always)]
        public string Colour { get; set; }

        [JsonProperty("textsize", Required = Required.Always)]
        public int Textsize { get; set; }

        [JsonProperty("subnametext", Required = Required.Always)]
        public string Subnametext { get; set; }

        [JsonProperty("subnamesize", Required = Required.Always)]
        public int Subnamesize { get; set; }

        [JsonProperty("subnamegap", Required = Required.Always)]
        public int Subnamegap { get; set; }

        [JsonProperty("blurradius", Required = Required.Always)]
        public int Blurradius { get; set; }

        [JsonProperty("bluedouble", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Bluedouble { get; set; }

        [JsonProperty("blurexpand", Required = Required.Always)]
        public int Blurexpand { get; set; }

        [JsonProperty("subnameeditable", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool? Subnameeditable { get; set; }
    }

    public partial class Template
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("displayname", Required = Required.Always)]
        public string Displayname { get; set; }

        [JsonProperty("deck", Required = Required.Always)]
        public string Deck { get; set; }

        [JsonProperty("defaultsindeck", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Defaultsindeck { get; set; }

        [JsonProperty("defaultcopies", Required = Required.Always)]
        public int Defaultcopies { get; set; }
    }

    public partial class Textarea
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("alignmenthorizontal", Required = Required.Always)]
        public string Alignmenthorizontal { get; set; }

        [JsonProperty("alignmentvertical", Required = Required.Always)]
        public string Alignmentvertical { get; set; }

        [JsonProperty("allowchange", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Allowchange { get; set; }

        [JsonProperty("defaultvalue", Required = Required.Always)]
        public string Defaultvalue { get; set; }

        [JsonProperty("colour", Required = Required.Always)]
        public string Colour { get; set; }

        [JsonProperty("textsize", Required = Required.Always)]
        public int Textsize { get; set; }

        [JsonProperty("rectxarray", Required = Required.Always)]
        public string Rectxarray { get; set; }

        [JsonProperty("rectyarray", Required = Required.Always)]
        public string Rectyarray { get; set; }

        [JsonProperty("debug", Required = Required.Always)]
        [JsonConverter(typeof(BooleanConverter))]
        public bool Debug { get; set; }
    }
}