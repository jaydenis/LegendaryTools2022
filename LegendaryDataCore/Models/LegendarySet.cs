using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryDataCore.Models
{

    public partial class LegendarySetModel
    {
        [Key]
        public int SetId { get; set; }

        [JsonProperty("Set Name", Required = Required.Always)]
        public string Set { get; set; }

        [JsonProperty("Owned", Required = Required.Always)]
        public bool Owned { get; set; }

        public List<MastermindModel> Masterminds { get; set; }
        public List<SchemeModel> Schemes { get; set; }
        public List<VillainModel> Villains { get; set; }
        public List<HenchmenModel> Henchmen { get; set; }
        public List<HeroModel> Heroes { get; set; }
    }

}
