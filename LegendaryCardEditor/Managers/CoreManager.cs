using LegendaryCardEditor.Models;
using LegendaryCardEditor.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LegendaryCardEditor.Managers
{
    public class CoreManager
    {
        SystemSettings settings;
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public CoreManager()
        {
            settings = SystemSettings.Load();
            settings.Save();
        }
        public List<DeckTypeModel> GetDeckTypes()
        {
            try
            {
                var path = settings.templatesFolder + "\\" + settings.json_decktypes;
                string jsonText = File.ReadAllText(path);
                var dataModel = JsonConvert.DeserializeObject<IList<DeckTypeModel>>(jsonText).ToList();

                return dataModel;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                return null;
            }
        }
        public List<Templates> GetTemplates()
        {
            try
            {
                string path = settings.templatesFolder + "\\" + settings.json_templates;
                string jsonText = File.ReadAllText(path);

                var dataModel = JsonConvert.DeserializeObject<IList<Templates>>(jsonText).ToList();

                return dataModel;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                return null;
            }
        }

        public List<TemplateTypeModel> GetTemplateTypes()
        {
            try
            {
                string path = settings.templatesFolder + "\\" + settings.json_templatetypes;
                string jsonText = File.ReadAllText(path);

                var dataModel = JsonConvert.DeserializeObject<IList<TemplateTypeModel>>(jsonText).ToList();

                return dataModel;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                return null;
            }
        }

        public TemplateEntity GetTemplate(string templateName)
        {
            try
            {
                string path = settings.templatesFolder + "\\cards\\" + templateName + ".json";
                string jsonText = File.ReadAllText(path);

                var dataModel = JsonConvert.DeserializeObject<TemplateEntity>(jsonText);

                return dataModel;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                return null;
            }
        }

        public List<LegendaryKeyword> GetKeywords()
        {
            try
            {
                string path = settings.templatesFolder + "\\" + settings.json_keywords;
                string jsonText = File.ReadAllText(path);

                var dataModel = JsonConvert.DeserializeObject<IList<LegendaryKeyword>>(jsonText).ToList();

                return dataModel;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                return null;
            }
        }

        public List<LegendaryIconViewModel> LoadIconsFromDirectory()
        {
            try
            {

                string jsonText = File.ReadAllText(settings.templatesFolder + "\\" + settings.json_icons);
                var dataModel = JsonConvert.DeserializeObject<IList<LegendaryIconViewModel>>(jsonText).ToList();

                var result = new List<LegendaryIconViewModel>();

                foreach (LegendaryIconViewModel item in dataModel.ToList())
                {
                    item.FileName = item.FileName.ToLower();
                    result.Add(item);
                }

                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                return null;
            }

        }

        public DeckList GetDecks(string path)
        {
            try
            {
                string jsonText = File.ReadAllText(path);
                var dataModel = JsonConvert.DeserializeObject<DeckList>(jsonText);

                return dataModel;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                return null;
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
                Logger.Error(ex, ex.Message);
                return false;
            }
        }

        public bool CreateBackup(DeckList model, string path)
        {
            try
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(model, Formatting.Indented));
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                return false;
            }
        }

        public bool SaveKeywords(List<LegendaryKeyword> model)
        {
            try
            {
                string path = settings.templatesFolder + "\\" + settings.json_keywords;
                File.WriteAllText(path, JsonConvert.SerializeObject(model, Formatting.Indented));
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                return false;
            }
        }

        public bool SaveIcons(List<LegendaryIconViewModel> model)
        {
            try
            {
                string path = settings.templatesFolder + "\\" + settings.json_icons;
                File.WriteAllText(path, JsonConvert.SerializeObject(model, Formatting.Indented));
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                return false;
            }
        }

        public bool SaveTemplate(TemplateEntity model, string path)
        {
            try
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(model, Formatting.Indented));
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                return false;
            }
        }
    }

}
