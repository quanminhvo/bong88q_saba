using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;
using LiveBetApp.Models.DataModels;


namespace LiveBetApp.Common
{
    public class Config
    {
        public static ConfigModel GetConfigModel()
        {
            try
            {
                using (StreamReader r = new StreamReader("Config.json"))
                {
                    string json = r.ReadToEnd();
                    ConfigModel items = JsonConvert.DeserializeObject<ConfigModel>(json);
                    if (items.bong88Url != null && items.bong88Url.Length > 0 && items.bong88Url[items.bong88Url.Length - 1] == '/')
                    {
                        items.bong88Url = items.bong88Url.Substring(0, items.bong88Url.Length - 1);
                    }
                    return items;
                }
            }
            catch
            {
                return new ConfigModel();
            }

        }

        public static License GetLicense()
        {
            using (StreamReader r = new StreamReader("License.json"))
            {
                string json = r.ReadToEnd();
                License items = JsonConvert.DeserializeObject<License>(json);
                return items;
            }
        }

        public static ConfigModelSbo GetConfigModelSbo()
        {
            using (StreamReader r = new StreamReader("ConfigSbo.json"))
            {
                string json = r.ReadToEnd();
                ConfigModelSbo items = JsonConvert.DeserializeObject<ConfigModelSbo>(json);
                return items;
            }
        }

    }
}
