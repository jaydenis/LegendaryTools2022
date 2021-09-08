﻿using LegendaryTools2022.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTools2022.Models
{
    public partial class CardTemplates
    {
        [JsonProperty("templates")]
        public List<TemplateModel> Templates { get; set; }
    }

    public partial class TemplateModel
    {
        [JsonProperty("card_template")]
        public CardTemplate CardTemplate { get; set; }

        [JsonProperty("form_controls")]
        public FormControls FormControls { get; set; }
    }

    public partial class CardTemplate
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("displayname", Required = Required.Always)]
        public string Displayname { get; set; }

        [JsonProperty("deck", Required = Required.Always)]
        public string Deck { get; set; }

        [JsonProperty("card_type_template, Required = Required.Always")]
        [JsonConverter(typeof(CustomStringEnumConverter))]
        public CardTypeTemplateEnum CardTypeTemplate { get; set; }

        [JsonProperty("rectxarray", Required = Required.Always)]
        public string Rectxarray { get; set; }

        [JsonProperty("rectyarray", Required = Required.Always)]
        public string Rectyarray { get; set; }

        [JsonProperty("cardwidth", Required = Required.Always)]
        public int Cardwidth { get; set; }

        [JsonProperty("cardheight", Required = Required.Always)]
        public int Cardheight { get; set; }

        [JsonProperty("frame_image", Required = Required.Always)]
        public string FrameImage { get; set; }

        [JsonProperty("back_cost", NullValueHandling = NullValueHandling.Ignore)]
        public string BackCost { get; set; }

        [JsonProperty("back_text", NullValueHandling = NullValueHandling.Ignore) ]
        public string BackText { get; set; }

        [JsonProperty("back_underlay", NullValueHandling = NullValueHandling.Ignore)]
        public string BackUnderlay { get; set; }

    }

    public partial class FormControls
    {
        [JsonProperty("show_team")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool ShowTeam { get; set; }

        [JsonProperty("show_power")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool ShowPower { get; set; }

        [JsonProperty("show_power2")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool ShowPower2 { get; set; }

        [JsonProperty("show_attributes")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool ShowAttributes { get; set; }

        [JsonProperty("show_attributes_cost")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool ShowAttributesCost { get; set; }

        [JsonProperty("show_attributes_recruit")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool ShowAttributesRecruit { get; set; }

        [JsonProperty("show_attributes_attack")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool ShowAttributesAttack { get; set; }

        [JsonProperty("show_attributes_piercing")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool ShowAttributesPiercing { get; set; }

        [JsonProperty("show_victory_points")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool ShowVictoryPoints { get; set; }

        //"show_attack_cost": "false"
        [JsonProperty("show_attack_cost")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool ShowAttackCost { get; set; }
    }
    public partial class LegendaryTemplate
    {
        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("displayname")]
        public string displayname { get; set; }

        [JsonProperty("deck")]
        public string deck { get; set; }

        [JsonProperty("rectxarray")]
        public string rectxarray { get; set; }

        [JsonProperty("rectyarray")]
        public string rectyarray { get; set; }

        [JsonProperty("cardwidth")]
        public string cardwidth { get; set; }

        [JsonProperty("cardheight")]
        public string cardheight { get; set; }
    }


  
}
