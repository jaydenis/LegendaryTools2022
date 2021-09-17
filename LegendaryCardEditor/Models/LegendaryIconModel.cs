
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryCardEditor.Models
{
    public partial class LegendaryIconViewModel
    {
        [JsonProperty("iconid")]
        public int IconId { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("minimizedUnderlay")]
        public bool MinimizedUnderlay { get; set; }
    }
}
