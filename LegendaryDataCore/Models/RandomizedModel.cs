
using LegendaryDataCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryDataCore.Models
{
    public partial class RandomizedModel
    {
        public GameTypeEnum GameType { get; set; }
        public MastermindModel Mastermind { get; set; }
        public SchemeModel Scheme { get; set; }
        public List<VillainModel> Villains { get; set; }
        public List<HenchmenModel> Henchmen { get; set; }
        public List<HeroModel> Heroes { get; set; }


    }


}
