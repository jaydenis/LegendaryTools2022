using LegendaryTools2022.Models;
using LegendaryTools2022.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTools2022.Managers
{
    public class CoreManager
    {
        SystemSettings settings;

        public CoreManager()
        {
            settings = SystemSettings.Load();
            settings.Save();
        }
        public List<DeckTypeModel> GetDeckTypes()
        {
            var path = settings.baseFolder + "\\" + settings.json_decktypes;
            string jsonText = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<IList<DeckTypeModel>>(jsonText).ToList();

            return dataModel;
        }

        public TemplateModel ReadTemplateSettings()
        {
            string path = settings.baseFolder + "\\" + settings.json_templates;
            string jsonText = File.ReadAllText(path);

            var dataModel = JsonConvert.DeserializeObject<TemplateModel>(jsonText);

            return dataModel;
        }

        public CardTemplates ReadTemplateSettings(string templateFolder)
        {
            string path = settings.baseFolder + "\\cards\\" + templateFolder + "\\" + settings.json_templates;
            string jsonText = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<CardTemplates>(jsonText);

            return dataModel;
        }

        public bool SaveTemplateSettings(string path, TemplateModel template)
        {
            try
            {
                path = path + "\\template.json";
                File.WriteAllText(path, JsonConvert.SerializeObject(template, Formatting.Indented));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Dictionary<string, List<CardTypeModel>> GetCardTypeViewModel()
        {
            string path = settings.baseFolder + "\\" + settings.json_cardtypes;
            string jsonText = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<IList<CardTypeModel>>(jsonText).ToList();

            var result = new Dictionary<string, List<CardTypeModel>>();

            foreach (CardTypeModel item in dataModel)
            {
                if (!result.ContainsKey(item.Category))
                {
                    var cardTypeList = dataModel.Where(x => x.Category == item.Category).ToList();

                    result.Add(item.Category, cardTypeList);
                }
            }

            return result;
        }

        public List<CardTypeModel> GetCardTypes()
        {
            string path = settings.baseFolder + "\\" + settings.json_cardtypes;
            string jsonText = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<IList<CardTypeModel>>(jsonText).ToList();

            var result = new List<CardTypeModel>();

            foreach (CardTypeModel item in dataModel)
            {
                    result.Add(item);
                
            }

            return result;
        }

        public List<LegendaryIconModel> LoadIconsFromDirectory()
        {

            string jsonText = File.ReadAllText(settings.baseFolder + "\\" + settings.json_icons);
            var dataModel = JsonConvert.DeserializeObject<IList<LegendaryIconModel>>(jsonText).ToList();

            var result = new List<LegendaryIconModel>();

            foreach (LegendaryIconModel item in dataModel.ToList())
            {

                item.FileName = ($@"{settings.baseFolder + "\\" + settings.iconsFolder}\{item.Category}\{item.FileName}").ToLower();
                result.Add(item);

            }

            return result;


        }

        public CustomSetsModel OpenCustomSets(string path)
        {
            string jsonText = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<CustomSetsModel>(jsonText);

            return dataModel;
        }

        public CustomSetProjectModel OpenCustomSet(string path)
        {
            string jsonText = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<CustomSetProjectModel>(jsonText);

            return dataModel;
        }

        public bool SaveCustomSet(CustomSetProjectModel model, string path)
        {
            try
            {

                File.WriteAllText(path, JsonConvert.SerializeObject(model, Formatting.Indented));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



    }
}
