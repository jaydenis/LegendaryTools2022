﻿using LegendaryTemplateEditor.Models;
using LegendaryTemplateEditor.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegendaryTemplateEditor.Managers
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
        public List<Templates> GetTemplates()
        {
            string path = settings.templatesFolder + "\\" + settings.json_templates;
            string jsonText = File.ReadAllText(path);

            var dataModel = JsonConvert.DeserializeObject<IList<Templates>>(jsonText).ToList();

            return dataModel;
        }


        public List<TemplateTypeModel> GetTemplateTypes()
        {
            string path = settings.templatesFolder + "\\" + settings.json_templatetypes;
            string jsonText = File.ReadAllText(path);

            var dataModel = JsonConvert.DeserializeObject<IList<TemplateTypeModel>>(jsonText).ToList();

            return dataModel;
        }

        public TemplateEntity GetTemplate(string path)
        {
            path = settings.templatesFolder + "\\cards\\" + path;
            string jsonText = File.ReadAllText(path);

            var dataModel = JsonConvert.DeserializeObject<TemplateEntity>(jsonText);

            return dataModel;
        }

        public List<LegendaryKeyword> GetKeywords()
        {
            string path = settings.templatesFolder + "\\" + settings.json_keywords;
            string jsonText = File.ReadAllText(path);

            var dataModel = JsonConvert.DeserializeObject<IList<LegendaryKeyword>>(jsonText).ToList();

            return dataModel;
        }


   
        public List<LegendaryIconViewModel> LoadIconsFromDirectory()
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

        public bool CreateBackup(DeckList model, string path)
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
                return false;
            }
        }
    }


}
