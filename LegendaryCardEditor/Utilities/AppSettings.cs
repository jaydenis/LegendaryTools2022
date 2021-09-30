
using Newtonsoft.Json;
using System.IO;

namespace LegendaryCardEditor.Utilities
{
    public class AppSettings<T> where T : new()
    {
        private const string DEFAULT_FILENAME = "settings.json";

        public void Save(string fileName = DEFAULT_FILENAME)
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public static void Save(T pSettings, string fileName = DEFAULT_FILENAME)
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(pSettings, Formatting.Indented));
        }

        public static T Load(string fileName = DEFAULT_FILENAME)
        {
            T t = new T();
            if (File.Exists(fileName))
            {
                string jsonText = File.ReadAllText(fileName);
                t = JsonConvert.DeserializeObject<T>(jsonText);
            }
            return t;


        }
    }
}
