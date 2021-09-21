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
        public List<DeckTypeModel> GetDeckTypes()
        {
            var path = settings.templatesFolder + "\\" + settings.json_decktypes;
            string jsonText = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<IList<DeckTypeModel>>(jsonText).ToList();

            return dataModel;
        }

        public LegendaryTemplateModel ReadTemplateSettings()
        {
            string path = settings.templatesFolder + "\\" + settings.json_templates;
            string jsonText = File.ReadAllText(path);

            var dataModel = JsonConvert.DeserializeObject<LegendaryTemplateModel>(jsonText);

            return dataModel;
        }

        public List<LegendaryTemplateModel> GetTemplates()
        {
            string path = settings.templatesFolder + "\\" + settings.json_templates;
            string jsonText = File.ReadAllText(path);

            var dataModel = JsonConvert.DeserializeObject<IList<LegendaryTemplateModel>>(jsonText).ToList();

            return dataModel;
        }

        //public CardTemplates ReadTemplateSettings(string templateFolder)
        //{
        //    string path = settings.baseFolder + "\\cards\\" + templateFolder + "\\" + settings.json_templates;
        //    string jsonText = File.ReadAllText(path);
        //    var dataModel = JsonConvert.DeserializeObject<CardTemplates>(jsonText);

        //    return dataModel;
        //}

        public bool SaveTemplateSettings(string path, LegendaryTemplateModel template)
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

        //public Dictionary<string, List<CardTypeModel>> GetCardTypeViewModel()
        //{
        //    string path = settings.baseFolder + "\\" + settings.json_cardtypes;
        //    string jsonText = File.ReadAllText(path);
        //    var dataModel = JsonConvert.DeserializeObject<IList<CardTypeModel>>(jsonText).ToList();

        //    var result = new Dictionary<string, List<CardTypeModel>>();

        //    foreach (CardTypeModel item in dataModel)
        //    {
        //        if (!result.ContainsKey(item.Category))
        //        {
        //            var cardTypeList = dataModel.Where(x => x.Category == item.Category).ToList();

        //            result.Add(item.Category, cardTypeList);
        //        }
        //    }

        //    return result;
        //}

        //public List<CardTypeModel> GetCardTypes()
        //{

        //    var path = settings.templatesFolder + "\\" + settings.json_cardtypes;
        //    string jsonText = File.ReadAllText(path);
        //    var dataModel = JsonConvert.DeserializeObject<IList<CardTypeModel>>(jsonText).ToList();

        //    return dataModel;           
        //}

        public List<LegendaryIconViewModel> LoadIconsFromDirectory()
        {

            string jsonText = File.ReadAllText(settings.templatesFolder + "\\" + settings.json_icons);
            var dataModel = JsonConvert.DeserializeObject<IList<LegendaryIconViewModel>>(jsonText).ToList();

            var result = new List<LegendaryIconViewModel>();

            foreach (LegendaryIconViewModel item in dataModel.ToList())
            {
                item.FileName = ($@"{settings.iconsFolder}\{item.Category}\{item.FileName}").ToLower();
                result.Add(item);
            }

            return result;


        }

        public LegendaryCustomSet OpenCustomSets(string path)
        {
            string jsonText = File.ReadAllText(path);
            var dataModel = JsonConvert.DeserializeObject<LegendaryCustomSet>(jsonText);

            return dataModel;
        }

        public DeckList GetDecks(string path)
        {
            try
            {
                string jsonText = File.ReadAllText(path);
                var dataModel = JsonConvert.DeserializeObject<DeckList>(jsonText);

                return dataModel;
            }
            catch
            {
                return null;
            }
        }

        public bool SaveCustomSet(CustomSet model, string path)
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

        public bool SaveDeck(DeckList model, string path)
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
