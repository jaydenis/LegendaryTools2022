using LegendaryCardEditor.Models;
using LegendaryCardEditor.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryCardEditor.Managers
{
    public class CoreManager
    {
        SystemSettings settings;

        public CoreManager()
        {
            settings = SystemSettings.Load();
            settings.Save();
        }
       

        public List<LegendaryIconViewModel> LoadIconsFromDirectory()
        {

            string jsonText = File.ReadAllText(settings.baseFolder + "\\" + settings.json_icons);
            var dataModel = JsonConvert.DeserializeObject<IList<LegendaryIconViewModel>>(jsonText).ToList();

            var result = new List<LegendaryIconViewModel>();

            foreach (LegendaryIconViewModel item in dataModel.ToList())
            {

                item.FileName = ($@"{settings.baseFolder + "\\" + settings.iconsFolder}\{item.Category}\{item.FileName}").ToLower();
                result.Add(item);

            }

            return result;


        }

        


    }
}
