using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace App.Common
{
    public class Config
    {
        public string Username = ConfigurationManager.AppSettings["username"];
        public string Password = ConfigurationManager.AppSettings["password"];
        public string Url = ConfigurationManager.AppSettings["url"];
    }
}
