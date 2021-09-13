using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryCardEditor.Models
{
    public class CurrentActiveDataModel
    {
        public DeckList AllDecksInSet { get; set; }
        public Deck ActiveDeck { get; set; }
        public CardModel SelectedCard { get; set; }
        public string ActiveSetPath { get; set; }
        public string ActiveSetDataFile { get; set; }
    }

    public class CardModel
    {
        public Card ActiveCard { get; set; }
        public LegendaryTemplateModel ActiveTemplate { get; set; }
    }
}
