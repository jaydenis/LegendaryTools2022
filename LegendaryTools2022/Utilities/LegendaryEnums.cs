using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTools2022.Utilities
{
    [JsonConverter(typeof(CustomStringEnumConverter))]
    public enum CardTypeTemplateEnum
    {
        hero_common_none,
        hero_common,
        hero_uncommon,
        hero_rare,
        //hero_common_covert,
        //hero_common_instinct,
        //hero_common_none,
        //hero_common_range,
        //hero_common_strength,
        //hero_common_tech,
        //hero_uncommon_covert,
        //hero_uncommon_instinct,
        //hero_uncommon_none,
        //hero_uncommon_range,
        //hero_uncommon_strength,
        //hero_uncommon_tech,
        //hero_rare,
        //bystander,
        //bystander_villain,
        //bystander_wound,
        //mastermind,
        //mastermind_tactic,
        //mastermind_token,
        //mastermind_transformed,
        //sidekick_common_covert,
        //sidekick_common_instinct,
        //sidekick_common_none,
        //sidekick_common_range,
        //sidekick_common_strength,
        //sidekick_common_tech,
        //villain,
        //villain_location,
        //villain_token,
        //villain_transformed,
        //henchmen,
        //henchmen_location,
        //wound

    };


    public class CustomStringEnumConverter : Newtonsoft.Json.Converters.StringEnumConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Type type = value.GetType() as Type;

            if (!type.IsEnum) throw new InvalidOperationException("Only type Enum is supported");
            foreach (var field in type.GetFields())
            {
                if (field.Name == value.ToString())
                {
                    var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    writer.WriteValue(attribute != null ? attribute.Description : field.Name);

                    return;
                }
            }

            throw new ArgumentException("Enum not found");
        }
    }

}
