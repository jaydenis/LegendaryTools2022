using LegendaryCardEditor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryCardEditor.Data
{
    public static class MyDbContextSeeder
    {
        public static void Seed(DataContext context)
        {

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.EntityDeckTypes.Add(new DeckTypes()
            {
                DeckTypeName = "Hero"
            });
            context.EntityDeckTypes.Add(new DeckTypes()
            {
                DeckTypeName = "Mastermind"
            });
            context.EntityDeckTypes.Add(new DeckTypes()
            {
                DeckTypeName = "Bystander",
                //ParentCategory = context.EntityDeckTypes.Local.Single(p => p.DeckTypeName == "novel")
            });
            context.EntityDeckTypes.Add(new DeckTypes()
            {
                DeckTypeName = "Villain"
            });
            context.EntityDeckTypes.Add(new DeckTypes()
            {
                DeckTypeName = "Special Sidekick",
                // ParentCategory = context.EntityDeckTypes.Local.Single(p => p.DeckTypeName == "novel")
            });
            context.EntityDeckTypes.Add(new DeckTypes()
            {
                DeckTypeName = "S.H.I.E.L.D. Officer",
                //ParentCategory = context.EntityDeckTypes.Local.Single(p => p.DeckTypeName == "fantasy")
            });
            context.EntityDeckTypes.Add(new DeckTypes()
            {
                DeckTypeName = "Wound"
            });
            context.EntityDeckTypes.Add(new DeckTypes()
            {
                DeckTypeName = "Horror",
                //ParentCategory = context.EntityDeckTypes.Local.Single(p => p.DeckTypeName == "textbook")
            });
            context.EntityDeckTypes.Add(new DeckTypes()
            {
                DeckTypeName = "Ambition",
                // ParentCategory = context.EntityDeckTypes.Local.Single(p => p.DeckTypeName == "novel")
            });

            context.EntityCardTypes.Add(new CardTypes()
            {
                CardTypeDisplayName = "Hero - Common",
                CardTypeName = "hero_common"
            });

            context.EntityCardTypes.Add(new CardTypes()
            {
                CardTypeDisplayName = "Hero - Unommon",
                CardTypeName = "hero_uncommon"
            });

            context.EntityCardTypes.Add(new CardTypes()
            {
                CardTypeDisplayName = "Hero - Rare",
                CardTypeName = "hero_rare"
            });

            context.EntityCardTypes.Add(new CardTypes()
            {
                CardTypeDisplayName = "Mastermind",
                CardTypeName = "mastermind"
            });

            context.EntityCardTypes.Add(new CardTypes()
            {
                CardTypeDisplayName = "Mastermind - Tactic",
                CardTypeName = "mastermind_tactic"
            });


            context.EntityDecks.Add(new Decks()
            {
                DeckName = "angela",
                DeckDisplayName = "Angela",
                DeckType = context.EntityDeckTypes.Local.Single(x => x.DeckTypeName.ToLower() == "hero"),
                TeamIconId = 2,
            });

            context.EntityCustomSets.Add(new CustomSets()
            {
                SetName = "battleworld_killville",
                SetDisplayName = "Battleworld - Killville",
                SetWorkPath = @"C:\Repos\CustomSets\sets\battleworld-killville",
                DateCreated = DateTime.Now

            });



            context.EntityTemplates.Add(new Templates()
            {
                TemplateName = "hero_common",
                TemplateDisplayName = "Hero - Common",
                RectXArray = "92,440,350,92",
                RectYArray = "522,522,670,670",
                CardWidth = 750,
                CardHeight = 1050,
                FrameImage = "hero_common_none.png",
                CostImage = "back_cost.png",
                TextImage = "back_text.png",
                UnderlayImage = "back_underlay.png",
                FormShowAttributes = false,
                FormShowAttributesAttack = false,
                FormShowAttributesCost = false,
                FormShowAttributesPiercing = false,
                FormShowAttributesRecruit = false,
                FormShowPowerPrimary = false,
                FormShowPowerSecondary = false,
                FormShowTeam = false,
                FormShowAttackCost = false,
                FormShowVictoryPoints = false
            });
            context.EntityTemplates.Add(new Templates()
            {
                TemplateName = "hero_uncommon",
                TemplateDisplayName = "Hero - Unommon",
                RectXArray = "92,440,350,92",
                RectYArray = "522,522,670,670",
                CardWidth = 750,
                CardHeight = 1050,
                FrameImage = "hero_uncommon_none.png",
                CostImage = "back_cost.png",
                TextImage = "back_text.png",
                UnderlayImage = "back_underlay.png",
                FormShowAttributes = false,
                FormShowAttributesAttack = false,
                FormShowAttributesCost = false,
                FormShowAttributesPiercing = false,
                FormShowAttributesRecruit = false,
                FormShowPowerPrimary = false,
                FormShowPowerSecondary = false,
                FormShowTeam = false,
                FormShowAttackCost = false,
                FormShowVictoryPoints = false
            });

            context.EntityTemplates.Add(new Templates()
            {
                TemplateName = "hero_rare",
                TemplateDisplayName = "Hero - Rare",
                RectXArray = "85,420,420,85",
                RectYArray = "522,522,670,670",
                CardWidth = 750,
                CardHeight = 1050,
                FrameImage = "hero_rare.png",
                CostImage = "back_cost.png",
                TextImage = "back_text.png",
                UnderlayImage = "back_underlay.png",
                FormShowAttributes = false,
                FormShowAttributesAttack = false,
                FormShowAttributesCost = false,
                FormShowAttributesPiercing = false,
                FormShowAttributesRecruit = false,
                FormShowPowerPrimary = false,
                FormShowPowerSecondary = false,
                FormShowTeam = false,
                FormShowAttackCost = false,
                FormShowVictoryPoints = false
            });

            context.EntityCards.Add(new Cards()
            {
                CardName = "angela_hellfire",
                CardDisplayName = "Hellfire",
                CardDisplayNameFont = 36,
                CardDisplayNameSub = "Angela",
                CardDisplayNameSubFont = 28,
                CardTemplate = context.EntityTemplates.Local.Single(x => x.TemplateName == "hero_common"),
                CardText = "You may gain a Wound. If you do get <k>+2<ATTACK>.",
                CardTextFont = 22,
                CardType = context.EntityCardTypes.Local.Single(x => x.CardTypeName == "hero_common"),
                ExportedCardFile = "angela_hellfire_hero_rare_2221013.png",
                ArtWorkFile = "img_117896305.png",
                AttributeCost = "8",
                AttributeAttack = "3",
                AttributePiercing = "",
                AttributeRecruit = "",
                AttributeVictoryPoints = "",
                NumberInDeck = 5,
                PowerPrimary = "<TECH>",
                PowerPrimaryIconId = 1,
                PowerSecondary = "",
                PowerSecondaryIconId = -1,
                TeamIconId = 2
            });

            context.EntityCards.Add(new Cards()
            {
                CardName = "angela_kill_the_spider_man",
                CardDisplayName = "Kill The spider-man",
                CardDisplayNameFont = 36,
                CardDisplayNameSub = "Angela",
                CardDisplayNameSubFont = 28,
                CardTemplate = context.EntityTemplates.Local.Single(x => x.TemplateName == "hero_uncommon"),
                CardText = "You may gain a Wound. If you do get <k>+2<ATTACK>.",
                CardTextFont = 22,
                CardType = context.EntityCardTypes.Local.Single(x => x.CardTypeName == "hero_uncommon"),
                ExportedCardFile = "angela_hellfire_hero_rare_2221013.png",
                ArtWorkFile = "img76214851.png",
                AttributeCost = "4",
                AttributeAttack = "3",
                AttributePiercing = "",
                AttributeRecruit = "",
                AttributeVictoryPoints = "",
                NumberInDeck = 5,
                PowerPrimary = "<TECH>",
                PowerPrimaryIconId = 1,
                PowerSecondary = "",
                PowerSecondaryIconId = -1,
                TeamIconId = 2
            });


            context.SaveChanges();
        }
    }
}
