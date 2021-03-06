using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryCardEditor.Models
{
    public class CurrentActiveDataModel
    {
        public int Id { get; set; }
        public DeckList AllDecksInSet { get; set; }
        public Deck ActiveDeck { get; set; }
        public CardModel SelectedCard { get; set; }
        public List<CardModel> AllCardsInDeck { get; set; }
        public string ActiveSetPath { get; set; }
        public string ActiveSetDataFile { get; set; }
    }

    public class CardModel
    {
        public string Id { get; set; }
        public CardEntity ActiveCard { get; set; }
        public TemplateEntity ActiveTemplate { get; set; }
    }
}
