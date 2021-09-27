using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTemplateEditor.Utilities
{
    
    public class SettingPropertyGridProxy
    {
        private SystemSettings _settings;
        private DirectoryInfo baseFolder;
        public SettingPropertyGridProxy(SystemSettings settings)
        {
            _settings = settings;
        }

       
        [Description("BaseFolder")]
        [DefaultValue(typeof(string), "C:\\")]
        public string BaseFolder
        {
            get { return _settings.baseFolder; }
            set { _settings.baseFolder = value; }
        }


    }
}
