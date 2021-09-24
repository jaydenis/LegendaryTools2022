using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTemplateEditor
{
    public class TemplateModel
    {
        public Point TeamIconXY { get; set; }
        public bool TeamIconVisible { get; set; }

        public Point PowerPrimaryIconXY { get; set; }
        public bool PowerPrimaryIconVisible { get; set; }

        public Point PowerSecondaryIconXY { get; set; }
        public bool PowerSecondaryVisible { get; set; }

        public Point CardNameXY { get; set; }
        public int CardNameTextSize { get; set; }
        public bool CardNameVisible { get; set; }

        public Point CardNameSubXY { get; set; }
        public int CardNameSubTextSize { get; set; }
        public bool CardNameSubVisible { get; set; }


        public int AttributesPrimaryTextSize { get; set; }
        public Point RecruitIconXY { get; set; }
        public Point RecruitValueXY { get; set; }
        public bool RecruitVisible { get; set; }

        public Point AttackIconXY { get; set; }
        public Point AttackValueXY { get; set; }
        public bool AttackVisible { get; set; }

        public Point PiercingIconXY { get; set; }
        public Point PiercingValueXY { get; set; }
        public bool PiercingVisible { get; set; }

        public int AttributesSecondryTextSize { get; set; }
        public Point CostIconXY { get; set; }
        public Point CostValueXY { get; set; }
        public bool CostVisible { get; set; }

        public Point AttackDefenseIconXY { get; set; }
        public Point AttackDefenseValueXY { get; set; }
        public bool AttackDefenseVisible { get; set; }

        public Point VictroyIconXY { get; set; }
        public Point VictroyValueXY { get; set; }
        public bool VictroyVisible { get; set; }
        public int VictoryTextSize { get; set; }

        public Point[] CardTextRectAreaXY { get; set; }
        public Point[] CardTextRectAreaY { get; set; }

        public int CardTextSise { get; set; }

    }

    public class TemplateEntity
    {
        [JsonProperty("templateId", Required = Required.Always)]
        public int TemplateId { get; set; }

        [JsonProperty("templateName", Required = Required.Always)]
        public string TemplateName { get; set; }

        [JsonProperty("templateDisplayName", Required = Required.Always)]
        public string TemplateDisplayName { get; set; }

        [JsonProperty("frameImage", Required = Required.Always)]
        public string FrameImage { get; set; }

        [JsonProperty("costImage", Required = Required.Always)]
        public string CostImage { get; set; }

        [JsonProperty("textImage", Required = Required.Always)]
        public string TextImage { get; set; }

        [JsonProperty("underlayImage", Required = Required.Always)]
        public string UnderlayImage { get; set; }

        [JsonProperty("imageHeight", Required = Required.Always)]
        public int ImageHeight { get; set; } = 1050;

        [JsonProperty("imageWidth", Required = Required.Always)]
        public int ImageWidth { get; set; } = 696;

        [JsonProperty("teamIconXY", Required = Required.Always)]
        public List<int> TeamIconXY { get; set; }

        [JsonProperty("teamIconVisible", Required = Required.Always)]
        public bool TeamIconVisible { get; set; }

        [JsonProperty("powerPrimaryIconXY", Required = Required.Always)]
        public List<int> PowerPrimaryIconXY { get; set; }

        [JsonProperty("powerPrimaryIconVisible", Required = Required.Always)]
        public bool PowerPrimaryIconVisible { get; set; }

        [JsonProperty("powerSecondaryIconXY", Required = Required.Always)]
        public List<int> PowerSecondaryIconXY { get; set; }

        [JsonProperty("powerSecondaryIconVisible", Required = Required.Always)]
        public bool PowerSecondaryIconVisible { get; set; }

        [JsonProperty("cardNameXY", Required = Required.Always)]
        public List<int> CardNameXY { get; set; }

        [JsonProperty("cardNameTextSize", Required = Required.Always)]
        public int CardNameTextSize { get; set; }

        [JsonProperty("cardNameVisible", Required = Required.Always)]
        public bool CardNameVisible { get; set; }

        [JsonProperty("cardNameSubXY", Required = Required.Always)]
        public List<int> CardNameSubXY { get; set; }

        [JsonProperty("cardNameSubTextSize", Required = Required.Always)]
        public int CardNameSubTextSize { get; set; }

        [JsonProperty("cardNameSubVisible", Required = Required.Always)]
        public bool CardNameSubVisible { get; set; }

        //AttributesPrimaryVisible
        [JsonProperty("attributesPrimaryVisible", Required = Required.Always)]
        public bool AttributesPrimaryVisible { get; set; }

        [JsonProperty("attributesPrimaryTextSize", Required = Required.Always)]
        public int AttributesPrimaryTextSize { get; set; }

        [JsonProperty("recruitIconXY", Required = Required.Always)]
        public List<int> RecruitIconXY { get; set; }

        [JsonProperty("recruitValueXY", Required = Required.Always)]
        public List<int> RecruitValueXY { get; set; }

        [JsonProperty("recruitVisible", Required = Required.Always)]
        public bool RecruitVisible { get; set; }

        [JsonProperty("attackIconXY", Required = Required.Always)]
        public List<int> AttackIconXY { get; set; }

        [JsonProperty("attackValueXY", Required = Required.Always)]
        public List<int> AttackValueXY { get; set; }

        [JsonProperty("attackVisible", Required = Required.Always)]
        public bool AttackVisible { get; set; }

        [JsonProperty("piercingIconXY", Required = Required.Always)]
        public List<int> PiercingIconXY { get; set; }

        [JsonProperty("piercingValueXY", Required = Required.Always)]
        public List<int> PiercingValueXY { get; set; }

        [JsonProperty("piercingVisible", Required = Required.Always)]
        public bool PiercingVisible { get; set; }

        [JsonProperty("attributesSecondryTextSize", Required = Required.Always)]
        public int AttributesSecondryTextSize { get; set; }

        [JsonProperty("costIconXY", Required = Required.Always)]
        public List<int> CostIconXY { get; set; }

        [JsonProperty("costValueXY", Required = Required.Always)]
        public List<int> CostValueXY { get; set; }

        [JsonProperty("costVisible", Required = Required.Always)]
        public bool CostVisible { get; set; }

        [JsonProperty("attackDefenseIconXY", Required = Required.Always)]
        public List<int> AttackDefenseIconXY { get; set; }

        [JsonProperty("attackDefenseValueXY", Required = Required.Always)]
        public List<int> AttackDefenseValueXY { get; set; }

        [JsonProperty("attackDefenseVisible", Required = Required.Always)]
        public bool AttackDefenseVisible { get; set; }

        [JsonProperty("victroyIconXY", Required = Required.Always)]
        public List<int> VictroyIconXY { get; set; }

        [JsonProperty("victroyValueXY", Required = Required.Always)]
        public List<int> VictroyValueXY { get; set; }

        [JsonProperty("victroyVisible", Required = Required.Always)]
        public bool VictroyVisible { get; set; }

        [JsonProperty("victoryTextSize", Required = Required.Always)]
        public int VictoryTextSize { get; set; }

        [JsonProperty("cardTextRectAreaX", Required = Required.Always)]
        public List<int> CardTextRectAreaX { get; set; }

        [JsonProperty("cardTextRectAreaY", Required = Required.Always)]
        public List<int> CardTextRectAreaY { get; set; }

        [JsonProperty("cardTextSize", Required = Required.Always)]
        public int CardTextSize { get; set; }
    }
}
