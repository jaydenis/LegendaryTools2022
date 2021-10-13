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
  
    public partial class MastermindModel
    {
        [Key]
        public int MastermindId { get; set; }
        [JsonProperty("Set", Required = Required.Always)]
        public LegendarySetModel Set { get; set; }
        
        [JsonProperty("Mastermind Name", Required = Required.Always)]
        public string MastermindName { get; set; }

        [JsonProperty("Henchman", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public string Henchman { get; set; }

        [JsonProperty("Villain", Required = Required.AllowNull, NullValueHandling = NullValueHandling.Include)]
        public string Villain { get; set; }
    }
    
    public partial class HeroModel
    {
        [Key]
        public int HeroId { get; set; }
        [JsonProperty("Set", Required = Required.Always)]
        public LegendarySetModel Set { get; set; }
    
        [JsonProperty("Hero Name", Required = Required.Always)]
        public string HeroName { get; set; }
       
    }
    
    public partial class VillainModel
    {
        [Key]
        public int VillainId { get; set; }
        [JsonProperty("Set", Required = Required.Always)]
        public LegendarySetModel Set { get; set; }

        [JsonProperty("Villain Name", Required = Required.Always)]
        public string VillainName { get; set; }


    }
    
    public partial class HenchmenModel
    {
        [Key]
        public int HenchmenId { get; set; }

        [JsonProperty("Set", Required = Required.Always)]
        public LegendarySetModel Set { get; set; }

       
        [JsonProperty("Henchmen Name", Required = Required.Always)]
        public string HenchmenName { get; set; }

    }
    
    public partial class SchemeModel
    {
        [Key]
        public int SchemeId { get; set; }
        [JsonProperty("Set", Required = Required.Always)]
        public LegendarySetModel Set { get; set; }

      
        [JsonProperty("Scheme Name", Required = Required.Always)]
        public string HenchmenName { get; set; }

    }

}
