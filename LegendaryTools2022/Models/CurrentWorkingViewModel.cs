using LegendaryTools2022.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTools2022.Models.ViewModels
{
   public class CurrentWorkingViewModel
    {
        public CustomSets CurrentSetModel { get; set; }
        public Decks CurrentDeckModel { get; set; }
        public Cards CurrentCardModel { get; set; }
        public Templates CurrentTemplateModel { get; set; }
        public string CurrentSetPath { get; set; }
        public string CurrentSetDataFile { get; set; }
    }
}
