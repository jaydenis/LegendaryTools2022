using LegendaryTools2022.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTools2022.Models
{

    public partial class CustomSetProjectModel
    {
        [JsonProperty("set_id")]
        public int SetId { get; set; }

        [JsonProperty("set_name")]
        public string SetName { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("date_created")]
        public DateTimeOffset DateCreated { get; set; }

        [JsonProperty("date_updated")]
        public DateTimeOffset DateUpdated { get; set; }

        [JsonProperty("decks")]
        public List<DeckModel> Decks { get; set; }
    }

    public partial class DeckModel
    {
        [JsonProperty("deck_id")]
        public int DeckId { get; set; }

        [JsonProperty("deck_type")]
        public string DeckType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("team_icon")]
        public int TeamIcon { get; set; }

        [JsonProperty("cards")]
        public List<CardModel> Cards { get; set; }
    }

    public partial class CardModel
    {
        [JsonProperty("card_id")]
        public int CardId { get; set; }
        [JsonProperty("deck_id")]
        public int DeckId { get; set; }

        [JsonProperty("team_icon")]
        public int TeamIcon { get; set; }
        [JsonProperty("card_name")]
        public string CardName { get; set; }

        [JsonProperty("card_type")]
        public string CardType { get; set; }

        [JsonProperty("card_type_template")]
        public string CardTypeTemplate { get; set; }

        [JsonProperty("card_back_text_image")]
        public string CardBackTextImage { get; set; }

        [JsonProperty("art_work_path")]
        public string ArtWorkPath { get; set; }

        [JsonProperty("art_work_size")]
        public string ArtWorkSize { get; set; }

        [JsonProperty("art_work_zoom")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool ArtWorkZoom { get; set; }

        [JsonProperty("card_display_name")]
        public string CardDisplayName { get; set; }

        [JsonProperty("card_display_name_font_size")]        
        public int CardDisplayNameFontSize { get; set; }

        [JsonProperty("card_name_sub")]
        public string CardNameSub { get; set; }

        //card_name_sub_font_size
        [JsonProperty("card_name_sub_font_size")]
        public int CardNameSubFontSize { get; set; }

        [JsonProperty("power")]
        public string Power { get; set; }
        
        [JsonProperty("power_icon")]
        public int PowerIcon { get; set; }

        [JsonProperty("power2")]
        public string Power2 { get; set; }
        [JsonProperty("power2_icon")]
        public int Power2Icon { get; set; }

        [JsonProperty("frame_image")]
        public string FrameImage { get; set; }

        [JsonProperty("attributes_cost")]        
        public string AttributesCost { get; set; }

        [JsonProperty("attributes_attack")]        
        public string AttributesAttack { get; set; }

        [JsonProperty("attributes_recruit")]
        public string AttributesRecruit { get; set; }

        [JsonProperty("attributes_piercing")]
        public string AttributesPiercing { get; set; }

        [JsonProperty("attributes_vp")]
        public string AttributesVp { get; set; }

        [JsonProperty("card_text")]
        public string CardText { get; set; }

        [JsonProperty("card_text_font_size")]        
        public int CardTextFontSize { get; set; }

        [JsonProperty("number_in_deck")]
        public int NumberInDeck { get; set; }

        [JsonProperty("exported_card_image")]
        public string ExportedCardImage { get; set; }
    }
}
