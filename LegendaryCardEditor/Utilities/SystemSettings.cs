using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryCardEditor.Utilities
{
    public class SystemSettings : AppSettings<SystemSettings>
    {
        public string lastFolder = @"";
        public string lastProject = "";

        public string baseFolder = @"C:\Repos\LegendaryTools2022\LegendaryCardEditor";
        public string imagesFolder = @"C:\Repos\LegendaryTools2022\LegendaryCardEditor\images";
        public string iconsFolder = @"C:\Repos\LegendaryTools2022\LegendaryCardEditor\images\icons";
        public string templatesFolder = @"C:\Repos\LegendaryTools2022\LegendaryCardEditor\Templates";

        public string json_decktypes = "decktypes.json";
        //public string json_cardgroups = "cardgroups.json";
        public string json_icons = "icons.json";
        //public string json_cardtypes = "cardtypes.json";
        public string json_templates = "template.json";
        public string default_blank_card = "default_blank_card.png";
    }
}
