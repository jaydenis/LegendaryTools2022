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
        public Card SelectedCard { get; set; }
        public LegendaryTemplateModel ActiveTemplate { get; set; }
        public string ActiveSetPath { get; set; }
        public string ActiveSetDataFile { get; set; }
    }
}
