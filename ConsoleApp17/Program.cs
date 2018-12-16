using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp17
{
    class Config
    {
        Dictionary<string, string> values = new Dictionary<string, string>();
        private Config() { }
        public string FileName { get; set; }
        static Config instance;
        static public Config GetInstance()
        {
            if (instance == null)
            {
                instance = new Config();
            }
            return instance;
        }
        public void SerializeToJason()
        {
            using (StreamWriter writer = File.CreateText(FileName))
            {
                JsonSerializer json = new JsonSerializer();
                json.Serialize(writer, values);
            }
        }

        public void Load(string filename)
        {
            FileInfo Info = new FileInfo(filename);
            FileName = filename;
            if (!Info.Exists)
            {
                SerializeToJason();
            }
            else
            {
                values = DeserializeFromJson();
            }
        }
        public Dictionary<string, string> DeserializeFromJson()
        {
            using (StreamReader r = new StreamReader(FileName))
            {
                string json = r.ReadToEnd();
                values = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            }
            return values;
        }

        public void Write(string key, string value)
        {
            values[key] = value;
            SerializeToJason();
        }
        public string Read(string key)
        {
            var item = values[key];
            if (item != null)
            {
                return item;
            }
            else
            {
                throw new Exception("This data did not find");
            }
        }
        public void ShowData()
        {
            foreach (var item in values)
            {
                Console.WriteLine($" {item.Key}  -  {item.Value}");
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Config config = Config.GetInstance();
            config.Load("ELvin.json");
            config.ShowData();
            try
            {
                Console.WriteLine(config.Read("color"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}
