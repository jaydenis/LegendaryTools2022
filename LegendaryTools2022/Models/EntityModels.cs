
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTools2022.Models.Entities
{
    public partial class CustomSetsViewModel
    {
        public List<CustomSets> CustomSets { get; set; } = new List<CustomSets>();
    }

    public partial class CustomSets
    {
        [Key]
        public Int64 SetId { get; set; }

        [MaxLength(50)]
        public String SetName { get; set; }

        [MaxLength(254)]
        public String SetWorkPath { get; set; }

        [MaxLength(50)]
        public String SetDisplayName { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public virtual IEnumerable<Decks> Decks { get; set; }
    }

    public partial class Decks
    {
        [Key]
        public Int64 DeckId { get; set; }

        [MaxLength(50)]
        public String DeckName { get; set; }

        [MaxLength(50)]
        public String DeckDisplayName { get; set; }

        public Int32? DeckTypeId { get; set; }

        public Int32? TeamIconId { get; set; }

        public Int32? SetId { get; set; }

        public virtual IEnumerable<Cards> Cards { get; set; }

    }

    public partial class Cards
    {
        [Key]
        public Int64 CardId { get; set; }

        [MaxLength(254)]
        public String CardName { get; set; }

        [MaxLength(254)]
        public String CardDisplayName { get; set; }

        public Int32? CardDisplayNameFont { get; set; }

        [MaxLength(254)]
        public String CardDisplayNameSub { get; set; }

        public Int32? CardDisplayNameSubFont { get; set; }

        public Int32? CardTypeId { get; set; }

        public Int32 TemplateId { get; set; }

        [MaxLength(254)]
        public String PowerPrimary { get; set; }

        [MaxLength(254)]
        public String PowerSecondary { get; set; }

        [MaxLength(4)]
        public String AttributeCost { get; set; }

        [MaxLength(4)]
        public String AttributeAttack { get; set; }

        [MaxLength(4)]
        public String AttributeRecruit { get; set; }

        [MaxLength(4)]
        public String AttributePiercing { get; set; }

        [MaxLength(254)]
        public String AttributeVictoryPoints { get; set; }

        [MaxLength(500)]
        public String CardText { get; set; }

        public Int32? CardTextFont { get; set; }

        public Int32? NumberInDeck { get; set; }

        [MaxLength(254)]
        public String ArtWorkFile { get; set; }

        [MaxLength(254)]
        public String ExportedCardFile { get; set; }

        public Int32? DeckId { get; set; }

        public Int32? TeamIconId { get; set; }

        public Int32? PowerPrimaryIconId { get; set; }

        public Int32? PowerSecondaryIconId { get; set; }

    }

    public partial class CardTypes
    {
        [Key]
        public Int64 CardTypeId { get; set; }

        [MaxLength(50)]
        public String CardTypeName { get; set; }

        [MaxLength(50)]
        public String CardTypeDisplayName { get; set; }

        public Int32? DeckTypeId { get; set; }

    }

    public partial class DeckTypes
    {
        [Key]
        public Int64 DeckTypeId { get; set; }

        [MaxLength(50)]
        public String DeckTypeName { get; set; }

    }

    public partial class Templates
    {
        [Key]
        public Int64 TemplateId { get; set; }

        [MaxLength(50)]
        public String TemplateName { get; set; }

        [MaxLength(50)]
        public String TemplateDisplayName { get; set; }

        [MaxLength(254)]
        public String RectXArray { get; set; }

        [MaxLength(254)]
        public String RectYArray { get; set; }

        public Int32? CardWidth { get; set; }

        public Int32? CardHeight { get; set; }

        [MaxLength(254)]
        public String FrameImage { get; set; }

        [MaxLength(254)]
        public String CostImage { get; set; }

        [MaxLength(254)]
        public String TextImage { get; set; }

        [MaxLength(254)]
        public String UnderlayImage { get; set; }

        public Boolean? FormShowTeam { get; set; }

        public Boolean? FormShowPowerPrimary { get; set; }

        public Boolean? FormShowPowerSecondary { get; set; }

        public Boolean? FormShowAttributes { get; set; }

        public Boolean? FormShowAttributesCost { get; set; }

        public Boolean? FormShowAttributesRecruit { get; set; }

        public Boolean? FormShowAttributesAttack { get; set; }

        public Boolean? FormShowAttributesPiercing { get; set; }

        public Boolean? FormShowVictoryPoints { get; set; }

        public Boolean? FormShowAttackCost { get; set; }

    }
}
