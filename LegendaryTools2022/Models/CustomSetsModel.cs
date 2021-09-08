using LegendaryTools2022.Models.Entities;
using LegendaryTools2022.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SQLite;
using System;
using System.Collections.Generic;

namespace LegendaryTools2022.Models
{


    public partial class XCustomSetsViewModel
    {
        public List<CustomSetViewModel> CustomSets { get; set; } = new List<CustomSetViewModel>();
    }

    public partial class CustomSetViewModel 
    {        
        public List<DeckViewModel> Decks { get; set; } = new List<DeckViewModel>();
    }

    public partial class DeckViewModel 
    {
  
        public List<CardViewModel> Cards { get; set; } = new List<CardViewModel>();

    }

    public partial class CardViewModel 
    {

    }

    public partial class CardTypeViewModel
    {
        public Int64 CardTypeId { get; set; }
        public string CardTypeName { get; set; }

        public string CardTypeDisplayname { get; set; }

        public int DeckTypeId { get; set; }
    }

    public partial class DeckTypeViewModel
    {
        public Int64 DeckTypeId { get; set; }
        public string DeckTypeName { get; set; }
    }

    public partial class CardTemplateViewModel
    {
        public Int64 TemplateId { get; set; }
        public string TemplateName { get; set; }
        public string TemplateDisplayName { get; set; }
        public string RectXArray { get; set; }
        public string RectYArray { get; set; }
        public int CardHeight { get; set; }
        public int CardWidth { get; set; }
        public string FrameImage { get; set; }
        public string CostImage { get; set; }
        public string TextImage { get; set; }
        public string UnderlayImage { get; set; }
        public bool FormShowTeam { get; set; }
        public bool FormShowPowerPrimary { get; set; }
        public bool FormShowPowerSecondary { get; set; }
        public bool FormShowAttributes { get; set; }
        public bool FormShowAttributesCost { get; set; }
        public bool FormShowAttributesRecruit { get; set; }
        public bool FormShowAttributesAttack { get; set; }
        public bool FormShowAttributesPiercing { get; set; }
        public bool FormShowVictoryPoints { get; set; }
        public bool FormShowAttackCost { get; set; }

    }





}
