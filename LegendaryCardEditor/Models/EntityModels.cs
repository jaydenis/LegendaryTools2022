using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryCardEditor.Models
{
    public partial class CustomSetsViewModel
    {
        public List<CustomSets> CustomSets { get; set; } = new List<CustomSets>();
    }

    [Table("CustomSets")]
    public partial class CustomSets
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SetId { get; set; }

        [Required]
        [MaxLength(50)]
        public String SetName { get; set; }

        [Required]
        [MaxLength(254)]
        public String SetWorkPath { get; set; }

        [Required]
        [MaxLength(50)]
        public String SetDisplayName { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public virtual ICollection<Decks> Decks { get; set; }
    }

    [Table("Decks")]
    public partial class Decks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DeckId { get; set; }

        [Required]
        [MaxLength(50)]
        public String DeckName { get; set; }

        [Required]
        [MaxLength(50)]
        public String DeckDisplayName { get; set; }

        public int TeamIconId { get; set; } = 0;

        public virtual CustomSets CustomSet { get; set; }

        public virtual DeckTypes DeckType { get; set; }

        public virtual ICollection<Cards> Cards { get; set; }

    }

    [Table("Cards")]
    public partial class Cards
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CardId { get; set; }

        [Required]
        [MaxLength(254)]
        public String CardName { get; set; }

        [Required]
        [MaxLength(254)]
        public String CardDisplayName { get; set; }

        public int CardDisplayNameFont { get; set; } = 32;

        [Required]
        [MaxLength(254)]
        public String CardDisplayNameSub { get; set; }

        public int CardDisplayNameSubFont { get; set; } = 28;

        public virtual CardTypes CardType { get; set; }

        public virtual Templates CardTemplate { get; set; }

        [MaxLength(25)]
        public String PowerPrimary { get; set; }

        [MaxLength(25)]
        public String PowerSecondary { get; set; }

        [MaxLength(4)]
        public String AttributeCost { get; set; }

        [MaxLength(4)]
        public String AttributeAttack { get; set; }

        [MaxLength(4)]
        public String AttributeRecruit { get; set; }

        [MaxLength(4)]
        public String AttributePiercing { get; set; }

        [MaxLength(4)]
        public String AttributeVictoryPoints { get; set; }

        [MaxLength(500)]
        public String CardText { get; set; }

        public int CardTextFont { get; set; } = 22;

        public int NumberInDeck { get; set; } = 1;

        [MaxLength(254)]
        public String ArtWorkFile { get; set; }

        [MaxLength(254)]
        public String ExportedCardFile { get; set; }

        public virtual Decks Deck { get; set; }

        public int TeamIconId { get; set; }

        public int PowerPrimaryIconId { get; set; }

        public int PowerSecondaryIconId { get; set; }

    }

    [Table("CardTypes")]
    public partial class CardTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CardTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public String CardTypeName { get; set; }

        [Required]
        [MaxLength(50)]
        public String CardTypeDisplayName { get; set; }



    }

    [Table("DeckTypes")]
    public partial class DeckTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DeckTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public String DeckTypeName { get; set; }

    }

    [Table("Templates")]
    public partial class Templates
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TemplateId { get; set; }

        [Required]
        [MaxLength(50)]
        public String TemplateName { get; set; }

        [Required]
        [MaxLength(50)]
        public String TemplateDisplayName { get; set; }

        [Required]
        [MaxLength(254)]
        public String RectXArray { get; set; }

        [Required]
        [MaxLength(254)]
        public String RectYArray { get; set; }

        public int CardWidth { get; set; } = 504;

        public int CardHeight { get; set; } = 750;

        [Required]
        [MaxLength(254)]
        public String FrameImage { get; set; }

        [MaxLength(254)]
        public String CostImage { get; set; }

        [MaxLength(254)]
        public String TextImage { get; set; }

        [MaxLength(254)]
        public String UnderlayImage { get; set; }

        public bool FormShowTeam { get; set; } = false;

        public bool FormShowPowerPrimary { get; set; } = false;

        public bool FormShowPowerSecondary { get; set; } = false;

        public bool FormShowAttributes { get; set; } = false;

        public bool FormShowAttributesCost { get; set; } = false;

        public bool FormShowAttributesRecruit { get; set; } = false;

        public bool FormShowAttributesAttack { get; set; } = false;

        public bool FormShowAttributesPiercing { get; set; } = false;

        public bool FormShowVictoryPoints { get; set; } = false;

        public bool FormShowAttackCost { get; set; } = false;

        //public virtual Cards Card { get; set; }

    }
}
