using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryCardEditor.Models
{
    public partial class BaseTemplate
    {
        [JsonProperty("imageHeight", Required = Required.Always)]
        public int ImageHeight { get; set; }

        [JsonProperty("imageWidth", Required = Required.Always)]
        public int ImageWidth { get; set; }

        [JsonProperty("teamIconXY", Required = Required.Always)]
        public List<int> TeamIconXY { get; set; }

        [JsonProperty("powerPrimaryIconXY", Required = Required.Always)]
        public List<int> PowerPrimaryIconXY { get; set; }

        [JsonProperty("powerSecondaryIconXY", Required = Required.Always)]
        public List<int> PowerSecondaryIconXY { get; set; }

        [JsonProperty("attributesPrimaryTextSize", Required = Required.Always)]
        public int AttributesPrimaryTextSize { get; set; }

        [JsonProperty("recruitIconXY", Required = Required.Always)]
        public List<int> RecruitIconXY { get; set; }

        [JsonProperty("recruitValueXY", Required = Required.Always)]
        public List<int> RecruitValueXY { get; set; }

        [JsonProperty("attackIconXY", Required = Required.Always)]
        public List<int> AttackIconXY { get; set; }

        [JsonProperty("attackValueXY", Required = Required.Always)]
        public List<int> AttackValueXY { get; set; }

        [JsonProperty("piercingIconXY", Required = Required.Always)]
        public List<int> PiercingIconXY { get; set; }

        [JsonProperty("piercingValueXY", Required = Required.Always)]
        public List<int> PiercingValueXY { get; set; }

        [JsonProperty("attributesSecondryTextSize", Required = Required.Always)]
        public int AttributesSecondryTextSize { get; set; }

        [JsonProperty("costIconXY", Required = Required.Always)]
        public List<int> CostIconXY { get; set; }

        [JsonProperty("costValueXY", Required = Required.Always)]
        public List<int> CostValueXY { get; set; }

        [JsonProperty("attackDefenseIconXY", Required = Required.Always)]
        public List<int> AttackDefenseIconXY { get; set; }

        [JsonProperty("attackDefenseValueXY", Required = Required.Always)]
        public List<int> AttackDefenseValueXY { get; set; }

        [JsonProperty("attackDefenseTextSize", Required = Required.Always)]
        public int AttackDefenseTextSize { get; set; }

        [JsonProperty("victroyIconXY", Required = Required.Always)]
        public List<int> VictroyIconXY { get; set; }

        [JsonProperty("victroyValueXY", Required = Required.Always)]
        public List<int> VictroyValueXY { get; set; }

        [JsonProperty("victoryTextSize", Required = Required.Always)]
        public int VictoryTextSize { get; set; }

        [JsonProperty("cardTextSize", Required = Required.Always)]
        public int CardTextSize { get; set; }
    }
}
