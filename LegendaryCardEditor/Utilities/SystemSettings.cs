using System.Windows.Forms;

namespace LegendaryCardEditor.Utilities
{
    public class SystemSettings : AppSettings<SystemSettings>
    {
        string _path;

        // public string lastFolder = @"";
        //public string lastProject = "";

        public string baseFolder { get; set; }
        public string imagesFolder { get; set; }
        public string iconsFolder { get; set; }
        public string templatesFolder { get; set; }

        public string json_decktypes = "decktypes.json";
        public string json_keywords = "keywords.json";
        public string json_icons = "icons.json";
        public string json_templatetypes = "template_types.json";
        public string json_templates = "templates.json";
        public string default_blank_card = "default_blank_card.png"; 
        
        public SystemSettings()
        {
            baseFolder = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            imagesFolder = $"{baseFolder}\\images";
            iconsFolder = $"{baseFolder}\\icons";
            templatesFolder = $"{baseFolder}\\templates";


        }
    }
}
