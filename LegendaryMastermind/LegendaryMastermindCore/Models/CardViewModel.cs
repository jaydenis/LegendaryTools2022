using LegendaryMastermindCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace LegendaryMastermindCore.Models
{
    public class CardViewModel : BaseViewModel
    {
        public CardTypeEnum CardType {get;set;}
    }

    public class CardHeroViewModel : BaseViewModel
    {
        public CardTypeSubEnum CardTypeSub { get; set; }
        public string SubTitle { get; set; }
        public string Team { get; set; }
        public PowerTypeEnum PowerTypePrimary { get; set; }
        public PowerTypeEnum? PowerTypeSecondary { get; set; }
        public string ValueCost { get; set; }
        public string ValueRecruit { get; set; }
        public string ValueAttack { get; set; }
        public string ValuePierce { get; set; }
        public string CardRulesText { get; set; }
        public string CardFlavorText { get; set; }

    }

    public class CardVillainViewModel : BaseViewModel
    {
        public CardTypeSubEnum CardTypeSub { get; set; }
        public string SubTitle { get; set; }
        public string Team { get; set; }       
        public string ValueVictoryPoints { get; set; }
        public string ValueAttack { get; set; }
        public string CardRulesText { get; set; }
        public string CardFlavorText { get; set; }

    }


}
