using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTools2022.Models
{
   public class CardNodeModel
    {
        public CustomSetProjectModel SelectedSetModel { get; set; }
        public DeckModel SelectedDeckModel { get; set; }
        public CardModel SelectedCardModel { get; set; }

        public string CurrentCustomSetPath { get; set; }
    }
}
