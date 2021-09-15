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
        public string iconsFolder = "images\\icons";

        public string json_decktypes = "Templates\\decktypes.json";
        public string json_cardgroups = "Templates\\cardgroups.json";
        public string json_icons = "Templates\\icons.json";
        public string json_cardtypes = "Templates\\cardtypes.json";
        public string json_templates = "Templates\\template.json";
        public string default_blank_card = "images\\default_blank_card.png";
    }
}
